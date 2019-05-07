using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum trollMajorStates
{

}
public enum trollStates
{

}
public enum dwarfMajorStates
{
    FollowMode,
    DefenceMode,
}
public enum dwarfStates
{
    Idle,
    Move,
    Attack,
    TakeDamage,
    Die,
}
public class Unit : MonoBehaviour, IEUnit
{
    protected byte health;
    protected float distanceToEnemy;
    protected float distanceToTreasure;
    protected NavMeshAgent unitAgent;
    protected Slider hpSlider;
    protected bool isDead;
    protected Animator unitAnimator;

    public void ApplyDamage(byte Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Attack()
    {

    }

    public void Dead()
    {
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponentInChildren<Canvas>());
        isDead = true;
    }

    public Transform GetClosestEnemy(List<Transform> allEnemyTranform)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;
        foreach (Transform potentialTarget in allEnemyTranform)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }


}