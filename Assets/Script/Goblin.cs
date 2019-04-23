using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyAI {
    enemyStates goblinState = enemyStates.Idle;


    public Goblin(Transform goblinObj) {
        base.enemyObj = goblinObj;
    }

    void Start() {
        health = 100;
    }
    bool gold = false;
    float restTimer;
    public override void UpdateEnemy(Transform playerObj, Transform treasureChest, List<Transform> dwarfTransform) {
        float distance = (base.enemyObj.position - playerObj.position).magnitude;
        float distanceToTreasure = (base.enemyObj.position - treasureChest.position).magnitude;
        
        restTimer += Time.deltaTime;


        Transform GetClosestEnemy(List<Transform> allDwarfsTransform) {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = enemyObj.transform.position;
            foreach (Transform potentialTarget in allDwarfsTransform) {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr) {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            return bestTarget;
        }
        float distanceToDwarfs = (base.enemyObj.transform.position - GetClosestEnemy(dwarfTransform).position).magnitude;

        switch (goblinState) {
            case enemyStates.Idle:

                goblinState = enemyStates.Idle;
                
                if (distanceToDwarfs < 10) {
                    goblinState = enemyStates.Attack;
                }
                if (distanceToTreasure < 15) {
                    goblinState = enemyStates.moveTowardsChest;
                }
                //if (restTimer > 1) {
                //    goblinState = enemyStates.Patrol;
                //}

                break;

            case enemyStates.Patrol:

                goblinState = enemyStates.Patrol;

                //if (restTimer > 2) {
                //    goblinState = enemyStates.Idle;
                //    restTimer = 0;
                //}
                if (distanceToTreasure < 1) {
                    goblinState = enemyStates.moveTowardsChest;
                }
                break;

            case enemyStates.Attack:
                //if (distance >= 50) {
                //    goblinState = enemyStates.Idle;
                //}
                //if (distanceToTreasure > distance) {
                //    goblinState = enemyStates.moveTowardsChest;
                //}
                break;


                //if (distance < 2) {
                //    goblinState = enemyStates.Attack;
                //}
                if (distanceToTreasure < 1.5) {
                    goblinState = enemyStates.Steal;
                }
                break;

            case enemyStates.Steal:

                goblinState = enemyStates.Steal;
                if (carryGold == true) {
                    goblinState = enemyStates.Flee;
                }
                break;

            case enemyStates.Flee:
                if (distance < 10) {
                    goblinState = enemyStates.Flee;
                }
                break;

            default:
                break;

        }
        UpdateState(playerObj, treasureChest, goblinState, dwarfTransform);
    }

}