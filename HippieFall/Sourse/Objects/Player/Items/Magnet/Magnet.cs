using Godot;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;

using HippieFall.Collectables;

namespace HippieFall.Items
{
	public class Magnet : Item, IEffectable
	{
		private List<Collectable> _collectablesInRange;
		private Vector3 _directionTo;
		private MagnetConfig _config;

		public override void _Ready()
		{
			_config = new MagnetConfig();
			_collectablesInRange = new List<Collectable>();
			Visible = false;
			SetPhysicsProcess(false);
			SetBlockSignals(false);
		}

		public override void _PhysicsProcess(float delta)
		{
			foreach (var collectable in _collectablesInRange)
			{
				if(IsInstanceValid(collectable))
				{
					_directionTo = collectable.GlobalTranslation.DirectionTo(GlobalTranslation);
					collectable.GlobalTranslation += delta * _directionTo * (_config.Force+_config.SpeedForceCorrection/3);
				}
			}
		}
		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<CollectableCoin>() is Collectable collectable)
				_collectablesInRange.Add(collectable);
		}

		private void OnAreaExited(Area area)
		{
			if (area.GetOwnerOrNull<Collectable>() is Collectable collectable)
				_collectablesInRange.Remove(collectable);
		}

		public void ChangeConfigData(Config config)
		{
			_config = new(((ItemConfig)config).MagnetConfig);
			Visible = _config.IsMagnetActivated;
			SetPhysicsProcess(_config.IsMagnetActivated);
			SetBlockSignals(_config.IsMagnetActivated);
		}
	}

}
