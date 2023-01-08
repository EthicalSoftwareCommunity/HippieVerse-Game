using Global.Data;
using Global.Data.EffectSystem;
using Global.GameSystem;
using Godot;
using HippieFall.Game;

namespace HippieFall.Bot;

public class BotEffectController :  ObjectEffectController
{
    public BotEffectController(Effect.EffectsTarget effectsTarget) : base(effectsTarget)
    {
    }

    public override void Init(Node node = null, Config config = null)
    {
        AddNode(node, (BotConfig)config);
        HippieFallUtilities.Game.GameEffectController.OnReceivedPlayerEffect += EffectController.AddEffect;
    }
}