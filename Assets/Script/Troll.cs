using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troll : MonoBehaviour
{
    public float health;
    byte carryGold;
    
    public NavMeshAgent trollAgent;

    void Start() {

    }

    public Trollstates GetMyState() {
        return currentTrollState;
    }

    Trollstates currentTrollState = Trollstates.ChargeToAttack;
    public enum Trollstates {
        Idle,
        Patrol,
        ChargeToAttack,
        DoAttack,
        moveTowardsChest,
        Steal,
        Flee
    }

    float restTimer;
    float attackTimer;


    public int Damage = 5;
    public float Distance;
    public float WeaponRange = 1.5F;

    public float attackDelay = 1f; //seconds
    private float lastAttackAt = -999f;

    void Attack(Dwarf target) {
        if (Time.time > lastAttackAt + attackDelay) {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }

    public void UpdateState(Transform treasureChest, List<Transform> dwarfTransform) {
        

        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
            Distance = hit.distance;
            if (Distance < WeaponRange)
            {
                Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
                Dwarf dwarf = hit.transform.GetComponent<Dwarf>();
                if (dwarf != null) Attack(dwarf);
            }
        }
        

        restTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        float distanceToDwarfs = (transform.position - GetClosestEnemy(dwarfTransform).position).magnitude;
        float distanceToTreasure = (transform.position - treasureChest.position).magnitude;
        switch (currentTrollState) {

            case Trollstates.Idle:
                if (distanceToDwarfs < 10) {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                if (distanceToTreasure < 15) {
                    currentTrollState = Trollstates.moveTowardsChest;
                }
                if (restTimer > 3) {
                    currentTrollState = Trollstates.Patrol;
                }
                Debug.Log("IDLE TROLL");
                break;

            case Trollstates.Patrol:
                //Move randomly between random points 
                //CODE here

                if (restTimer > 6) {
                    currentTrollState = Trollstates.Idle;
                    restTimer = 0;
                }
                if (distanceToDwarfs < 10) {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                if (distanceToTreasure < 15) {
                    currentTrollState = Trollstates.moveTowardsChest;
                }
                Debug.Log("PATROL TROLL");
                break;

            case Trollstates.ChargeToAttack:
                if (distanceToDwarfs >= 15 || distanceToTreasure >= 15) {
                    currentTrollState = Trollstates.Idle;
                }
                if (distanceToTreasure < 1.5) {
                    currentTrollState = Trollstates.Steal;
                }
                if (distanceToDwarfs < 10) {
                    trollAgent.SetDestination((GetClosestEnemy(dwarfTransform).position));
                    Debug.Log("CHARGE TROLL");
                }
                if (distanceToDwarfs < 1.5f) {
                    attackTimer = 0;
                    currentTrollState = Trollstates.DoAttack;
                }

                break;

            case Trollstates.DoAttack:
                //Do damage to the nearest dwarf unit 

                if (attackTimer > 0.3) {
                    Debug.Log("I DID AN ATTACK");
                    currentTrollState = Trollstates.ChargeToAttack;

                }
                break;

            //Memoirs Glöm inte att flytta enemyOBJ och inte getclosestenemy
            case Trollstates.moveTowardsChest:
                //Look at the treasure
                if (distanceToDwarfs < 6) {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                if (distanceToTreasure < 1.5) {
                    currentTrollState = Trollstates.Steal;
                }
                trollAgent.Move(treasureChest.position);
                trollAgent.updateRotation = true;
                break;

            case Trollstates.Steal:
                carryGold = 10;
                Debug.Log("I STOLE");
                break;

            case Trollstates.Flee:
                Debug.Log("FLEEEE");
                break;
        }
    }

    Transform GetClosestEnemy(List<Transform> allDwarfsTransform) {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;
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
    void Update()
    {

    }
}
