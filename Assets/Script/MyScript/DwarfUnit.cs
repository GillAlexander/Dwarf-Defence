using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DwarfUnit : Unit
{
    dwarfStates currentDwarfState = dwarfStates.Idle;
    dwarfMajorStates currentMajorDwarfState = dwarfMajorStates.DefenceMode;

    private void Awake()
    {
        health = 100;
        unitAgent = GetComponent<NavMeshAgent>();
        hpSlider = GetComponentInChildren<Slider>();
        unitAnimator = GetComponent<Animator>();
        isDead = false;
    }


    public dwarfMajorStates GetMyState()
    {
        return currentMajorDwarfState;
    }
    public void UpdateState(Transform treasureChest, List<Transform> enemyTransform)
    {
        distanceToEnemy = (transform.position - GetClosestEnemy(enemyTransform).position).magnitude;
        distanceToTreasure = (transform.position - treasureChest.position).sqrMagnitude;
        switch (currentMajorDwarfState)
        {
            case dwarfMajorStates.FollowMode:
                Debug.Log("FollowMode");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentMajorDwarfState = dwarfMajorStates.DefenceMode;
                }
                unitAnimator.SetBool("dwarfAttack", true);
                unitAnimator.SetBool("dwarfMove", false);
                if (distanceToTreasure < 10)
                {
                    //dwarfAgent.isStopped = true;
                    unitAnimator.SetBool("dwarfAttack", false);
                    unitAnimator.SetBool("dwarfMove", false);
                }
                break;
            case dwarfMajorStates.DefenceMode:
                Debug.Log("DefenceMode");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentMajorDwarfState = dwarfMajorStates.FollowMode;
                }
                switch (currentDwarfState)
                {
                    case dwarfStates.Idle:
                        unitAnimator.SetBool("dwarfAttack", false);
                        unitAnimator.SetBool("dwarfMove", false);
                        if (unitAgent.velocity != Vector3.zero)
                        {
                            currentDwarfState = dwarfStates.Move;
                        }
                        if (distanceToEnemy < unitAgent.stoppingDistance)
                        {
                            currentDwarfState = dwarfStates.Attack;
                        }
                        break;
                    case dwarfStates.Move:
                        unitAnimator.SetBool("dwarfAttack", false);
                        unitAnimator.SetBool("dwarfMove", true);
                        if (distanceToEnemy < unitAgent.stoppingDistance)
                        {
                            currentDwarfState = dwarfStates.Attack;
                        }
                        if (unitAgent.velocity == Vector3.zero)
                        {
                            currentDwarfState = dwarfStates.Idle;
                        }
                        break;
                    case dwarfStates.Attack:
                        unitAnimator.SetBool("dwarfAttack", true);
                        unitAnimator.SetBool("dwarfMove", false);
                        if (distanceToEnemy > unitAgent.stoppingDistance)
                        {
                            currentDwarfState = dwarfStates.Idle;
                        }
                        break;
                    case dwarfStates.TakeDamage:
                        break;
                    case dwarfStates.Die:
                        unitAnimator.SetBool("dwarfDie", true);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    void Update()
    {

    }

}
