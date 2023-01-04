using Global.Data;

namespace HippieFall.Items;

public class ShieldConfig:Config
{
    public bool IsShieldActivated { get; set; } = false;
    public float CountOfDestroy { get; set; } = 3f;

    public ShieldConfig()
    {
    }

    public ShieldConfig(Config config)
    {
        if (config is ShieldConfig shieldConfig)
        {
            CountOfDestroy = shieldConfig.CountOfDestroy;
            IsShieldActivated = shieldConfig.IsShieldActivated;
        }
    }
}