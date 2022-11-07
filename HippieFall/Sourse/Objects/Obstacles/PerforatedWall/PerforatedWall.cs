using System.Collections.Generic;
using Global;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Tunnels
{
	class PerforatedWall : Obstacle, IEffectable
	{
		[Export] private List<NodePath> _lasersPath;
		private float _rotationSpeed;
		public override void _Ready()
		{
			ChangeConfigData(new PerforatedWallConfig());
			ToggleLaserObstacle();
		}

		private void ToggleLaserObstacle()
		{
			foreach (var path in _lasersPath)
			{
				bool toggle = Utilities.GetRandomNumberInt(-1, 1, Utilities.Parameter.WITH_OUT_ZERO) == 1;
				Spatial laser = GetNode<Spatial>(path);
				if(toggle) laser.QueueFree();
			}
		}

		public override void _PhysicsProcess(float delta)
		{
			RotateY(_rotationSpeed*delta);
		}

		public void ChangeConfigData(Config config)
		{
			if (config is PerforatedWallConfig perforatedWallConfig)
			{
				_rotationSpeed = perforatedWallConfig.RotationSpeed;
			}
		}
	}
}
