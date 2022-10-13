using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="PlayerInput", menuName = "ScriptableObject/PlayerInput")]
public class PlayerInput : ScriptableObject, DankeLikeInputAction.IGamePlayActions
{
    public event UnityAction<Vector2> onMove;
    public event UnityAction onStopMove;

    public DankeLikeInputAction inputAction;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            onMove?.Invoke(context.ReadValue<Vector2>());

        if (context.phase == InputActionPhase.Canceled)
            onStopMove?.Invoke();
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
