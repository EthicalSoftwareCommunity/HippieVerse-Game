using Godot;

namespace HippieFall
{
    public class Joystic : TouchScreenButton
    {
        public readonly int Boundary = 64;
        private readonly Vector2 _radius = new Vector2(28, 28);
        private readonly float _returnAccel = 20f;
        private readonly float _threshold = 5;
        private int _onGoingDrag = -1;

        private bool _canDraging = false;

        public override void _Process(float delta)
        {
            if (_onGoingDrag == -1)
            {
                Vector2 differencePosition = new Vector2(0, 0) - _radius - Position;
                Position += differencePosition * _returnAccel * delta;
            }
        }

        private void _on_Control_gui_input(InputEvent @event)
        {
            if (@event is InputEventScreenTouch inputEvent)
            {
                GD.Print("GUI");
                if (inputEvent.Pressed)
                    _canDraging = true;
            }
        }
        public override void _Input(InputEvent @event)
        {
            if (_canDraging)
            {
                if (@event is InputEventScreenDrag)
                {
                    InputEventScreenDrag eventDrag = (InputEventScreenDrag)@event;
                    if(eventDrag.Index!=_onGoingDrag && _onGoingDrag != -1) return;
                    Vector2 parentGlobalPosition = new Vector2(GetParent<Node2D>().Position.x,
                        GetParent<Node2D>().Position.y);
                    float eventDistantionFromCenter = (eventDrag.Position - parentGlobalPosition).Length();

                    if (eventDistantionFromCenter >= Boundary * GlobalScale.x || eventDrag.Index == _onGoingDrag)
                    {
                        GlobalPosition = eventDrag.Position - _radius * GlobalScale;

                        if (GetButtonPosition().Length() > Boundary)
                            Position = GetButtonPosition().Normalized() * Boundary - _radius;
                        _onGoingDrag = eventDrag.Index;
                    }
                }

                if (@event is InputEventScreenTouch && !@event.IsPressed() &&
                    ((InputEventScreenTouch)@event).Index == _onGoingDrag)
                {
                    _onGoingDrag = -1;
                    _canDraging = false;
                    Position = Vector2.Zero;
                }
            }
        }

        public Vector2 GetValue()
        {
            if (GetButtonPosition().Length() > _threshold)
                return GetButtonPosition().Normalized();
            return new Vector2(0, 0);
        }
        
        public Vector2 GetButtonPosition()
        {
            return Position + _radius;
        }
    }
}