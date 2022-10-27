using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public int ID;
    public int level;
    [HideInInspector] public string description;
    [HideInInspector] public int damage;
    [HideInInspector] public float atkCD;
    [HideInInspector] public float remainCD;

    public WeaponData weaponData;
    protected PlayerController player;
    protected PlayerInput playerInput;

    private Vector3 weaponDir;

    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playerInput = player.input;
        InitData();
    }

    protected virtual void OnEnable()
    {
        playerInput.onMove += SetWeaponDirection;
    }

    protected virtual void OnDisable()
    {
        playerInput.onMove -= SetWeaponDirection;
    }

    public void InitData()
    {
        for (int i = 0; i < GameDataManager.Instance.Weapons.Count; i++)
        {
            if (GameDataManager.Instance.Weapons[i].ID == ID && GameDataManager.Instance.Weapons[i].level == level)
                weaponData = GameDataManager.Instance.Weapons[i];
        }

        damage = weaponData.damage;
        atkCD = weaponData.atkCD;
        description = weaponData.description;
    }

    public void SetWeaponDirection(Vector2 dir)
    {
        transform.right = dir;
    }
}
