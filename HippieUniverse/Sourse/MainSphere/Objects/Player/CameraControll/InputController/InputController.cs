using Godot;
using Global;
using Global.Constants;

namespace HippieUniverse
{
	public class InputController:Node
	{
		[Signal]
		private delegate void OnInputEventTypeActivated(int type, Vector2 position);

		private int _currentInputType = 0;
		private SlideDetector _slideDetector;
		private TouchDetector _touchDetector;
		private MultiTouchDetector _multiTouchDetector;

		public override void _Ready()
		{
			_slideDetector = (SlideDetector)GetNode("SlideDetector");
			_touchDetector = (TouchDetector)GetNode("TouchDetector");
			_multiTouchDetector = (MultiTouchDetector)GetNode("MultiTouchDetector");
			
			_slideDetector.Connect("OnSlideStart", this, nameof(SetIncomingInputType));
			_slideDetector.Connect("OnSliding", this, nameof(SetIncomingInputType));
			_touchDetector.Connect("OnTouchedDown", this, nameof(SetIncomingInputType));
			_touchDetector.Connect("OnTouchedUp", this, nameof(SetIncomingInputType));
			_touchDetector.Connect("OnHoldingTouched", this, nameof(SetIncomingInputType));
			_touchDetector.Connect("OnHoldingTouching", this, nameof(SetIncomingInputType));
			_touchDetector.Connect("OnHoldingReleased", this, nameof(SetIncomingInputType));
			_slideDetector.Connect("OnSlideReleased", this, nameof(SetIncomingInputType));
			_multiTouchDetector.Connect("OnZoomIn", this, nameof(SetIncomingInputType));
			_multiTouchDetector.Connect("OnZoomOut", this, nameof(SetIncomingInputType));
			_multiTouchDetector.Connect("OnMultiTouchStart", this, nameof(SetIncomingInputType));
			_multiTouchDetector.Connect("OnMultiTouchEnd", this, nameof(SetIncomingInputType));
		}

		private void SetIncomingInputType(int type, Vector2 position)
		{
			if (_currentInputType  == C_InputTouchEventType.MULTI_TOUCH_START || type == C_InputTouchEventType.MULTI_TOUCH_START)
			{
				if (_currentInputType != C_InputTouchEventType.MULTI_TOUCH_START)
					_currentInputType = type;
				if (type == C_InputTouchEventType.ZOOM_IN)
				{
					EmitSignal(nameof(OnInputEventTypeActivated), C_InputTouchEventType.ZOOM_IN, position);
					GD.Print("Zoom In");
				}
				else if (type == C_InputTouchEventType.ZOOM_OUT)
				{
					EmitSignal(nameof(OnInputEventTypeActivated), C_InputTouchEventType.ZOOM_OUT, position);
					GD.Print("Zoom Out");
				}
				else if (type == C_InputTouchEventType.MULTI_TOUCH_END)
				{
					GD.Print("OFF Zoom");
					EmitSignal(nameof(OnInputEventTypeActivated), C_InputTouchEventType.ZOOM_OFF, position);
					ResetInput();
				}
				return;
			}

			if (_currentInputType == C_InputTouchEventType.SLIDE_START || type == C_InputTouchEventType.SLIDE_START)
			{
				if (_currentInputType != C_InputTouchEventType.SLIDE_START)
				{
					_currentInputType = C_InputTouchEventType.SLIDE_START;
				}
				else if (type== C_InputTouchEventType.SLIDING)
				{
					EmitSignal(nameof(OnInputEventTypeActivated), C_InputTouchEventType.SLIDING, position);
					GD.Print("Sliding");
				}
				else if (type == C_InputTouchEventType.SLIDE_END)
				{
					GD.Print("OFF Sliding");
					ResetInput();
				}
				return;
			}

			if (_currentInputType == C_InputTouchEventType.NONE)
			{
				GD.Print(type);
				_currentInputType = type;
				EmitSignal(nameof(OnInputEventTypeActivated), _currentInputType, position); 
				ResetInput();
			}
		}
		
		public void ResetInput()
		{
			_currentInputType = C_InputTouchEventType.NONE;
		}
	}
}
