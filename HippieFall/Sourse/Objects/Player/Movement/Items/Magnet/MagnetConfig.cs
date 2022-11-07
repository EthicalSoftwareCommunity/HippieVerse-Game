using System;
using Global.Data;

namespace HippieFall.Items
{
    public class MagnetConfig : Config
    {
        public bool IsMagnetActivated { get; set; } = false;
        public float Force { get; set; }
        public float SpeedForceCorrection { get; set; } = 1f;

        public MagnetConfig()
        {
        }

        public MagnetConfig(Config config)
        {
            MagnetConfig magnetConfig = (MagnetConfig)config;
            Force = magnetConfig.Force;
            SpeedForceCorrection = magnetConfig.SpeedForceCorrection;
            IsMagnetActivated = magnetConfig.IsMagnetActivated;
        }
    }
}