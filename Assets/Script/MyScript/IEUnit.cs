using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEUnit
{
    void UpdateState();
    void GetClosestEnemy();
    void ApplyDamage();
    void Dead();
    void Attack();

}
