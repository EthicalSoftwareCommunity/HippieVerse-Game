using Godot;
using System;
using System.Collections.Generic;
using Global;
using HippieFall.Game;

public class BotMarker : Spatial
{
    [Export] private List<NodePath> possibleMarkers;

    public Spatial Marker;
    public override void _Ready()
    {
        Marker = GetNode<Spatial>(possibleMarkers[Utilities.GetRandomNumberInt(0, possibleMarkers.Count - 1)]);
        HippieFallUtilities.Game.Bot.BotMarkersController.AddNewMarker(this);
        Visible = false;
    }
}
