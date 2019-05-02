using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dwarf : MonoBehaviour
{
    public float health;
    public Transform treasureChest;
    public NavMeshAgent dwarfAgent;
    public Slider hpSlider;
    public int Damage = 5;
    public float Distance;
    public float WeaponRange = 1.5F;
    public float attackDelay = 1f; //seconds
    private float lastAttackAt = -999f;
    public bool isDead;
    dwarfStates currentDwarfState = dwarfStates.Idle;
    dwarfMajorStates currentMajorDwarfState = dwarfMajorStates.DefenceMode;
    private float distanceToTrolls;
    public Animator dwarfAnimator;

    void Start()
    {

    }

    public dwarfMajorStates GetMyState()
    {
        return currentMajorDwarfState;
    }
    public enum dwarfMajorStates
    {
        FollowMode,
        DefenceMode,
    }
    public enum dwarfStates
    {
        Idle,
        Move,
        Attack,
        TakeDamage,
        Die,
    }


    public void UpdateState(Transform treasureChest, List<Transform> enemyTransform)
    {

        distanceToTrolls = (transform.position - GetClosestEnemy(enemyTransform).position).magnitude;
        switch (currentMajorDwarfState)
        {
            case dwarfMajorStates.FollowMode:
                float distanceToTreasure = (transform.position - treasureChest.position).magnitude;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //dwarfAgent.isStopped = false;
                    currentMajorDwarfState = dwarfMajorStates.DefenceMode;
                }
                dwarfAnimator.SetBool("dwarfAttack", true);
                dwarfAnimator.SetBool("dwarfMove", false);

                if (distanceToTreasure < 10)
                {
                    //dwarfAgent.isStopped = true;
                    dwarfAnimator.SetBool("dwarfAttack", false);
                    dwarfAnimator.SetBool("dwarfMove", false);
                }
                else
                {
                    //dwarfAgent.isStopped = false;
                    dwarfAgent.SetDestination(treasureChest.position);
                    dwarfAnimator.SetBool("dwarfAttack", false);
                    dwarfAnimator.SetBool("dwarfMove", true);
                }
                break;

            case dwarfMajorStates.DefenceMode:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentMajorDwarfState = dwarfMajorStates.FollowMode;
                }

                if (distanceToTrolls < 5)
                {
                    transform.LookAt(GetClosestEnemy(enemyTransform).position);
                }

                switch (currentDwarfState)
                {
                    case dwarfStates.Idle:
                        //Neutral state 
                        dwarfAnimator.SetBool("dwarfAttack", false);
                        dwarfAnimator.SetBool("dwarfMove", false);

                        if (dwarfAgent.velocity != Vector3.zero)
                        {
                            currentDwarfState = dwarfStates.Move;
                        }
                        if (distanceToTrolls < dwarfAgent.stoppingDistance)
                        {
                            currentDwarfState = dwarfStates.Attack;
                        }
                        break;

                    case dwarfStates.Move:
                        dwarfAnimator.SetBool("dwarfAttack", false);
                        dwarfAnimator.SetBool("dwarfMove", true);

                        if (distanceToTrolls < dwarfAgent.stoppingDistance)
                        {
                            currentDwarfState = dwarfStates.Attack;
                        }
                        if (dwarfAgent.velocity == Vector3.zero)
                        {
                            currentDwarfState = dwarfStates.Idle;
                        }
                        break;

                    case dwarfStates.Attack:
                        dwarfAnimator.SetBool("dwarfAttack", true);
                        dwarfAnimator.SetBool("dwarfMove", false);

                        if (distanceToTrolls > dwarfAgent.stoppingDistance)
                        {
                            currentDwarfState = dwarfStates.Idle;
                        }
                        break;
                        
                    case dwarfStates.TakeDamage:
                        break;

                    case dwarfStates.Die:
                        break;

                    default:
                        break;

                }
                break;

            default:
                break;
        }
        Transform GetClosestEnemy(List<Transform> allEnemyTranform)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = this.transform.position;
            foreach (Transform potentialTarget in allEnemyTranform)
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
    }

    public void ApplyDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponentInChildren<Canvas>());
        dwarfAnimator.SetBool("dwarfDie", true);
        isDead = true;
    }
    private void OnTriggerEnter(Collider enemyWeapon)
    {
        if (enemyWeapon.CompareTag("trollWeapon"))
        {
            ApplyDamage(10);
        }
    }
    void Attack(Goblin target)
    {
        if (Time.time > lastAttackAt + attackDelay)
        {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }
    void Attack(Troll target)
    {
        if (Time.time > lastAttackAt + attackDelay)
        {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }

    void Update()
    {

        hpSlider.value = health;
        hpSlider.transform.LookAt(Camera.main.transform);
    }
}

////Buttontest
//public void changeDwarfStateToDefence() {
//    currentDwarfState = dwarfStates.DefenceMode;
//}
//public void changeDwarfStateToFollow() {
//    currentDwarfState = dwarfStates.DefenceMode;
//}