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
			public Obstacle Obstacle  { get; set; }
		}
		
		public List<ObstacleObject> ObstacleObjects { get; set; }

		private EffectsController _effectController;
		
		private Config _config;
		private FanConfig _fan;
		private SawConfig _saw;
		private List<Config> _configs;
		private PerforatedWallConfig _perforatedWall;

		public override void _Ready()
		{
			_fan = new FanConfig();
			_saw = new SawConfig();
			_perforatedWall = new PerforatedWallConfig();
			_configs = new List<Config>()
			{
				_fan,
				_saw,
				_perforatedWall,
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
					return;
				}
			}
		}

		private Config GetConfigByType(Obstacle obstacle)
		{
			if (obstacle is Fan)
				return new FanConfig(_fan);
			if (obstacle is Saw)
				return new SawConfig(_saw);
			if (obstacle is PerforatedWall)
				return  new PerforatedWallConfig(_perforatedWall);
			return null;
		}
	}
}
