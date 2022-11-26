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
        public LevelConfig(LevelConfig config)
        {
                Speed = config.Speed;
                DeepIncrease = config.DeepIncrease;
        }
    }
}