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
    public event Action<NamedEffect> OnEffectAdded; 
    private Timer _timer;
    private bool _timerIsStarted = false;
    private LongTapedOnDisplay _longTapedOnDisplay;

    private int touchIndex;
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
                touchIndex = inputEvent.Index;
                _timer.Start();
                
            }
            else if(inputEvent.Pressed == false)
            {
                ReduceEffect();
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch inputEvent)
        {
            if (inputEvent.Pressed == false)
            {
                if(touchIndex == inputEvent.Index)
                    ReduceEffect();
            }
        }
    }

    private void ReduceEffect()
    {
        GD.Print("Stopped");
        _timer.Stop();
        if(_longTapedOnDisplay!=null)
            _longTapedOnDisplay.RemoveDynamicsEffects();
        touchIndex = -1;
    }
    private void IncreaseLevelSpeed(object sender, ElapsedEventArgs e)
    {
        GD.Print("Hold");
        _longTapedOnDisplay = new LongTapedOnDisplay();
        OnEffectAdded?.Invoke(_longTapedOnDisplay);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
