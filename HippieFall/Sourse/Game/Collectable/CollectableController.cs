using System.CodeDom;
using System.Collections.Generic;
using System.Linq.Expressions;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using HippieFall.Collectables;
using HippieFall.Game;
using HippieFall.Tunnels;
using GameController = HippieFall.Game.GameController;

namespace HippieFall
{
    public class CollectableController:Node, IEffectableController
    {
        public class CollectableObject
        {
            private Config _config;
            public Config Config
            {
                get => _config;
                set
                {
                    _config = value;
                    if(Collectable is IEffectable collectable)
                        collectable.ChangeConfigData(value);
                }
            }

            public Collectable Collectable { get; set; }
        }
        [Export] private NodePath _collectableSpawnerPath;
        public CollectableSpawner CollectableSpawner { get; private set; }
        private List<CollectableObject> CollectableObjects { get; set; } = new List<CollectableObject>();
        private EffectsController _effectController;
        private Config _config;
        private List<Config> _configs;
        private CollectableCoinConfig _coinConfig = new CollectableCoinConfig();
        private CollectableGemcoinConfig _gemCoinConfig = new CollectableGemcoinConfig();
        private CollectableChestConfig _chestConfig = new CollectableChestConfig();
        public override void _Ready()
        {
            CollectableSpawner = GetNode<CollectableSpawner>(_collectableSpawnerPath);
            CollectableSpawner.OnCollectableCreated += AddCollectable;
            _configs = new List<Config>()
            {
                _coinConfig,
                _gemCoinConfig,
                _chestConfig
            };
            _effectController = new EffectsController(Effect.EffectsTarget.Collectable);
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
        }

        private void Init(GameController game)
        {
            game.GameEffectController.OnReceivedCollectableEffect += _effectController.AddEffect;
            _effectController.DynamicEffectAdded += ApplyDynamicEffects;
            _effectController.ConstantEffectAdded += ApplyConstantEffect;
        }
        public void AddCollectable(Collectable collectable)
        {
            collectable.OnDestroy += RemoveObstacle;
            CollectableObject obj = new CollectableObject
            {
                Collectable = collectable,
                Config = _effectController.ApplyEffectsOnConfig(GetConfigByType(collectable))
            };
            CollectableObjects.Add(obj);
        }
        public void RemoveObstacle(Collectable collectable)
        {
            foreach (var collectableObject in CollectableObjects)
            {
                if (collectableObject.Collectable == collectable)
                {
                    CollectableObjects.Remove(collectableObject);
                    return;
                }
            }
        }
        public void ApplyDynamicEffects()
        {
            foreach (var collectableObject in CollectableObjects)
                collectableObject.Config =
                    _effectController.ApplyEffectsOnConfig(GetConfigByType(collectableObject.Collectable));
            CollectableSpawner.ReloadSetting();
        }
        
        public void ApplyConstantEffect(Effect effect)
        {
            foreach (var config in _configs)
            {
                _config = config;
                _config = effect.Apply(_config);
            }
            ApplyDynamicEffects();
        }
        private Config GetConfigByType(Collectable collectable)
        {
            if (collectable is CollectableCoin)
                return new CollectableCoinConfig(_coinConfig);
            if (collectable is CollectableGemcoin)
                return new CollectableGemcoinConfig(_gemCoinConfig);
            if (collectable is CollectableChest)
                return  new CollectableChestConfig(_chestConfig);
            return null;
        }

    }
}