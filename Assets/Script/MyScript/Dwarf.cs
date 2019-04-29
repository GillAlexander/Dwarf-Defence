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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentMajorDwarfState = dwarfMajorStates.DefenceMode;
                }
                dwarfAgent.SetDestination(treasureChest.position);
                break;

            case dwarfMajorStates.DefenceMode:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentMajorDwarfState = dwarfMajorStates.FollowMode;
                }
                //Rotate to n
                Vector3 targetDir = GetClosestEnemy(enemyTransform).position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5, 0.0f);
                if (distanceToTrolls < 5)
                {
                    Vector3 Rotate180 = new Vector3(0, 0, 180);
                    transform.rotation = Quaternion.LookRotation(newDir- Rotate180);
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
                        Debug.Log("DwarfMove");
                        break;
                        //Move dwarf
                    case dwarfStates.Attack:
                        dwarfAnimator.SetBool("dwarfAttack", true);
                        dwarfAnimator.SetBool("dwarfMove", false);
                        Debug.Log("DwarfAttack");
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

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    currentDwarfState = dwarfStates.FollowMode;
        //}
        //Vector3 targetDir = GetClosestEnemy(enemyTransform).position - transform.position;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5, 0.0f);
        //if (distanceToTrolls < 5)
        //{
        //    transform.rotation = Quaternion.LookRotation(newDir);
        //}

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
        Destroy(gameObject);
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