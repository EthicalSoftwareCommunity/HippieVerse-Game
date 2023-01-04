using System;
using System.Drawing.Printing;
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
			if (config is MagnetConfig magnetConfig)
			{
				Force = magnetConfig.Force;
				SpeedForceCorrection = magnetConfig.SpeedForceCorrection;
				IsMagnetActivated = magnetConfig.IsMagnetActivated;
			}
		}
	}
}
