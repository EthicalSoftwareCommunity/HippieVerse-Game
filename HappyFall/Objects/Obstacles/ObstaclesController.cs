using System.Collections.Generic;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using GameController = HippieFall.Game.GameController;

namespace HippieFall.Tunnels
{
    public class ObstaclesController : Node, IEffectableController
    {
        public class ObstacleObject
        {
            private Config _config;
            public Config Config
            {
                get => _config;
                set
                {
                    _config = value;
                    if(Obstacle is IEffectable obstacle)
                        obstacle.ChangeConfigData(value);
                }
            }

            private Obstacle _obstacle;
            public Obstacle Obstacle;
        }
        
        public List<ObstacleObject> ObstacleObjects { get; set; }

        private EffectsController _effectController;
        
        private Config _config;
        private List<Config> _configs;
        private FanConfig _fan;
        private SawConfig _saw;

        public override void _Ready()
        {
            _fan = new FanConfig();
            _saw = new SawConfig();
            _configs = new List<Config>()
            {
                _fan,
                _saw
            };
            ObstacleObjects = new List<ObstacleObject>();
            _effectController = new EffectsController(Effect.EffectsTarget.Obstacles);
            
            GetNode("/root").GetChild(0).Connect(
                nameof(GameController.GameIsReady), 
                target:this, 
                nameof(Init));
            
            _effectController.DynamicEffectAdded += ApplyDynamicEffects;
            _effectController.ConstantEffectAdded += ApplyConstantEffect;

        }
        
        private void Init(GameController game)
        {
            game.GameEffectController.OnReceivedObstaclesEffect += _effectController.AddEffect;
        }
        public void ApplyDynamicEffects()
        {
            foreach (var obstacleObject in ObstacleObjects)
                obstacleObject.Config = _effectController.
                    ApplyEffectsOnConfig(GetConfigByType(obstacleObject.Obstacle));
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
        public void AddObstacle(Obstacle obstacle)
        {
            obstacle.OnDestroy += RemoveObstacle;
            ObstacleObject obj = new ObstacleObject
            {
                Obstacle = obstacle,
                Config = _effectController.ApplyEffectsOnConfig(GetConfigByType(obstacle))
            };
            ObstacleObjects.Add(obj);
        }
        public void RemoveObstacle(Obstacle obstacle)
        {
            foreach (var obstacleObject in ObstacleObjects)
            {
                if (obstacleObject.Obstacle == obstacle)
                {
                    ObstacleObjects.Remove(obstacleObject);
                }
            }
        }

        private Config GetConfigByType(Obstacle obstacle)
        {
            Config config = null;
            if (obstacle is Fan)
                config = new FanConfig(_fan);
            else if (obstacle is Saw)
                config = new SawConfig(_saw);
            return config;
        }
    }
}