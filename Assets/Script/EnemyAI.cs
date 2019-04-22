using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : baseUnit {

    protected Transform enemyObj;

    NavMeshAgent agent;

    void start() {
    }
    //Enemy states
    protected enum enemyStates {
        Idle,
        Patrol,
        Attack,
        moveTowardsChest,
        Steal,
        Flee
    }

    //Update enemy by giving it a new state
    public virtual void UpdateEnemy(Transform playerObj, Transform treasureChest) {

    }

    //Update enemy behaviour based on state
    protected void UpdateState(Transform playerObj, Transform treasureChest, enemyStates enemyState) {
        float fleeSpeed = 2.5f;
        float strollSpeed = 2f;
        float attackSpeed = 5f;
        switch (enemyState) {
            case enemyStates.Idle:
                Debug.Log("IDle Bitch");
                break;

            case enemyStates.Patrol:
                //Move randomly between random points 
                Vector3 randomPosition = new Vector3(Random.Range(0, 360), 0f, Random.Range(0, 360));
                enemyObj.rotation = Quaternion.LookRotation(enemyObj.position - randomPosition);
                enemyObj.Translate(enemyObj.forward * strollSpeed * Time.deltaTime);
                Debug.Log("Patrol Bitch");
                break;

            case enemyStates.Attack:
                enemyObj.rotation = Quaternion.LookRotation(playerObj.position - enemyObj.position);
                enemyObj.Translate(enemyObj.forward * strollSpeed * Time.deltaTime);
                Debug.Log("Attack Bitch");
                break;

            case enemyStates.moveTowardsChest:
                //Look at the treasure
                enemyObj.rotation = Quaternion.LookRotation(treasureChest.position - enemyObj.position);
                //Move
                enemyObj.Translate(enemyObj.forward * strollSpeed * Time.deltaTime);
                Debug.Log("MoveTowardsChest Bitch");
                break;

            case enemyStates.Steal:
                carryGold = true;
                Debug.Log("Steal Bitch");
                break;

            case enemyStates.Flee:
                enemyObj.rotation = Quaternion.LookRotation(enemyObj.position - Vector3.forward);
                //Move
                enemyObj.Translate(enemyObj.forward * fleeSpeed * Time.deltaTime);
                Debug.Log("FLee Bitch");
                break;

            default:
                break;
        }
    }

    //Transform GetClosestEnemy(Transform[] enemies) {
    //    Transform bestTarget = null;
    //    float closestDistanceSqr = Mathf.Infinity;
    //    Vector3 currentPosition = transform.position;
    //    foreach (Transform potentialTarget in enemies) {
    //        Vector3 directionToTarget = potentialTarget.position - currentPosition;
    //        float dSqrToTarget = directionToTarget.sqrMagnitude;
    //        if (dSqrToTarget < closestDistanceSqr) {
    //            closestDistanceSqr = dSqrToTarget;
    //            bestTarget = potentialTarget;
    //        }
    //    }

    //    return bestTarget;
    //}

}
