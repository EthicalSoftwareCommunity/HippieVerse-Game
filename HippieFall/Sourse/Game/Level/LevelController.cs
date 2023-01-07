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

        [Export] public NodePath TunnelSpawnerPath;
        public float Deep { get; set; }
        
        private System.Timers.Timer _setConstantEffectTimer;
        private System.Timers.Timer _deepChangeTimer;
        private GameInterface _interface;
        public TunnelSpawner Spawner { get; private set; }
        public LevelConfig LevelConfig { get; set; }
        private LevelEffectController _effectController;
        
        public override void _Ready()
        {
            Spawner = GetNode<TunnelSpawner>("TunnelsSpawner");
            LevelConfig = new LevelConfig();
            HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        }
        public void Init()
        {
            _effectController = new LevelEffectController();
            _effectController.Init(this, LevelConfig);
            _interface = HippieFallUtilities.Game.GameInterface;
        }

        public void StartGame()
        {
            _setConstantEffectTimer = new System.Timers.Timer(1000);
            _setConstantEffectTimer.Elapsed += ConstantEffects;
            _setConstantEffectTimer.AutoReset = true;
            _setConstantEffectTimer.Start();

            _deepChangeTimer = new System.Timers.Timer(1000 / LevelConfig.Speed);
            _deepChangeTimer.Elapsed += DeepMovementChange;
            _deepChangeTimer.AutoReset = true;
            _deepChangeTimer.Start();
            
            Spawner.StartGame();
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

        public override void _PhysicsProcess(float delta)
        {
            foreach (var tunnel in Spawner.Tunnels) 
                tunnel.Translation += new Vector3(0, LevelConfig.Speed * delta, 0);
        }

        public void OnDestroyTunnelTriggerAreaEntered(Area area)
        {
            Spawner.CashRemoveTunnel(area.GetOwnerOrNull<Tunnel>());
            Spawner.SpawnTunnelAgain();
        }

        public void ChangeConfigData(Config config)
        {
            if (config is LevelConfig levelConfig)
            {
                LevelConfig = levelConfig;
            }
            ConfigChanged?.Invoke(LevelConfig);
        }

        public void Pause()
        {
            _setConstantEffectTimer?.Stop();
            _deepChangeTimer?.Stop();
        }

        public void Resume()
        {
            _setConstantEffectTimer?.Start();
            _deepChangeTimer?.Start();
        }
    }
}