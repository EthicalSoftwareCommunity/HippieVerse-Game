using System;
using System.Collections.Generic;
using Godot;
using System.Timers;
using Global.Data.EffectSystem;
using HippieFall.Effects;
using Timer = System.Timers.Timer;

public class IncreaseLevelSpeedByTapArea : Control
{
    [Export] private float timeToHoldTouch = 1f;
    public event Action<List<Effect>> OnEffectAdded; 
    private Timer _timer;
    private bool _timerIsStarted = false;
    private DynamicEffect increaseLevelSpeedTwice;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = new Timer(timeToHoldTouch * 1000);
        _timer.AutoReset = false;
        _timer.Elapsed += IncreaseLevelSpeed;
    }
    private void _on_Control_gui_input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch inputEvent)
        {
            if (inputEvent.Pressed)
            {
                GD.Print("Started");
               
                _timer.Start();
                
            }
            else if(inputEvent.Pressed == false)
            {
                GD.Print("Stopped");
                _timer.Stop();
                increaseLevelSpeedTwice.RemoveEffect();
            }
        }
      
    }

    private void IncreaseLevelSpeed(object sender, ElapsedEventArgs e)
    {
        GD.Print("Hold");
        increaseLevelSpeedTwice = new DynamicEffect(new ChangeLevelSpeed(2f, true), 9999);
        OnEffectAdded?.Invoke(new List<Effect>() { increaseLevelSpeedTwice });
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
