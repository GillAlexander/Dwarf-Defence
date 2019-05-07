using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEUnit
{
    //void UpdateState(Transform treasureChest, List<Transform> enemyTransform);
    Transform GetClosestEnemy(List<Transform> allEnemyTranform);
    void ApplyDamage(byte Damage);
    void Dead();
    void Attack();
}
