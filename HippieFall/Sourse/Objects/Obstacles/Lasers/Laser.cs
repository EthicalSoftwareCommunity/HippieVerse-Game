using System;
using System.Collections.Generic;
using System.Timers;
using Global;
using Global.Constants;
using Godot;
using Timer = System.Timers.Timer;

namespace HippieFall.Tunnels
{
	class Laser : Obstacle
	{
		
		public List<Laser> LasersTargets = new List<Laser>();
		public List<Spatial> Lasers = new List<Spatial>();
		
		public void AddLaser()
		{
			Spatial laserMesh = GD.Load<PackedScene>(C_TunnelObstaclePath.CYBER_LASER).Instance<Spatial>();
			//Make laser Material unique for various animation
			((CSGBox)laserMesh).Material = ((CSGBox)laserMesh)?.Material.Duplicate() as Material;
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
					if(IsInstanceValid(Lasers[i]))
					   Lasers[i].QueueFree();
					break;
				}
			
			if (directionType == LaserController.DirectionType.Duplex)
				target.HideAtLaserBarrel(this, LaserController.DirectionType.Single);
		}
		
		public void FlashingAtLaserBarrel(Laser target, LaserController.DirectionType directionType, float flashTime = 0)
		{
			for (int i = 0; i < LasersTargets.Count; i++)
				if (LasersTargets[i] == target)
				{
					if (IsInstanceValid(Lasers[i]))
					{
						if (flashTime == 0)
							flashTime = Utilities.GetRandomNumberFloat(1f,3f);
						Lasers[i].GetNode<AnimationPlayer>("AnimationPlayer").PlaybackSpeed
								= flashTime; 
						
					}
					break;
				}
			if (directionType == LaserController.DirectionType.Duplex)
				target.FlashingAtLaserBarrel(this, LaserController.DirectionType.Single,flashTime);
			
		}
	}
	
}
