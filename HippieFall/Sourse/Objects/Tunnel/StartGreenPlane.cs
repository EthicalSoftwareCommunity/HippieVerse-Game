using Godot;
using System.Timers;
using HippieFall.Game;
using Timer = System.Timers.Timer;

public class StartGreenPlane : CSGBox
{
    private LevelConfig _levelConfig;
    
    public override void _Ready()
    {
        Timer timer = new Timer(4000);
        timer.Elapsed += DestroyPlane;
        timer.Start();
        HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this); 
    }

    private void DestroyPlane(object sender, ElapsedEventArgs e)
    {
        QueueFree();   
    }

    private void Init()
    {
        _levelConfig = HippieFallUtilities.Game.Level.LevelConfig;
    }

    public override void _PhysicsProcess(float delta)
    {
        Translation += new Vector3(0, _levelConfig.Speed * delta, 0);
    }
}
