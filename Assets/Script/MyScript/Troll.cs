using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Troll : MonoBehaviour
{
    public float health;
    byte carryGold;
    public Slider hpSlider;
    public float senseUnitDistance;
    public NavMeshAgent trollAgent;
    public Animator trollAnimator;
    public bool isDead;
    void Start()
    {
        trollAnimator.SetBool("trollDie", false);
    }

    public Trollstates GetMyState()
    {
        return currentTrollState;
    }

    Trollstates currentTrollState = Trollstates.Idle;
    public enum Trollstates
    {
        Idle,
        Patrol,
        ChargeToAttack,
        DoAttack,
        moveTowardsChest,
        Steal,
        Flee
    }

    float restTimer;


    public int Damage = 5;
    public float Distance;
    public float WeaponRange = 1.5F;

    public float attackDelay = 1f; //seconds
    private float lastAttackAt = -999f;

    void Attack(Dwarf target)
    {
        if (Time.time > lastAttackAt + attackDelay)
        {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }

    public void UpdateState(Transform treasureChest, List<Transform> dwarfTransform)
    {
        Transform GetClosestEnemy(List<Transform> allDwarfsTransform)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = this.transform.position;
            foreach (Transform potentialTarget in allDwarfsTransform)
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



        float? distanceToDwarfs = (transform.position - GetClosestEnemy(dwarfTransform).position).magnitude;
        float distanceToTreasure = (transform.position - treasureChest.position).magnitude;
        switch (currentTrollState)
        {
            case Trollstates.Idle:
                trollAnimator.SetBool("trollMove", false);
                trollAnimator.SetBool("trollAttack", false);
                trollAgent.isStopped = true;
                if (distanceToDwarfs < senseUnitDistance)
                {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                if (distanceToTreasure < senseUnitDistance)
                {
                    currentTrollState = Trollstates.moveTowardsChest;
                }
                if (restTimer > 3)
                {
                    currentTrollState = Trollstates.Patrol;
                }
                break;

            case Trollstates.Patrol:
                //Move randomly between random points 
                //CODE here
                trollAgent.isStopped = false;
                if (restTimer > 6)
                {
                    currentTrollState = Trollstates.Idle;
                    restTimer = 0;
                }
                if (distanceToDwarfs < senseUnitDistance)
                {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                if (distanceToTreasure < senseUnitDistance)
                {
                    currentTrollState = Trollstates.moveTowardsChest;
                }
                break;

            case Trollstates.ChargeToAttack:
                trollAgent.isStopped = false;
                trollAnimator.SetBool("trollMove", true);
                trollAgent.SetDestination((GetClosestEnemy(dwarfTransform).position));
                transform.LookAt(GetClosestEnemy(dwarfTransform).position);
                if (distanceToDwarfs >= senseUnitDistance || distanceToTreasure >= senseUnitDistance)
                {
                    currentTrollState = Trollstates.Idle;
                }
                //if (distanceToTreasure < 3)
                //{
                //    currentTrollState = Trollstates.Steal;
                //}
                if (distanceToDwarfs < senseUnitDistance)
                {
                    trollAgent.SetDestination((GetClosestEnemy(dwarfTransform).position));
                }
                if (distanceToDwarfs < trollAgent.stoppingDistance)
                {
                    currentTrollState = Trollstates.DoAttack;
                }
                break;

            case Trollstates.DoAttack:
                trollAnimator.SetBool("trollMove", false);
                trollAnimator.SetBool("trollAttack", true);
                transform.LookAt(GetClosestEnemy(dwarfTransform).position);
                if (distanceToDwarfs > trollAgent.stoppingDistance)
                {
                    currentTrollState = Trollstates.ChargeToAttack;
                }

                //Do damage to the nearest dwarf unit 
                //Code here
                break;

            //Memoirs Glöm inte att flytta enemyOBJ och inte getclosestenemy
            case Trollstates.moveTowardsChest:
                trollAgent.isStopped = false;
                //Look at the treasure
                if (distanceToTreasure <= 0)
                {
                    currentTrollState = Trollstates.Steal;
                }
                if (distanceToDwarfs < 6)
                {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                trollAgent.SetDestination(treasureChest.position);
                break;

            case Trollstates.Steal:
                trollAgent.isStopped = false;
                carryGold = 10;
                Debug.Log("I STOLE");
                currentTrollState = Trollstates.Flee;
                break;

            case Trollstates.Flee:
                trollAgent.isStopped = false;
                trollAgent.SetDestination(Vector3.back);
                Vector3 dirToChest = transform.position - treasureChest.position;
                Vector3 fleePos = transform.position + dirToChest;
                trollAgent.SetDestination(fleePos);
                Debug.Log("FLEEEE");
                break;
        }

        hpSlider.value = health;
        hpSlider.transform.LookAt(Camera.main.transform);
    }
    private void OnTriggerEnter(Collider enemyWeapon)
    {
        if (enemyWeapon.CompareTag("dwarfWeapon"))
        {
            Debug.Log("FHAN");
            ApplyDamage(15);
        }
    }
    private void Update()
    {
        restTimer += Time.deltaTime;
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
        isDead = true;
        trollAnimator.SetBool("trollDie", true);
    }
}