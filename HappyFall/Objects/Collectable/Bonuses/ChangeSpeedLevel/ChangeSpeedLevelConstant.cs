using HippieFall.Tunnels;

namespace HippieFall.Collectables
{
    public class ChangeSpeedLevelConstant : Bonus
    {
        public ChangeSpeedLevelConstant()
        {
            Effects.Add(new ChangeLevelSpeed());
        }
    }
}