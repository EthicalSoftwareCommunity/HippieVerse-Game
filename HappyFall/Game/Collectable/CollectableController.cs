using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using HippieFall.Collectables;
using HippieFall.Game;
using GameController = HippieFall.Game.GameController;

namespace HippieFall
{
    public class CollectableController:Node, IEffectableController
    {
        [Export] private NodePath _collectableSpawnerPath;
        public CollectableSpawner CollectableSpawner { get; private set; }
        
        private EffectsController _effectController;

        public override void _Ready()
        {
            CollectableSpawner = GetNode<CollectableSpawner>(_collectableSpawnerPath);
            _effectController = new EffectsController(Effect.EffectsTarget.Collectable);
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
        }

        private void Init(GameController game)
        {
            game.GameEffectController.OnReceivedCollectableEffect += _effectController.AddEffect;
            _effectController.DynamicEffectAdded += ApplyDynamicEffects;
            _effectController.ConstantEffectAdded += ApplyConstantEffect;
        }
        public void ApplyDynamicEffects()
        {
            foreach (var collectable in CollectableSpawner.CollectableItems)
                collectable.Config = (CollectableConfig)_effectController.
                    ApplyEffectsOnConfig(collectable.Config);
            CollectableSpawner.ReloadSetting();
        }

        public void ApplyConstantEffect(Effect effect)
        {
            foreach (var collectable in CollectableSpawner.CollectableItems)
                collectable.Config = (CollectableConfig)effect.Apply(collectable.Config);
            ApplyDynamicEffects();
        }

    }
}