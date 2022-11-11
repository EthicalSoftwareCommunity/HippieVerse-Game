using Global.Data.EffectSystem;
using HippieFall.Collectables;
using HippieFall.Effects;

public class MagnetBonus : Bonus
{
	public MagnetBonus()
	{
		Config = new CollectableConfig()
		{
			SpawnWeight = 300f
		};
		Effects.Add(new DynamicEffect(new Magnet(10f), 20f));
	}
}
