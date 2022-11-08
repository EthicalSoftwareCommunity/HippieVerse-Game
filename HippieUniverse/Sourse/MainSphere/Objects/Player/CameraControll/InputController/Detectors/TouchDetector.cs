using Global;
using Global.Constants;
using Godot;

namespace HippieUniverse
{
	class TouchDetector : Node
	{
		[Signal] public delegate void OnTouchedDown(int type, Vector2 position);
		[Signal] public delegate void OnTouchedUp(int type, Vector2 position);
		[Signal] public delegate void OnHoldingTouched(int type, Vector2 position);
		[Signal] public delegate void OnHoldingTouching(int type, Vector2 position);
		[Signal] public delegate void OnHoldingReleased(int type, Vector2 position);

		private Vector2 _position = new Vector2();
		private bool _isTouched = false;
		private bool _isHoldTouched = false;
		private Timer _timer;
		public override void _Ready()
		{
			_timer = (Timer)GetNode("Timer");
		}

		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventScreenTouch)
			{
				if (@event.IsPressed())
				{
					if (_isTouched == false)
					{
						_position = ((InputEventScreenTouch)@event).Position;
						EmitSignal(nameof(OnTouchedDown), C_InputTouchEventType.TOUCH_DOWN, _position);
						_timer.Start();
						_isTouched = true;
					}
				}
				else if (@event.IsPressed() == false)
				{
					_position = ((InputEventScreenTouch)@event).Position;
					if (_isTouched)
						EmitSignal(nameof(OnTouchedUp), C_InputTouchEventType.TOUCH_UP, _position);
					else if (_isHoldTouched)
					{
						EmitSignal(nameof(OnHoldingReleased), C_InputTouchEventType.HOLD_TOUCH_UP, _position);
						_isTouched = false;
						_isHoldTouched = false;
					}
				}
			}
		}

		public void OnTimerTimeout()
		{
			_isHoldTouched = true;
			if (_isTouched)
			{
				EmitSignal(nameof(OnHoldingTouched), C_InputTouchEventType.HOLD_TOUCH_DOWN, _position);
				_isTouched = false;
			}
		}
	}
}
