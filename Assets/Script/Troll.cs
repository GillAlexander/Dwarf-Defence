using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyAI {
    enemyStates trollState = enemyStates.Idle;

    public Troll(Transform trollObj) {
        base.enemyObj = trollObj;
    }
    void Start() {
        health = 250;
    }
    float restTimer;
    public override void UpdateEnemy(Transform playerObj, Transform treasureChest, List<Vector3> dwarfVector3) {
        float distance = (base.enemyObj.position - playerObj.position).magnitude;
        float distanceToTreasure = (base.enemyObj.position - treasureChest.position).magnitude;
        restTimer += Time.deltaTime;

        switch (trollState) {
            case enemyStates.Idle:
                if (distance < 5) {
                    trollState = enemyStates.Attack;
                    Debug.Log("Bitch");
                }
                if (distanceToTreasure < 15) {
                    trollState = enemyStates.moveTowardsChest;
                }
                if (restTimer > 4) {
                    trollState = enemyStates.Patrol;
                }
                break;
            case enemyStates.Patrol:
                if (restTimer > 8) {
                    trollState = enemyStates.Idle;
                    restTimer = 0;
                }
                if (distanceToTreasure < 15) {
                    trollState = enemyStates.moveTowardsChest;
                }
                break;

            case enemyStates.Attack:
                if (distance >= 5) {
                    trollState = enemyStates.Patrol;
                }
                break;

            case enemyStates.moveTowardsChest:
                if (distance < 6) {
                    trollState = enemyStates.Attack;
                }
                if (distanceToTreasure < 1.5) {
                    trollState = enemyStates.Steal;
                }
                break;
            case enemyStates.Steal:
                trollState = enemyStates.Steal;
                if (carryGold == true) {
                    trollState = enemyStates.Flee;
                }
                break;
            case enemyStates.Flee:
                if (distance < 10) {
                    trollState = enemyStates.Flee;
                }
                break;
            default:
                break;
        }
        UpdateState(playerObj, treasureChest, trollState, dwarfVector3);
    }
}
