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
    public override void UpdateEnemy(Transform playerObj, Transform treasureChest, List<Vector3> dwarfVector3) {
        float distance = (base.enemyObj.position - playerObj.position).magnitude;
        float distanceToTreasure = (base.enemyObj.position - treasureChest.position).magnitude;
        
        restTimer += Time.deltaTime;


        Vector3 GetClosestEnemy(List<Vector3> allDwarfes) {
            Vector3 bestTarget = new Vector3();
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = enemyObj.transform.position;
            foreach (Vector3 potentialTarget in allDwarfes) {
                Vector3 directionToTarget = potentialTarget - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr) {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget;
        }
        float distanceToDwarfs = (base.enemyObj.position - GetClosestEnemy(dwarfVector3)).magnitude;

        switch (goblinState) {
            case enemyStates.Idle:
                Debug.Log(distanceToDwarfs);
                goblinState = enemyStates.Idle;

                if (distanceToDwarfs < 80) {
                    goblinState = enemyStates.Attack;
                }
                if (distanceToTreasure < 15) {
                    goblinState = enemyStates.moveTowardsChest;
                }
                if (restTimer > 4) {
                    goblinState = enemyStates.Patrol;
                }

                break;

            case enemyStates.Patrol:

                goblinState = enemyStates.Patrol;

                if (restTimer > 8) {
                    goblinState = enemyStates.Idle;
                    restTimer = 0;
                }
                if (distanceToTreasure < 15) {
                    goblinState = enemyStates.moveTowardsChest;
                }
                break;

            case enemyStates.Attack:
                if (distance >= 5) {
                    goblinState = enemyStates.Patrol;
                }
                //if (distanceToTreasure > distance) {
                //    goblinState = enemyStates.moveTowardsChest;
                //}
                break;

            case enemyStates.moveTowardsChest:

                if (distance < 2) {
                    goblinState = enemyStates.Attack;
                }
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
        UpdateState(playerObj, treasureChest, goblinState, dwarfVector3);
    }

}