using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Timers;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.Data.GameSystem;
using HippieFall.Effects;
using HippieFall.Tunnels;

namespace HippieFall.Game
{
    public class LevelController : Spatial, IEffectable, IPauseable
    {
        public event Action<Config> ConfigChanged;
        public event Action<List<Effect>> OnLevelEffectAdded;
        public float Deep { get; set; }
        
        private System.Timers.Timer _setConstantEffectTimer;
        private System.Timers.Timer _deepChangeTimer;
        private GameInterface _interface;
        public TunnelSpawner Spawner { get; private set; }

        public Config _config;
        public LevelConfig LevelConfig
        {
            get => new LevelConfig(_config);
            set => _config = value;
        }

        private EffectsController _effectController;
        public override void _Ready()
        {
            Spawner = GetNode<TunnelSpawner>("TunnelsSpawner");
            _config = new LevelConfig();
            _effectController = new EffectsController(Effect.EffectsTarget.Level);
            HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        }
        public void Init()
        {
            _interface = HippieFallUtilities.Game.GameInterface;
            
            _setConstantEffectTimer = new System.Timers.Timer(1000);
            _setConstantEffectTimer.Elapsed += ConstantEffects;
            _setConstantEffectTimer.AutoReset = true;
            _setConstantEffectTimer.Start();

            _deepChangeTimer = new System.Timers.Timer(1000 / LevelConfig.Speed);
            _deepChangeTimer.Elapsed += DeepMovementChange;
            _deepChangeTimer.AutoReset = true;
            _deepChangeTimer.Start();
            
        }
        public override void _ExitTree()
        {
            _deepChangeTimer.Dispose();
            _setConstantEffectTimer.Dispose();
        }

        private void DeepMovementChange(object sender, ElapsedEventArgs e)
        {
            Deep += LevelConfig.DeepIncrease;
            _deepChangeTimer.Interval = (1000 / LevelConfig.Speed);
            _interface.GameScore.Text = Deep.ToString(CultureInfo.InvariantCulture);
        }

        private void ConstantEffects(object sender, ElapsedEventArgs e)
        {
            OnLevelEffectAdded?.Invoke(new List<Effect>(){new ChangeLevelSpeed(0.1f)});
        }

        public override void _Process(float delta)
        {
            foreach (var tunnel in Spawner.Tunnels) 
                tunnel.Translation += new Vector3(0, LevelConfig.Speed * delta, 0);
        }

        public void OnDestroyTunnelTriggerAreaEntered(Area area)
        {
            Spawner.CashRemoveTunnel(area.GetOwnerOrNull<Tunnel>());
            SpawnTunnelAgain();
        }

        private void SpawnTunnelAgain()
        {
            var position = Spawner.Tunnels.Last().Translation + Spawner.TunnelsOffset;
            Spawner.SpawnTunnel(position);
        }

        public void ChangeConfigData(Config config)
        {
            if (config is LevelConfig levelConfig)
            {
                LevelConfig = levelConfig;
            }
        }

        public void Pause()
        {
            _setConstantEffectTimer.Stop();
            _deepChangeTimer.Stop();
        }

        public void Resume()
        {
            _setConstantEffectTimer.Start();
            _deepChangeTimer.Start();
        }
    }
}