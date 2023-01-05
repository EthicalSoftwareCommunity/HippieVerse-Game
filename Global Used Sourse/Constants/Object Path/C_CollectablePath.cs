namespace Global.Constants
{
	public class C_CollectablePath
	{
		private const string PATH_TO_REWARDS = "res://HippieFall/Sourse/Objects/Collectable/Rewards/";
		private const string PATH_TO_BONUSES = "res://HippieFall/Sourse/Objects/Collectable/Bonuses/";
		
		//Rewards
		public const string COIN = PATH_TO_REWARDS + "Coin/CollectableCoin.tscn";
		public const string CRYSTAL = PATH_TO_REWARDS + "Crystal/CollectableCrystal.tscn";
		public const string CRYSTAL_DEPOSIT = PATH_TO_REWARDS + "Crystal/CollectableCrystalDeposit.tscn";
		public const string CHEST = PATH_TO_REWARDS + "Chest/CollectableChest.tscn";

		//Bonuses
		public const string SLOW_OBSTACLES = PATH_TO_BONUSES + "SlowObstacles/SlowObstacles.tscn";
		public const string DOUBLE_SCORE = PATH_TO_BONUSES + "DoubleScore/DoubleScore.tscn";
		public const string MAGNET = PATH_TO_BONUSES + "Magnet/MagnetBonus.tscn";
		public const string SHIELD = PATH_TO_BONUSES + "Shield/ShieldBonus.tscn";
	}
}
