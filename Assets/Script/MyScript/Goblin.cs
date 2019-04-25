using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Goblin : MonoBehaviour {
    public float health;
    byte carryGold;
    public NavMeshAgent goblinAgent;
    float restTimer;
    float attackTimer;
    public Slider hpSlider;
    public int Damage = 5;
    public float Distance;
    public float WeaponRange = 1.5F;
    public float senseUnitDistance;
    public float attackDelay = 0.1f; //seconds
    private float lastAttackAt = -999f;

    void Start() {

    }

    public GoblinStates GetMyState() {
        return currentGoblinState;
    }

    GoblinStates currentGoblinState = GoblinStates.Idle;
    public enum GoblinStates {
        Idle,
        Patrol,
        ChargeToAttack,
        DoAttack,
        moveTowardsChest,
        Steal,
        Flee
    }

    void Attack(Dwarf target) {
        if (Time.time > lastAttackAt + attackDelay)
        {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }

    public void UpdateState(Transform treasureChest, List<Transform> dwarfTransform) {
        Transform GetClosestEnemy(List<Transform> allDwarfsTransform) {
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

        float distanceToDwarfs = (transform.position - GetClosestEnemy(dwarfTransform).position).magnitude;
        float distanceToTreasure = (transform.position - treasureChest.position).magnitude;

        switch (currentGoblinState)
        {
            case GoblinStates.Idle:
                if (distanceToDwarfs < 10)
                {
                    currentGoblinState = GoblinStates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    currentGoblinState = GoblinStates.moveTowardsChest;
                }
                if (restTimer > 3)
                {
                    currentGoblinState = GoblinStates.Patrol;
                }
                Debug.Log("IDLE Goblin");
                break;

            case GoblinStates.Patrol:
                //Move randomly between random points 
                //CODE here

                if (restTimer > 6)
                {
                    currentGoblinState = GoblinStates.Idle;
                    restTimer = 0;
                }
                if (distanceToDwarfs < 10)
                {
                    currentGoblinState = GoblinStates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    currentGoblinState = GoblinStates.moveTowardsChest;
                }
                Debug.Log("PATROL Goblin");
                break;

            case GoblinStates.ChargeToAttack:
                if (distanceToDwarfs >= 15 || distanceToTreasure >= 15)
                {
                    currentGoblinState = GoblinStates.Idle;
                }
                if (distanceToTreasure < 1.5)
                {
                    currentGoblinState = GoblinStates.Steal;
                }
                if (distanceToDwarfs < 10)
                {
                    goblinAgent.SetDestination((GetClosestEnemy(dwarfTransform).position));
                    Debug.Log("CHARGE Goblin");
                }
                if (distanceToDwarfs < 2f)
                {
                    attackTimer = 0;
                    currentGoblinState = GoblinStates.DoAttack;
                }

                break;

            case GoblinStates.DoAttack:
                //Do damage to the nearest dwarf unit 

                currentGoblinState = GoblinStates.Idle;
                break;

            //Memoirs Glöm inte att flytta enemyOBJ och inte getclosestenemy
            case GoblinStates.moveTowardsChest:
                //Look at the treasure
                if (distanceToTreasure < 1.5)
                {
                    currentGoblinState = GoblinStates.Steal;
                }
                if (distanceToDwarfs < 3.5)
                {
                    currentGoblinState = GoblinStates.ChargeToAttack;
                }

                goblinAgent.SetDestination(treasureChest.position);
                break;

            case GoblinStates.Steal:
                carryGold = 10;
                Debug.Log("I STOLEGoblin");
                break;

            case GoblinStates.Flee:
                goblinAgent.SetDestination(Vector3.back);
                Debug.Log("Goblin FLEEEE");
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Distance = hit.distance;
            if (Distance < WeaponRange)
            {
                Dwarf dwarf = hit.transform.GetComponent<Dwarf>();
                if (dwarf != null) Attack(dwarf);
            }
        }
    }



    void Update() {
        restTimer += Time.deltaTime;
        attackTimer = Time.deltaTime;

        hpSlider.value = health;
        hpSlider.transform.LookAt(Camera.main.transform);
    }


    public void ApplyDamage(int damage) {
        health -= damage;

        if (health <= 0)
        {
            Dead();
        }
    }
    private void Dead() {
        Destroy(gameObject);
    }
}
