using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HippieFall.Game;

namespace HippieFall.Bot
{
    public class BotMarkersController : Node
    {
        public List<BotMarker> Markers = new List<BotMarker>();
        private BotMarker _currentMarker;
        private Vector3  markerPosition = new Vector3(0, 0, 0);
        private Vector3 targetPosition;

        public override void _PhysicsProcess(float delta)
        {
            CalculateMoveParametersToMarker();
        }

        private void CalculateMoveParametersToMarker()
        {
            if (_currentMarker == null)
            {
                markerPosition = new Vector3(0, 0, 0);
                if (Markers.Count != 0)
                {
                    _currentMarker = Markers[0];
                }
            }
            else
            {
                markerPosition = _currentMarker.Marker.GlobalTranslation;
            }
            
            targetPosition = new Vector3(HippieFallUtilities.Game.Bot.GlobalTranslation);
            markerPosition.y = 0;
            targetPosition.y = 0;
            HippieFallUtilities.Game.Bot.BotMovementController.Move = targetPosition.DirectionTo(markerPosition).Normalized() * Mathf.Clamp(Mathf.Sqrt(targetPosition.DistanceTo(markerPosition))*64, 0.5f, 1000);
            
            if(_currentMarker!=null)
                if(HippieFallUtilities.Game.Bot.GlobalTranslation.y < _currentMarker?.Marker.GlobalTranslation.y)
                    SetNextMarker();
        }
        public void AddNewMarker(BotMarker marker)
        {
            Markers.Add(marker);
        }
        private void SetNextMarker()
        {
            if (Markers.Count != 0)
            {
                _currentMarker = Markers[0];
                Markers.RemoveAt(0);
            }
            else
            {
                _currentMarker = null;
            }
        }
    }
}