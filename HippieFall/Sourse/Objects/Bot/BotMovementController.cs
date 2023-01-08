using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Game;

namespace HippieFall.Bot;

public class BotMovementController :  Node, IEffectable
{
    private Bot _bot;
    public Vector3 Move = new Vector3(0,0,0);
    private MovementConfig _config = new MovementConfig();
    public Vector3 NextMove = new Vector3(64,64, 0);
    public override void _Ready()
    {
        SetPhysicsProcess(false);
        HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
    }

    private void Init()
    {
        _bot = HippieFallUtilities.Game.Bot;
        SetPhysicsProcess(true);
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 move2d = new Vector2(NextMove.x, NextMove.y);
        if (move2d.Length() > 0)
        {
            Move *= +_config.Speed * delta * GetMovementCoefficient(move2d.Normalized().Length());
            _bot.MoveAndCollide(Move);
        }
    }

    private float GetMovementCoefficient(float x)
    {
        return Mathf.Log(x*5) / 200;
    }
    public void ChangeConfigData(Config config)
    {
        
    }
}