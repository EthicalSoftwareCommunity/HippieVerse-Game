using Godot;
using System;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Collectables;
using HippieFall.Tunnels;

public class MagnetBonus : Bonus
{
	public MagnetBonus()
	{
		Config = new CollectableConfig()
		{
			SpawnWeight = 30f
		};
		Effects.Add(new DynamicEffect(new Magnet(10f), 20f));
	}
}
