using Godot;
using System;
using HippieFall.Game;

public class StartGameByTap : Control
{
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch inputEvent)
        {
            if (inputEvent.Pressed)
            {
                HippieFallUtilities.Game.Level.StartGame();
                QueueFree();
            }
        }
    }
}
