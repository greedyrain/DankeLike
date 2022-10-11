using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peojectile_Shotgun : MonoBehaviour
{
    Weapon weapon;
    public float buletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * buletSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetHurt(weapon.damage);

        }
    }

    public void Init(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
