﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : baseUnit {

    protected Transform enemyObj;

    NavMeshAgent agent;
    

    void start() {
        //agent = new Component<NavMeshAgent>; GetComponent<NavMeshAgent>();
    }
    //Enemy states
    protected enum enemyStates {
        Idle,
        Patrol,
        ChargeToAttack,
        DoAttack,
        moveTowardsChest,
        Steal,
        Flee
    }

    //Update enemy by giving it a new state
    public virtual void UpdateEnemy(Transform playerObj, Transform treasureChest, List<Transform> dwarfTransform) {

    }

    //Update enemy behaviour based on state
    protected void UpdateState(Transform playerObj, Transform treasureChest, enemyStates enemyState, List<Transform> dwarfTransform) {
        float fleeSpeed = 2.5f;
        float strollSpeed = 2f;


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

        float distanceToDwarfs = (enemyObj.transform.position - GetClosestEnemy(dwarfTransform).position).magnitude;

        //StateMachine that makes sure the AI cant have several faces at once
        switch (enemyState) {
            case enemyStates.Idle:
                
                break;

            case enemyStates.Patrol:
                //Move randomly between random points 
                Vector3 randomPosition = new Vector3(Random.Range(0, 5), 0f, Random.Range(0, 5));
                enemyObj.rotation = Quaternion.LookRotation(enemyObj.position - randomPosition);
                break;

            case enemyStates.ChargeToAttack:
                enemyObj.rotation = Quaternion.LookRotation(GetClosestEnemy(dwarfTransform).position - enemyObj.position);
                enemyObj.Translate(enemyObj.forward * 2 * Time.deltaTime);

                //agent.Move((GetClosestEnemy(dwarfTransform).position));
                break;

            case enemyStates.DoAttack:
                //Do damage to the nearest dwarf unit 
                //Do damage

                break;

                //Memoirs Glöm inte att flytta enemyOBJ och inte getclosestenemy
            case enemyStates.moveTowardsChest:
                //Look at the treasure
                enemyObj.rotation = Quaternion.LookRotation(treasureChest.position - enemyObj.position);
                //Move
                enemyObj.Translate(enemyObj.forward * strollSpeed * Time.deltaTime);
                break;

            case enemyStates.Steal:
                carryGold = true;
                break;

            case enemyStates.Flee:
                enemyObj.rotation = Quaternion.LookRotation(enemyObj.position - Vector3.forward);
                //Move
                enemyObj.Translate(enemyObj.forward * fleeSpeed * Time.deltaTime);
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