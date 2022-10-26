using System.Collections.Generic;
using Godot;
using Global;
using Global.Constants;


namespace HippieUniverse
{
	class MultiTouchDetector : Node
	{
		
		[Signal] public delegate void OnMultiTouchEnd(int type, Vector2 none);
		[Signal] public delegate void OnMultiTouchStart(int type, Vector2 none);
		[Signal] public delegate void OnZoomIn(int type, Vector2 none);
		[Signal] public delegate void OnZoomOut(int type, Vector2 none);

		private enum ZoomState {ZoomIn, ZoomOut, ZoomOff}

		[Export] private float distanseOffset = 10;
		const int MAX_POINTS = 2;
		
		private class Point
		{
			public bool State;
			public Vector2 Position;
		}

		private List<Point> _points = new List<Point>();
		private ZoomState _state = ZoomState.ZoomOff;
		private float _lastDistantion = 0;
		private float _currentDist = 0;
		private bool _zoomStarted = false;
		private int _countStates = 0;
		public override void _Ready()
		{
			for (int i = 0; i < MAX_POINTS; i++)
			{
				_points.Add(new Point()
				{
					Position = new Vector2(),
					State = false
				});
				SetProcessInput(true);
			}
		}
		
		public override void _Input(InputEvent @event)
		{
			_countStates = 0;
			
			if (@event is InputEventScreenTouch)
			{
				InputEventScreenTouch eventTouch = (InputEventScreenTouch)@event;
				if (eventTouch.Index < MAX_POINTS)
				{
					_points[eventTouch.Index].State = eventTouch.Pressed;
					_points[eventTouch.Index].Position = eventTouch.Position;
					if (eventTouch.Pressed)
					{
						if (_zoomStarted && _countStates == 0)
						{
							GD.Print("Not Multy");
							EmitSignal(nameof(OnMultiTouchEnd), C_InputTouchEventType.MULTI_TOUCH_END, new Vector2());
							_zoomStarted = false;
							_state = ZoomState.ZoomOff;
						}
					}
				}
			}

			for (int i = 0; i < MAX_POINTS; i++)
			{
				if (_points[i].State)
					_countStates += 1;
			}
			
			if (@event is InputEventScreenDrag)
			{
				InputEventScreenDrag eventDrag = (InputEventScreenDrag)@event;
				if (eventDrag.Index < MAX_POINTS)
				{
					_points[eventDrag.Index].Position = eventDrag.Position;
					if (_countStates >= 2)
					{
						if (_zoomStarted == false)
						{
							EmitSignal(nameof(OnMultiTouchStart), C_InputTouchEventType.MULTI_TOUCH_START, new Vector2());
							GD.Print("MULTI");
							_zoomStarted = true;
						}

						_currentDist = _points[0].Position.DistanceTo(_points[1].Position);
						if (_currentDist + distanseOffset < _lastDistantion && _state != ZoomState.ZoomOut)
						{
							EmitSignal(nameof(OnZoomOut), C_InputTouchEventType.ZOOM_OUT, new Vector2());
							_state = ZoomState.ZoomOut;
						}
						else if (_currentDist - distanseOffset > _lastDistantion && _state != ZoomState.ZoomIn)
						{
							EmitSignal(nameof(OnZoomIn), C_InputTouchEventType.ZOOM_IN, new Vector2());
							_state = ZoomState.ZoomIn;
						}

						_lastDistantion = _currentDist;
					}
				}
			}
		}
	}
}
	
	
