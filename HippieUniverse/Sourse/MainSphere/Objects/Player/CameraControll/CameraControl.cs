using System;
using Global;
using Global.Constants;
using Godot;
using Array = Godot.Collections.Array;


namespace HippieUniverse
{
	class CameraControl : KinematicBody
	{
		[Export] private float sensitivity = 0.5f;
		[Export] private float smoothness = 0.5f;
		[Export] private int pitch_limit = 360;
		[Export] private int yaw_limit = 360;
		
		[Export] private float acceleration = 1.0f;
		[Export] private float deceleration = 0.0f;
		[Export] private Vector3 max_speed = new Vector3(1.0f, 1.0f, 1.0f);
		
		Vector2 _mouse_offset = new Vector2();

		private float _yaw = 0.0f;
		private float _pitch = 0.0f;
		private float _total_yaw = 0.0f;
		private float _total_pitch = 0.0f;

		private float _direction = 0;
		private Vector3 _speed = new Vector3(0.0f, 0.0f, 0.0f);

		private InputController inputController;
		private Camera camera;
		

		private const float ROTATION_MULTIPLIER = 0.01f;

		public override void _Ready()
		{
			inputController = (InputController)GetNode("InputController");
			camera = (Camera)GetNode("UniverseCamera");

			inputController.Connect("OnInputEventTypeActivated", this, "HandleIncomingInput");
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}



		private void HandleIncomingInput(int type, Vector2 position)
		{
			int inputType = type;
			switch (type)
			{
				case C_InputTouchEventType.TOUCH_DOWN: 
					break;
				case C_InputTouchEventType.TOUCH_UP:
					inputController.ResetInput();
					GD.Print("Touch to ...");
					break;
				case C_InputTouchEventType.HOLD_TOUCH_DOWN:
					GD.Print("HOLD Touch to ...");
					Node obj = GetMouseRaycastObject();
					if (obj != null)
					{
						InteractionController controller = null;
						if (obj.HasNode("InteractionController"))
							controller = obj.GetNode<InteractionController>("InteractionController");
						inputController.ResetInput();
						controller?.Interact(C_InputTouchEventType.HOLD_TOUCH_DOWN);
					}
					break;
				case C_InputTouchEventType.HOLD_TOUCH_UP:
					inputController.ResetInput();
					break;
				case C_InputTouchEventType.SLIDE_START:
					inputController.ResetInput();
					break;
				case C_InputTouchEventType.SLIDING:
					_mouse_offset = position;
					break;
				case C_InputTouchEventType.SLIDE_END:
					inputController.ResetInput();
					break;
				case C_InputTouchEventType.ZOOM_IN:
					_direction = 1;
					break;
				case C_InputTouchEventType.ZOOM_OUT:
					_direction = -1;
					break;
				case C_InputTouchEventType.ZOOM_OFF:
					_direction = 0;
					break;
			}
		}

		public override void _PhysicsProcess(float delta)
		{
			UpdateMovement(delta);
			UpdateRotation(delta);
		}

		private void UpdateMovement(float delta)
		{
			_speed = max_speed * acceleration * Transform.basis.z * -1 * _direction * delta;
			MoveAndSlide(_speed);
		}

		private void UpdateRotation(float delta)
		{
			Vector2 offset = new Vector2();

			offset = _mouse_offset * sensitivity;

			_mouse_offset = new Vector2();

			
			_yaw = (float)(_yaw * smoothness + offset.x * (1.0 - smoothness));
			_pitch = (float)(_pitch * smoothness + offset.y * (1.0 - smoothness));

			if (yaw_limit < 360)
				_yaw = Mathf.Clamp(_yaw, -yaw_limit - _total_yaw, yaw_limit - _total_yaw);
			if (pitch_limit < 360)
				_pitch = Mathf.Clamp(_pitch, -pitch_limit - _total_pitch, pitch_limit - _total_pitch);

			_total_yaw += _yaw;
			_total_pitch += _pitch;
			
			RotateY(Mathf.Deg2Rad(-_yaw));
			RotateObjectLocal(new Vector3(1, 0, 0), Mathf.Deg2Rad(-_pitch));
		}

		private void SetSmoothness(float value)
		{
			smoothness = Mathf.Clamp(value, 0.001f, 0.999f);
		}

		private bool CheckTouchInteractAbility()
		{
			Node obj = GetMouseRaycastObject();
			var controller = obj.GetNode("InteractionController");
			return controller == null;
		}

		private Node GetMouseRaycastObject()
		{
			var spaceState = GetWorld().DirectSpaceState;
			var mousePosition = GetViewport().GetMousePosition();
			var rayOrigin = camera.ProjectRayOrigin(mousePosition);
			var rayEnd = rayOrigin + camera.ProjectRayNormal(mousePosition) * 2000;
			var results = spaceState.IntersectRay(rayOrigin, rayEnd, new Array() { this });

			Node result = new Node();
			if (results.Count > 0)
			{
				result = (Node)results["collider"];
				GD.Print(result);
			}
			return result;
		}
	}
}

