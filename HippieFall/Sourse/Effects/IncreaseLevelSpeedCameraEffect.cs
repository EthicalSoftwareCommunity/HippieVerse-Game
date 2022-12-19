using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;

namespace HippieFall.Effects;

public class IncreaseLevelSpeedCameraEffect : Effect
{
    public IncreaseLevelSpeedCameraEffect()
    {
        Target = EffectsTarget.GameCamera;
    }

    public override Config Apply(Config config)
    {
        if (config is GameCameraConfig gameCameraConfig)
        {
            gameCameraConfig.Fov += 20;
            gameCameraConfig.MaxDistanceKoef += 0.2f;
            return gameCameraConfig;
        }
        return config;
    }
}