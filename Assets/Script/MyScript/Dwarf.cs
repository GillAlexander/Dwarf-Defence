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
    Animator dwarfAnimator;

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
                    transform.rotation = Quaternion.LookRotation(newDir);
                }

                switch (currentDwarfState)
                {
                    case dwarfStates.Idle:

                        break;
                    case dwarfStates.Move:
                        break;
                    case dwarfStates.Attack:
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
        distanceToTrolls = (transform.position - GetClosestEnemy(enemyTransform).position).magnitude;

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Distance = hit.distance;
            if (Distance < WeaponRange)
            {
                Troll Troll = hit.transform.GetComponent<Troll>();
                if (Troll != null) Attack(Troll);
                Goblin Goblin = hit.transform.GetComponent<Goblin>();
                if (Goblin != null) Attack(Goblin);
            }
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