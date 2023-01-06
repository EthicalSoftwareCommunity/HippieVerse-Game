using Global.Data;
using Global.Data.EffectSystem;
using Global.GameSystem;
using Godot;

namespace HippieFall.Game;

public class GameCameraEffectController : ObjectEffectController
{
    public GameCameraEffectController() : base(Effect.EffectsTarget.GameCamera)
    {
        
    }
    
    public override void Init(Node node, Config config)
    {
        AddNode(node, config);
        HippieFallUtilities.Game.GameEffectController.OnReceivedGameCameraEffect += EffectController.AddEffect;
    }
}