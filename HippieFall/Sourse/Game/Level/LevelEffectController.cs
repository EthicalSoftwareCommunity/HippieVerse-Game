using Global.Data;
using Global.Data.EffectSystem;
using Global.GameSystem;
using Godot;

namespace HippieFall.Game;

public class LevelEffectController : ObjectEffectController
{
    private LevelConfig _levelConfig;
    public LevelEffectController() : base(Effect.EffectsTarget.Level)
    {
    }

    public override void _Ready()
    {
        _levelConfig = new();
        Configs.Add(_levelConfig);
        HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
    }

    protected override void Init()
    {
        AddNode(HippieFallUtilities.Game.Level);
        HippieFallUtilities.Game.GameEffectController.OnReceivedLevelEffect += EffectController.AddEffect;
    }

    protected override Config GetConfigByType(Node node)
    {
        if (node is LevelController) return new LevelConfig(_levelConfig);
        return null;
    }
}