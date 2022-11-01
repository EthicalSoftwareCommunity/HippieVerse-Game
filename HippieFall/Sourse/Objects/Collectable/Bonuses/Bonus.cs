using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Tunnels;

namespace HippieFall.Collectables
{
    public abstract class Bonus : Collectable
    {
        public List<Effect> Effects = new List<Effect>();

        public override void ChangeConfigData(Config config)
        {
            
        }
    }
}