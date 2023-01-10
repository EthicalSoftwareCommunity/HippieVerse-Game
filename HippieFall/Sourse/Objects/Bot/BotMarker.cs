using Godot;
using System;
using System.Collections.Generic;
using System.Timers;
using Global;
using HippieFall.Game;
using Timer = System.Timers.Timer;

public class BotMarker : Spatial
{
    [Export] private List<NodePath> possibleMarkers;

    public Spatial Marker;
    private Vector3 StartPosition;
    public override void _Ready()
    {
        Marker = GetNode<Spatial>(possibleMarkers[Utilities.GetRandomNumberInt(0, possibleMarkers.Count - 1)]);
        HippieFallUtilities.Game.Bot.BotMarkersController.AddNewMarker(this);
        Visible = false;
        Marker.Translation += new Vector3(Utilities.GetRandomNumberFloat(0, 0.3f),
            Utilities.GetRandomNumberFloat(0, 0.3f), Utilities.GetRandomNumberFloat(0, 0.3f));
        StartPosition = Marker.Translation;
        Timer timer = new Timer(500);
        timer.Elapsed += ChangeMarkerPosition;
        timer.Start();

    }

    private void ChangeMarkerPosition(object sender, ElapsedEventArgs e)
    {
        Translation = StartPosition + new Vector3(Utilities.GetRandomNumberFloat(-1f, 1f),
            Utilities.GetRandomNumberFloat(-1f, 1f), Utilities.GetRandomNumberFloat(-1f, 1f));
    }
    
}
