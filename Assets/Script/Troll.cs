using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyAI
{
    enemyStates trollState = enemyStates.Idle;

    public Troll(Transform trollObj)
    {
        base.enemyObj = trollObj;
    }
    void Start()
    {
        health = 250;
    }
    float restTimer;
    float attackTimer;
    public override void UpdateEnemy(Transform playerObj, Transform treasureChest, List<Transform> dwarfTransform)
    {
        float distance = (base.enemyObj.position - playerObj.position).magnitude;
        float distanceToTreasure = (base.enemyObj.position - treasureChest.position).magnitude;

        restTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        Transform GetClosestEnemy(List<Transform> allDwarfsTransform)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = enemyObj.transform.position;
            foreach (Transform potentialTarget in allDwarfsTransform)
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
        float distanceToDwarfs = (base.enemyObj.transform.position - GetClosestEnemy(dwarfTransform).position).magnitude;

        switch (trollState)
        {
            case enemyStates.Idle:
                if (distanceToDwarfs < 10)
                {
                    trollState = enemyStates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    trollState = enemyStates.moveTowardsChest;
                }
                if (restTimer > 3)
                {
                    trollState = enemyStates.Patrol;
                }
                break;

            case enemyStates.Patrol:
                if (restTimer > 6)
                {
                    trollState = enemyStates.Idle;
                    restTimer = 0;
                }
                if (distanceToDwarfs < 10)
                {
                    trollState = enemyStates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    trollState = enemyStates.moveTowardsChest;
                }
                break;

            case enemyStates.ChargeToAttack:
                if (distanceToDwarfs >= 15 && distanceToTreasure >= 15)
                {
                    trollState = enemyStates.Idle;
                }
                if (distanceToTreasure < 1.5)
                {
                    trollState = enemyStates.Steal;
                }
                if (distanceToDwarfs < 1)
                {
                    attackTimer = 0;
                    trollState = enemyStates.DoAttack;

                }
                break;

            case enemyStates.DoAttack:
                //Do the attack
                if (attackTimer > 0.5)
                {
                    trollState = enemyStates.ChargeToAttack;
                }
                break;

            case enemyStates.moveTowardsChest:
                if (distance < 6)
                {
                    trollState = enemyStates.ChargeToAttack;
                }
                if (distanceToTreasure < 1.5)
                {
                    trollState = enemyStates.Steal;
                }
                break;
            case enemyStates.Steal:
                trollState = enemyStates.Steal;
                if (carryGold == true)
                {
                    trollState = enemyStates.Flee;
                }
                break;
            case enemyStates.Flee:
                if (distance < 10)
                {
                    trollState = enemyStates.Flee;
                }
                break;
            default:
                break;
        }
        UpdateState(playerObj, treasureChest, trollState, dwarfTransform);
    }
}