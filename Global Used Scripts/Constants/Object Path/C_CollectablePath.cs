namespace Global.Constants
{
    public class C_CollectablePath
    {
        private const string PATH_TO_REWARDS = "res://HappyFall/Objects/Collectable/Rewards/";
        private const string PATH_TO_BONUSES = "res://HappyFall/Objects/Collectable/Bonuses/";
        
        //Rewards
        public const string COIN = PATH_TO_REWARDS + "Coin/CollectableCoin.tscn";
        public const string GEMCOIN = PATH_TO_REWARDS + "Gemcoin/CollectableGemcoin.tscn";
        public const string CHEST = PATH_TO_REWARDS + "Chest/CollectableChest.tscn";

        //Bonuses
        public const string SLOW_OBSTACLES = PATH_TO_BONUSES + "SlowObstacles/SlowObstacles.tscn";
        public const string DOUBLE_SCORE = PATH_TO_BONUSES + "DoubleScore/DoubleScore.tscn";
        public const string MAGNET = PATH_TO_BONUSES + "Magnet/MagnetBonus.tscn";
    }
}