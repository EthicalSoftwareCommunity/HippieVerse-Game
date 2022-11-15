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
        [Export] private float _speed;
        [Export] private float _deepIncrease;
        public event Action<Config> ConfigChanged;
        public event Action<List<Effect>> OnLevelEffectAdded;
        public float Deep { get; set; }
        
        private System.Timers.Timer _setConstantEffectTimer;
        private System.Timers.Timer _deepChangeTimer;
        private GameInterface _interface;
        private TunnelSpawner _spawner;
        private Config _config;

        public Config Config
        {
            get => new LevelConfig(_config);
            set => ChangeConfigData(value);
        }

        private EffectsController _effectController;
        public override void _Ready()
        {
            _spawner = GetNode<TunnelSpawner>("TunnelsSpawner");
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

            _setConstantEffectTimer = new System.Timers.Timer(1000);
            _setConstantEffectTimer.Elapsed += ConstantEffects;
            _setConstantEffectTimer.AutoReset = true;
            _setConstantEffectTimer.Start();

            _deepChangeTimer = new System.Timers.Timer(1000 / _speed);
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
            Deep += _deepIncrease;
            _interface.GameScore.Text = Deep.ToString(CultureInfo.InvariantCulture);
            _deepChangeTimer.Interval = 1000 / (_speed/2+1);
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
            foreach (var tunnel in _spawner.Tunnels) 
                tunnel.Translation += new Vector3(0, _speed * delta, 0);
        }

        public void OnDestroyTunnelTriggerAreaEntered(Area area)
        {
            _spawner.RemoveTunnel(area.GetParentOrNull<Tunnel>());
            SpawnTunnelAgain();
        }

        private void SpawnTunnelAgain()
        {
            var position = _spawner.Tunnels.Last().Translation + _spawner.TunnelsOffset;
            _spawner.SpawnTunnel(position);
        }

        public void ChangeConfigData(Config config)
        {
             if (!(config is LevelConfig levelConfig)) return;
            _deepIncrease = levelConfig.DeepIncrease;
            _speed = levelConfig.Speed;
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