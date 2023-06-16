using InControl;

namespace Sensen.Components
{
    public class PlayerActionsSetSegmented<T>
    where T : PlayerActionSet
    {
        public T KeyboardActions { get; }
        public T ControllerActions { get; }
        public bool IsKeyboard { get; private set; }
        public bool IsController => !IsKeyboard;
        public T Actions => IsKeyboard ? KeyboardActions : ControllerActions;

        public bool IsDestroyed { get; private set; }

        public PlayerActionsSetSegmented(T keyboardActions, T controllerActions)
        {
            KeyboardActions = keyboardActions;
            ControllerActions = controllerActions;
            ListenOptionsSetup.ToKeyboardOnly(KeyboardActions.ListenOptions);
            ListenOptionsSetup.ToControllerOnly(ControllerActions.ListenOptions);
        }

        public void SwitchToKeyboard()
        {
            IsKeyboard = true;
        }

        public void SwitchToController()
        {
            IsKeyboard = false;
        }

        public void Destroy()
        {
            KeyboardActions.Destroy();
            ControllerActions.Destroy();
            IsDestroyed = true;
        }
    }
}
