﻿using System.Collections;
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
    public override void UpdateEnemy(Transform playerObj, Transform treasureChest) {
        float distance = (base.enemyObj.position - playerObj.position).magnitude;
        float distanceToTreasure = (base.enemyObj.position - treasureChest.position).magnitude;
        restTimer += Time.deltaTime;

        switch (goblinState) {
            case enemyStates.Idle:

                goblinState = enemyStates.Idle;
                if (distance < 5) {
                    goblinState = enemyStates.Attack;
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
                if (distanceToTreasure < 5) {
                    goblinState = enemyStates.moveTowardsChest;
                }
                break;

            case enemyStates.Attack:
                if (distance >= 5) {
                    goblinState = enemyStates.Patrol;
                }
                break;

            case enemyStates.moveTowardsChest:
                goblinState = enemyStates.moveTowardsChest;
                if (distanceToTreasure < 1) {
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
        UpdateState(playerObj, treasureChest, goblinState);
    }

}
