using Godot;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Global;
using HippieFall.Tunnels;

namespace HippieFall.Tunnels
{
	class LaserController : Obstacle
	{
		[Export] private List<NodePath> _laserBarrelPaths = new List<NodePath>();
		public enum DirectionType
		{
			Duplex,
			Single
		}
		private List<LaserPattern> _presets = new List<LaserPattern>();
		private List<Laser> _laserBarrel = new List<Laser>();
		private LaserPattern _currentPreset;
		public override void _Ready()
		{
			ObstacleType = ObstacleTypes.Controller;
			InitPresets();
			InitLasers();
			_currentPreset = _presets[Utilities.GetRandomNumberInt(0, _presets.Count)];
			LoadLaserPreset();
		}
		private void LoadLaserPreset()
		{
			foreach (var direction in _currentPreset.Preset)
				_laserBarrel[direction.From].LookAtLaserBarrel(_laserBarrel[direction.To], direction.DirectionType);
		}
		
		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<Player>() != null)
				HideLaserPreset();
		}
		
		private void HideLaserPreset()
		{
			foreach (var direction in _currentPreset.Hideable)
				_laserBarrel[direction.From].HideAtLaserBarrel(_laserBarrel[direction.To], direction.DirectionType);
		}
		private void InitPresets()
		{ /* Positions
		 * 0  3
		 * 1  4
		 * 2  5
		 */
			//hoizonal
			LaserPattern horizontal = new LaserPattern()
			{
				Preset =
				{
					new LaserPattern.LaserDirection(0, 3, DirectionType.Duplex),
					new LaserPattern.LaserDirection(1, 4, DirectionType.Duplex),
					new LaserPattern.LaserDirection(2, 5, DirectionType.Duplex),
				},
			};
			horizontal.Hideable.Add(horizontal.Preset[new Random().Next(0,horizontal.Preset.Count)]);
			_presets.Add(horizontal);
			
			//crossOver
			LaserPattern crossOver = new LaserPattern
			{
				Preset =
				{
					new LaserPattern.LaserDirection(0, 5, DirectionType.Duplex),
					new LaserPattern.LaserDirection(1, 4, DirectionType.Duplex),
					new LaserPattern.LaserDirection(2, 3, DirectionType.Duplex),
				}
			};
			crossOver.Hideable.Add(crossOver.Preset[new Random().Next(0,crossOver.Preset.Count)]);
			_presets.Add(crossOver);
			
			//all
			/*LaserPattern all = new LaserPattern();
			all.Preset.Add(new LaserPattern.LaserDirection(0, 3, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(0, 4, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(0, 5, DirectionType.Duplex));
			/*all.Preset.Add(new LaserPattern.LaserDirection(1, 3, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(1, 4, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(1, 5, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(2, 3, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(2, 4, DirectionType.Duplex));
			all.Preset.Add(new LaserPattern.LaserDirection(2, 5, DirectionType.Duplex));#1#

			for (int i = 0; i <= all.Preset.Count/2+1; i++)
			{
				all.Hideable.Add(all.Preset[Utilities.GetRandomNumberInt(0,all.Preset.Count)]);
			}*/
			/*_presets.Add(all);*/
		}

		private void InitLasers()
		{
			foreach (NodePath laserPath in _laserBarrelPaths)
				_laserBarrel.Add(GetNode<Laser>(laserPath));
		}
	}
	
	class LaserPattern
	{
		public struct LaserDirection
		{
			public readonly int From;
			public readonly int To;
			public readonly LaserController.DirectionType DirectionType;
			public LaserDirection(int from, int to, LaserController.DirectionType directionType)
			{
				From = from;
				To = to;
				DirectionType = directionType;
			}
		}
		
		public List<LaserDirection> Preset { get; set; }
		public List<LaserDirection> Hideable{ get; set; }

		public LaserPattern()
		{
			Preset = new List<LaserDirection>();
			Hideable = new List<LaserDirection>();
		}
	}

}
