using Global.Data;

namespace HippieFall.Game
{
    public class LevelConfig : Config
    {
        public float Speed = 8;
        public float DeepIncrease = 1;

        public LevelConfig()
        {
            
        }
        public LevelConfig(Config config)
        {
            if (config is LevelConfig _config)
            {
                Speed = _config.Speed;
                DeepIncrease = _config.DeepIncrease;
            }
           
        }
    }
}