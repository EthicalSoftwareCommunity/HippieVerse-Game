using Godot;
using System;
using Global;

public class SettingsInteract : InteractionController
{
    
    public override void HoldTouchInteract()
    {
        base.HoldTouchInteract();
        MainSphereUtilites.MainSphere.Settings.OpenSettings(true);    
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
