using Godot;

namespace HippieFall
{
    public class Joystic : TouchScreenButton
    {
        private readonly int _boundary = 64;
        private readonly Vector2 _radius = new Vector2(28, 28);
        private readonly float _returnAccel = 20f;
        private readonly float _threshold = 2;
        private int _onGoingDrag = -1;

        public override void _Process(float delta)
        {
            if (_onGoingDrag == -1)
            {
                Vector2 differencePosition = new Vector2(0, 0) - _radius - Position;
                Position += differencePosition * _returnAccel * delta;
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventScreenDrag)
            {
                InputEventScreenDrag eventDrag = (InputEventScreenDrag)@event;
                Vector2 parentGlobalPosition = new Vector2(GetParent<Node2D>().Position.x,
                    GetParent<Node2D>().Position.y);
                float eventDistantionFromCenter = (eventDrag.Position - parentGlobalPosition).Length();

                if (eventDistantionFromCenter >= _boundary * GlobalScale.x || eventDrag.Index == _onGoingDrag)
                {
                    GlobalPosition = eventDrag.Position - _radius * GlobalScale;

                    if (GetButtonPosition().Length() > _boundary) 
                        Position = GetButtonPosition().Normalized() * _boundary - _radius;
                    _onGoingDrag = eventDrag.Index;
                }
            }

            if (@event is InputEventScreenTouch && !@event.IsPressed() &&
                ((InputEventScreenTouch)@event).Index == _onGoingDrag) 
                _onGoingDrag = -1;
        }
        
        private Vector2 GetValue()
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