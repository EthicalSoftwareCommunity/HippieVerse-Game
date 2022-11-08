using System;
using Global;
using Godot;
using HippieFall.Biomes;

namespace HippieFall.Tunnels
{
	public class Tunnel:Spatial
	{
		[Export] private NodePath _tunnelPath;
		[Export] private NodePath _obstaclePath;

		private TunnelConfig _tunnelConfig;
		private Spatial _tunnel;
		private Spatial _obstacle;
		
		public TunnelConfig Config
		{
			get => _tunnelConfig;
			set
			{
				_tunnel.AddChild(value);
				_tunnelConfig = value;
			}
		}

		public Spatial ObstacleMesh
		{
			get => _obstacle;
			set
			{
				value.Translation = GetRandomPositionForObstacle();
				_obstacle.AddChild(value);
			}
		}

		public override void _Ready()
		{
			_tunnelConfig = new TunnelConfig();
			_tunnel = GetNode<Spatial>(_tunnelPath);
			_obstacle = GetNode<Spatial>(_obstaclePath);
		}

		private Vector3 GetRandomPositionForObstacle()
		{
			return Config.SpawnPoints[Utilities.GetRandomNumberInt(0, Config.SpawnPoints.Count)].Translation;
		}
	}
}
