using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="PlayerInput", menuName = "ScriptableObject/PlayerInput")]
public class PlayerInput : ScriptableObject, DankeLikeInputAction.IGamePlayActions
{
    public event UnityAction<Vector2> onMove;
    public event UnityAction onStopMove;
    public event UnityAction onSkill;
    public event UnityAction onSkillCancel;
    public event UnityAction onGetHurt;

    public DankeLikeInputAction inputAction;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            onMove?.Invoke(context.ReadValue<Vector2>());

        if (context.phase == InputActionPhase.Canceled)
            onStopMove?.Invoke();
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && Keyboard.current.jKey.wasPressedThisFrame)
            onSkill?.Invoke();
    }

    public void OnGetHurt(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            onGetHurt?.Invoke();
    }

    private void OnEnable()
    {
        inputAction = new DankeLikeInputAction();

        inputAction.GamePlay.SetCallbacks(this);
        EnableGamePlayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void EnableGamePlayInput()
    {
        inputAction.GamePlay.Enable();
    }

    public void DisableAllInput()
    {
        inputAction.GamePlay.Disable();
    }
    
    
}
