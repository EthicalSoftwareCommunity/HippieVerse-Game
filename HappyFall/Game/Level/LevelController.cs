using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Timers;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using HippieFall.Tunnels;

namespace HippieFall.Game
{
    public class LevelController : Spatial, IEffectable
    {
        public event Action<Config> ConfigChanged;
        public event Action<List<Effect>> OnLevelEffectAdded;
        private System.Timers.Timer timer;
        [Export] private float _deep;
        [Export] private float _deepIncrease;
        [Export] private float _speed;
        private GameInterface _interface;
        private Player _player;
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
            _player = GetNode<Player>("../Player");
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

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += ConstantEffects;
            timer.AutoReset = true;
            timer.Start();
        }

        private void ConstantEffects(object sender, ElapsedEventArgs e)
        {
            ApplyConstantEffect(new ChangeLevelSpeed(0.1f));
        }

        public void ApplyDynamicEffects()
        {
            Config = _effectController.ApplyEffectsOnConfig(Config);
            ConfigChanged?.Invoke(Config);
        }

        public void ApplyConstantEffect(Effect effect)
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
            _deep += _deepIncrease;
            _interface.GameScore.Text = _deep.ToString(CultureInfo.InvariantCulture);
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
    }
}