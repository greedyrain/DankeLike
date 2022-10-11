using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public int ID;
    [HideInInspector] public string description;
    [HideInInspector] public int damage;
    [HideInInspector] public float atkCD;
    [HideInInspector] public float remainCD;

    WeaponData data;
    PlayerController player;
    PlayerInput playerInput;

    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playerInput = player.input;
        InitData();

    }

    private void OnEnable()
    {
        playerInput.onMove += SetWeaponDirection;
    }

    private void OnDisable()
    {
        playerInput.onMove -= SetWeaponDirection;
    }

    public void InitData()
    {


        data = GameDataManager.Instance.Weapons[ID - 1];
        description = data.description;
        damage = data.damage;
        atkCD = data.atkCD;
        remainCD = data.remainCD;
    }

    public void SetWeaponDirection(Vector2 dir)
    {
        transform.rotation.SetLookRotation(dir);
    }
}
