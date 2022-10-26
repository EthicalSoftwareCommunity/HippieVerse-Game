using Global.Constants;
using Godot;
namespace Global
{
    public abstract class InteractionController : Node
    {
        public virtual void TouchInteract()
        {
        }

        public virtual void HoldTouchInteract()
        {
        }

        public virtual void SlideTouchInteract()
        {
        }

        public virtual void Interact(int inputType)
        {
            switch (inputType)
            {
                case C_InputTouchEventType.HOLD_TOUCH_DOWN: HoldTouchInteract(); break;
                case C_InputTouchEventType.TOUCH_UP: TouchInteract(); break;
            }
        }
    }
}