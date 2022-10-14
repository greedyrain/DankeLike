using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageOverTime
{
    public void DamageOverTime(GameObject target);
    public void CancelDamageOverTime(GameObject target);
}
