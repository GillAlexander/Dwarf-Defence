using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGoblin : EnemyAI
{
    enemyStates goblinState = enemyStates.Idle;


    public OldGoblin(Transform goblinObj)
    {
        base.enemyObj = goblinObj;
    }

    void Start()
    {
        health = 100;
    }
    bool gold = false;
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

        switch (goblinState)
        {
            case enemyStates.Idle:
                if (distanceToDwarfs < 10)
                {
                    goblinState = enemyStates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    goblinState = enemyStates.moveTowardsChest;
                }
                if (restTimer > 3)
                {
                    goblinState = enemyStates.Patrol;
                }

                break;

            case enemyStates.Patrol:
                if (restTimer > 6)
                {
                    goblinState = enemyStates.Idle;
                    restTimer = 0;
                }
                if (distanceToDwarfs < 10)
                {
                    goblinState = enemyStates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    goblinState = enemyStates.moveTowardsChest;
                }
                break;

            case enemyStates.ChargeToAttack:
                if (distanceToDwarfs >= 15 && distanceToTreasure >= 15)
                {
                    goblinState = enemyStates.Idle;
                }
                if (distanceToTreasure < 1.5)
                {
                    goblinState = enemyStates.Steal;
                }
                if (distanceToDwarfs < 1)
                {
                    attackTimer = 0;
                    goblinState = enemyStates.DoAttack;

                }
                
                break;

            case enemyStates.DoAttack:
                //Do the attack
                if (attackTimer > 0.5)
                {
                    goblinState = enemyStates.ChargeToAttack;
                }

                
                break;

            case enemyStates.moveTowardsChest:
                if (distanceToDwarfs < 6)
                {
                    goblinState = enemyStates.ChargeToAttack;
                }
                if (distanceToTreasure < 1.5)
                {
                    goblinState = enemyStates.Steal;
                }
                break;

            case enemyStates.Steal:
                if (carryGold == true)
                {
                    goblinState = enemyStates.Flee;
                }
                break;

            case enemyStates.Flee:
                goblinState = enemyStates.Flee;
                break;

            default:
                break;

        }
        UpdateState(playerObj, treasureChest, goblinState, dwarfTransform);
    }

}