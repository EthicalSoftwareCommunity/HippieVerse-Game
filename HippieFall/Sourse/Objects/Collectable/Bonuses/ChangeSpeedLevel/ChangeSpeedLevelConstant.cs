using HippieFall.Effects;

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