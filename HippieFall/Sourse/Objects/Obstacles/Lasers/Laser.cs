using System;
using System.Collections.Generic;
using Global;
using Global.Constants;
using Godot;

namespace HippieFall.Tunnels
{
	class Laser : Obstacle
	{
		private Spatial _mesh = new Spatial();
		public Spatial Mesh
		{
			get => _mesh;
			set
			{
				AddChild(value);
				_mesh = value;
			}
		}
		public List<Laser> LasersTargets = new List<Laser>();
		public List<Spatial> Lasers = new List<Spatial>();
		
		public void AddLaser()
		{
			Spatial laserMesh = GD.Load<PackedScene>(C_TunnelObstaclePath.CYBER_LASER).Instance<Spatial>();
			AddChild(laserMesh);
			Lasers.Add(laserMesh);
		}
		
		public void LookAtLaserBarrel(Laser target, LaserController.DirectionType directionType)
		{
			LasersTargets.Add(target);
			LookAt(target.GlobalTranslation, Vector3.Up);
			RotateZ(Mathf.Deg2Rad(90));
			AddLaser();
			
			if (directionType == LaserController.DirectionType.Duplex)
				target.LookAtLaserBarrel(this, LaserController.DirectionType.Single);
		}
		

		public void HideAtLaserBarrel(Laser target, LaserController.DirectionType directionType)
		{
			for (int i = 0; i < LasersTargets.Count; i++)
				if (LasersTargets[i] == target)
				{
					Lasers[i].QueueFree();
					break;
				}
			
			if (directionType == LaserController.DirectionType.Duplex)
				target.HideAtLaserBarrel(this, LaserController.DirectionType.Single);
		}
	}
	
}
