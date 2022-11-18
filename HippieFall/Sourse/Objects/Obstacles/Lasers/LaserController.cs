using System;
using System.Collections.Generic;
using System.Timers;
using Global;
using Global.Constants;
using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;
using Timer = System.Timers.Timer;

namespace HippieFall.Tunnels
{
	class LaserController : Obstacle
	{
		public List<LaserController> LasersTargets = new List<LaserController>();
		public List<Laser> Lasers = new List<Laser>();

		public override void _Ready()
		{
			ObstacleType = ObstacleTypes.Controller;
		}

		public void AddLaser()
		{
			Laser laser = GD.Load<PackedScene>(C_TunnelObstaclePath.CYBER_LASER).Instance<Laser>();
			AddChild(laser);
			Lasers.Add(laser);
		}

		public void LookAtLaserBarrel(LaserController target, LasersPlatformController.DirectionType directionType)
		{
			LasersTargets.Add(target);
			LookAt(target.GlobalTranslation, Vector3.Up);
			RotateZ(Mathf.Deg2Rad(90));
			AddLaser();
			
			if (directionType == LasersPlatformController.DirectionType.Duplex)
				target.LookAtLaserBarrel(this, LasersPlatformController.DirectionType.Single);
		}
		
		public void HideAtLaserBarrel(LaserController target, LasersPlatformController.DirectionType directionType)
		{
			for (int i = 0; i < LasersTargets.Count; i++)
				if (LasersTargets[i] == target)
				{
					if(IsInstanceValid(Lasers[i]))
					   Lasers[i].QueueFree();
					break;
				}
			
			if (directionType == LasersPlatformController.DirectionType.Duplex)
				target.HideAtLaserBarrel(this, LasersPlatformController.DirectionType.Single);
		}
		
		public void FlashingAtLaserBarrel(LaserController target, LasersPlatformController.DirectionType directionType, float flashTime = 0)
		{
			for (int i = 0; i < LasersTargets.Count; i++)
				if (LasersTargets[i] == target)
				{
					if (IsInstanceValid(Lasers[i]))
					{
						if (flashTime == 0)
							flashTime = Utilities.GetRandomNumberFloat(0.7f, 2f);
						//Make laser Material unique for various animation
						Lasers[i].GetNode<AnimationPlayer>("AnimationPlayer").PlaybackSpeed
								= flashTime; 
						
					}
					break;
				}
			if (directionType == LasersPlatformController.DirectionType.Duplex)
				target.FlashingAtLaserBarrel(this, LasersPlatformController.DirectionType.Single,flashTime);
			
		}
	}
	
}
