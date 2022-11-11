using System;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
	public abstract class Collectable : Spatial, IEffectable
	{
		public event Action<Collectable> OnDestroy; 
		protected CollectableConfig _config;
		public virtual CollectableConfig Config { get; set; }
		
		protected virtual void OnAreaEntered(Area area)
		{
			if (area.GetParentOrNull<Player>() != null) Destroy();
		}
		public void Destroy()
		{
			OnDestroy?.Invoke(this);
			QueueFree();
		}
		public abstract void ChangeConfigData(Config config);
	}
}
