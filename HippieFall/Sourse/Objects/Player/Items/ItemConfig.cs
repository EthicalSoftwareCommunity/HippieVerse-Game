using Global.Data;

namespace HippieFall.Items
{
    public class ItemConfig : Config
    {
        public MagnetConfig MagnetConfig;
        public ShieldConfig ShieldConfig;

        public ItemConfig()
        {
            MagnetConfig = new ();
            ShieldConfig = new ();
        }

        public ItemConfig(ItemConfig configItemConfig)
        {
            MagnetConfig = new MagnetConfig(configItemConfig.MagnetConfig);
            ShieldConfig = new ShieldConfig(configItemConfig.ShieldConfig);
        }
    }
}