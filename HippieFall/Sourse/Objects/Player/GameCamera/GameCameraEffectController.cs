using Global.Data;
using Global.Data.EffectSystem;
using Global.GameSystem;
using Godot;

namespace HippieFall.Game;

public class GameCameraEffectController : ObjectEffectController
{
    private GameCameraConfig _gameCameraConfig;
    public GameCameraEffectController() : base(Effect.EffectsTarget.GameCamera)
    {
        
    }
    
    public override void Init(Node node, Config config)
    {
        _gameCameraConfig = new((GameCameraConfig)config);
        Configs.Add(_gameCameraConfig);
        AddNode(node);
        HippieFallUtilities.Game.GameEffectController.OnReceivedGameCameraEffect += EffectController.AddEffect;
    }

    public override Config GetConfigByType(Node node)
    {
        if (node is GameCamera) return new GameCameraConfig(_gameCameraConfig);
        return null;
    }
}