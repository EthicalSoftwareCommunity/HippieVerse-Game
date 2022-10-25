



using Global;
using Global.Constants;
using Godot;

namespace HippieUniverse
{
	class SlideDetector : Node
	{
		[Signal] public delegate void OnSlideStart(int type, Vector2 position);
		[Signal] public delegate void OnSliding(int type, Vector2 position);
		[Signal] public delegate void OnSlideReleased(int type, Vector2 position);

		[Export] private float _slideZone = 25f;

		private Vector2 _slideStartPosition;
		private Vector2 _slideCurrentPosition;
		private bool _isSliding = false;

		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventScreenDrag)
			{
				InputEventScreenDrag eventDrag = (InputEventScreenDrag)@event;
				_slideCurrentPosition = eventDrag.Position;
				if (_isSliding)
					Sliding(eventDrag.Relative);
				else if (GetSlideRelative() > _slideZone)
				{
					StartDetectionSlide();
				}
			}
			if (@event is InputEventScreenTouch)
			{
				InputEventScreenTouch eventTouch = (InputEventScreenTouch)@event;
				if (eventTouch.Pressed)
				{
					_slideStartPosition = eventTouch.Position;
				}

				if (eventTouch.Pressed == false && _isSliding)
				{
					_isSliding = false;
					EndDetectionSlide(eventTouch.Position);
					GD.Print("Not dragging");
				}
			}
		}

		private float GetSlideRelative()
		{
			return _slideStartPosition.DistanceTo(_slideCurrentPosition);
		}

		private void StartDetectionSlide()
		{
			_isSliding = true;
			EmitSignal(nameof(OnSlideStart), C_InputTouchEventType.SLIDE_START, _slideStartPosition);
			GD.Print("Slide Start");
		}

		private void Sliding(Vector2 position)
		{
			EmitSignal(nameof(OnSliding),C_InputTouchEventType.SLIDING, position);
		}

		private void EndDetectionSlide(Vector2 position)
		{
			_isSliding = false;
			EmitSignal(nameof(OnSlideReleased), C_InputTouchEventType.SLIDE_END, position);
		}
		
	}
}