using Global.Data;
using Global.Data.EffectSystem;
using Global.GameSystem;
using Godot;

namespace HippieFall.Game;

class LevelEffectController : ObjectEffectController
{
    private LevelConfig _levelConfig;
    public LevelEffectController() : base(Effect.EffectsTarget.Level)
    {
    }
    
    public override void Init(Node node, Config config)
    {
        _levelConfig = new();
        AddNode(node, _levelConfig);
        HippieFallUtilities.Game.GameEffectController.OnReceivedLevelEffect += EffectController.AddEffect;
    }
}