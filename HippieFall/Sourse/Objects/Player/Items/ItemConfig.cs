using Global.Data;

namespace HippieFall.Items
{
    public class ItemConfig : Config
    {
        public MagnetConfig MagnetConfig;

        public ItemConfig()
        {
            MagnetConfig = new MagnetConfig();
        }

        public ItemConfig(ItemConfig configItemConfig)
        {
            MagnetConfig = new MagnetConfig(configItemConfig.MagnetConfig);
        }
        
        public static bool operator == (ItemConfig a, ItemConfig b)
        {
            return (a.MagnetConfig == b.MagnetConfig);
        }

        public static bool operator !=(ItemConfig a, ItemConfig b)
        {
            return !(a == b);
        }
    }
}