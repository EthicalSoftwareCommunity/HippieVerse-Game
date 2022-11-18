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
        private Config _config;

        public Config Config
        {
            get => new LevelConfig(_config);
            set => ChangeConfigData(value);
        }

        public LevelConfig LevelConfig = new LevelConfig();

        private EffectsController _effectController;
        public override void _Ready()
        {
            Spawner = GetNode<TunnelSpawner>("TunnelsSpawner");
            _config = new LevelConfig();
            ChangeConfigData(_config);
            _effectController = new EffectsController(Effect.EffectsTarget.Level);
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
        }
        public void Init(GameController game)
        {
            _interface = game.GameInterface;
            game.GameEffectController.OnReceivedLevelEffect += _effectController.AddEffect;
            _effectController.DynamicEffectAdded += ApplyDynamicEffects;
            _effectController.ConstantEffectAdded += ApplyConstantEffect;

            _setConstantEffectTimer = new System.Timers.Timer(10000);
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
            _interface.GameScore.Text = Deep.ToString(CultureInfo.InvariantCulture);
            //_deepChangeTimer.Interval = 1000 / (LevelConfig.Speed/2+1);
        }

        private void ConstantEffects(object sender, ElapsedEventArgs e)
        {
            ApplyConstantEffect(new ChangeLevelSpeed(0.1f));
        }

        private void ApplyDynamicEffects()
        {
            Config = _effectController.ApplyEffectsOnConfig(Config);
            ConfigChanged?.Invoke(Config);
        }

        private void ApplyConstantEffect(Effect effect)
        {
            _config = effect.Apply(_config);
            Config = _config;
            ApplyDynamicEffects();
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
             if (!(config is LevelConfig levelConfig)) return;
            LevelConfig.DeepIncrease = levelConfig.DeepIncrease;
            LevelConfig.Speed = levelConfig.Speed;
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