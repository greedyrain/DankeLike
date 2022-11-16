using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoyStickPanel : BasePanel
{
    public Image touchArea;
    public GameObject joyStick;
    public GameObject joyStickController;
    public PlayerController player;
    public Vector2 direction;
    public event UnityAction<Vector2> OnDrag;
    public override void Init()
    {
        Show();
        player = FindObjectOfType<PlayerController>();
        EventTrigger touchAreaEventTrigger = touchArea.GetComponent<EventTrigger>();
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener(PointerDown);
        
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener(PointerUp);
        
        EventTrigger.Entry entryDrag = new EventTrigger.Entry();
        entryDrag.eventID = EventTriggerType.Drag;
        entryDrag.callback.AddListener(PointerDrag);
        
        touchAreaEventTrigger.triggers.Add(entryDown);
        touchAreaEventTrigger.triggers.Add(entryUp);
        touchAreaEventTrigger.triggers.Add(entryDrag);
    }

    private void LateUpdate()
    {
        if (direction != Vector2.zero)
        {
            player.Move(direction);
        }
    }

    private void PointerDown(BaseEventData data)
    {
        joyStick.SetActive(true);
        joyStick.transform.position = Mouse.current.position.ReadValue();
        joyStickController.transform.localPosition = Vector3.zero;
    }
    
    private void PointerUp(BaseEventData data)
    {
        joyStick.SetActive(false);
        direction = Vector2.zero;
    }
    
    private void PointerDrag(BaseEventData data)
    {
        joyStickController.transform.position = Mouse.current.position.ReadValue();
        if ((joyStickController.transform.position - joyStick.transform.position).magnitude > 50)
        {
            joyStickController.transform.localPosition =
                (joyStickController.transform.position - joyStick.transform.position).normalized * 50;
        }
        direction = joyStickController.transform.position - joyStick.transform.position;
        // player.weapon.SetWeaponDirection(joyStickController.transform.position - joyStick.transform.position);
        // player.direction = joyStickController.transform.position - joyStick.transform.position;
        // onDrag?.Invoke(joyStickController.transform.position - joyStick.transform.position);
    }
    
    
}
