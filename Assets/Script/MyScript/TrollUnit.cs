using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TrollUnit : Unit
{
    trollStates currentTrollState = trollStates.Idle;
    public float senseEnemyDistance;

    private void Awake()
    {
        health = 250;
        unitAgent = GetComponent<NavMeshAgent>();
        hpSlider = GetComponentInChildren<Slider>();
        unitAnimator = GetComponent<Animator>();
        isDead = false;
    }
    public trollStates GetMyState()
    {
        return currentTrollState;
    }
    public void UpdateState(Transform treasureChest, List<Transform> enemyTransform)
    {
        distanceToEnemy = (transform.position - GetClosestEnemy(enemyTransform).position).magnitude;
        distanceToTreasure = (transform.position - treasureChest.position).sqrMagnitude;

        switch (currentTrollState)
        {
            case trollStates.Idle:
                unitAnimator.SetBool("trollMove", false);
                unitAnimator.SetBool("trollAttack", false);
                if (distanceToEnemy < senseEnemyDistance)
                {
                    currentTrollState = trollStates.ChargeToAttack;
                }
                if (distanceToTreasure < senseEnemyDistance)
                {
                    currentTrollState = trollStates.moveTowardsChest;
                }
                //if (restTimer > 3)
                //{
                //    currentTrollState = trollStates.Patrol;
                //}
                break;
            case trollStates.Patrol:
                //Move randomly between random points 
                unitAgent.isStopped = false;
                //if (restTimer > 6)
                //{
                //    currentTrollState = Trollstates.Idle;
                //    restTimer = 0;
                //}
                if (distanceToEnemy < senseEnemyDistance)
                {
                    currentTrollState = trollStates.ChargeToAttack;
                }
                if (distanceToTreasure < senseEnemyDistance)
                {
                    currentTrollState = trollStates.moveTowardsChest;
                }
                break;
            case trollStates.ChargeToAttack:
                unitAgent.isStopped = false;
                unitAnimator.SetBool("trollMove", true);
                unitAgent.SetDestination((GetClosestEnemy(enemyTransform).position));
                transform.LookAt(GetClosestEnemy(enemyTransform).position);
                if (distanceToEnemy >= senseEnemyDistance && distanceToTreasure >= senseEnemyDistance)
                {
                    currentTrollState = trollStates.Idle;
                }
                if (distanceToEnemy < senseEnemyDistance)
                {
                    unitAgent.SetDestination((GetClosestEnemy(enemyTransform).position));
                }
                if (distanceToEnemy < unitAgent.stoppingDistance)
                {
                    currentTrollState = trollStates.DoAttack;
                }
                break;
            case trollStates.DoAttack:
                unitAnimator.SetBool("trollMove", false);
                unitAnimator.SetBool("trollAttack", true);
                transform.LookAt(GetClosestEnemy(enemyTransform).position);
                if (distanceToEnemy > unitAgent.stoppingDistance)
                {
                    currentTrollState = trollStates.ChargeToAttack;
                }
                break;
                break;
            case trollStates.moveTowardsChest:
                unitAgent.isStopped = false;
                unitAnimator.SetBool("trollMove", true);
                unitAnimator.SetBool("trollAttack", false);
                //Look at the treasure
                if (distanceToTreasure <= 0)
                {
                    currentTrollState = trollStates.Steal;
                }
                if (distanceToEnemy < 6)
                {
                    currentTrollState = trollStates.ChargeToAttack;
                }
                unitAgent.SetDestination(treasureChest.position);
                break;
            case trollStates.Steal:
                unitAgent.isStopped = false;
                //carryGold = 10;
                Debug.Log("I STOLE");
                currentTrollState = trollStates.Flee;
                break;
            case trollStates.Flee:
                unitAgent.isStopped = false;
                unitAgent.SetDestination(Vector3.back);
                Vector3 dirToChest = transform.position - treasureChest.position;
                Vector3 fleePos = transform.position + dirToChest;
                unitAgent.SetDestination(fleePos);
                Debug.Log("FLEEEE");
                break;
            default:
                break;
        }
    }
    void Update()
    {
        
    }
}
