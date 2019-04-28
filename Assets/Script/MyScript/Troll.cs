﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Troll : MonoBehaviour {
    public float health;
    byte carryGold;
    public Slider hpSlider;
    public float senseUnitDistance;
    public NavMeshAgent trollAgent;
    public Animator trollAnimator;
    void Start() {

    }

    public Trollstates GetMyState() {
        return currentTrollState;
    }

    Trollstates currentTrollState = Trollstates.Idle;
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
                if (distanceToTreasure < 15)
                {
                    currentTrollState = Trollstates.moveTowardsChest;
                }
                if (restTimer > 3)
                {
                    currentTrollState = Trollstates.Patrol;
                }
                Debug.Log("IDLE TROLL");
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
                if (distanceToDwarfs < 10)
                {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                if (distanceToTreasure < 15)
                {
                    currentTrollState = Trollstates.moveTowardsChest;
                }
                Debug.Log("PATROL TROLL");
                break;

            case Trollstates.ChargeToAttack:
                trollAgent.isStopped = false;
                trollAnimator.SetBool("trollMove", true);
                trollAgent.SetDestination((GetClosestEnemy(dwarfTransform).position));
                if (distanceToDwarfs >= 15 || distanceToTreasure >= 15)
                {
                    currentTrollState = Trollstates.Idle;
                }
                if (distanceToTreasure < 3)
                {
                    currentTrollState = Trollstates.Steal;
                }
                if (distanceToDwarfs < 10)
                {
                    trollAgent.SetDestination((GetClosestEnemy(dwarfTransform).position));
                    Debug.Log("CHARGE TROLL");

                }
                if (distanceToDwarfs < 4)
                {

                    attackTimer = 0;
                    currentTrollState = Trollstates.DoAttack;
                }
                break;

            case Trollstates.DoAttack:
                trollAnimator.SetBool("trollMove", false);
                trollAnimator.SetBool("trollAttack", true);
                if (distanceToDwarfs > 3)
                {
                    currentTrollState = Trollstates.ChargeToAttack;
                }
                currentTrollState = Trollstates.ChargeToAttack;

                //Do damage to the nearest dwarf unit 
                //Code here
                currentTrollState = Trollstates.ChargeToAttack;
                break;

            //Memoirs Glöm inte att flytta enemyOBJ och inte getclosestenemy
            case Trollstates.moveTowardsChest:
                trollAgent.isStopped = false;
                //Look at the treasure
                if (distanceToTreasure < 2)
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
                break;

            case Trollstates.Flee:
                trollAgent.isStopped = false;
                trollAgent.SetDestination(Vector3.back);
                Debug.Log("FLEEEE");
                break;
        }

        hpSlider.value = health;
        hpSlider.transform.LookAt(Camera.main.transform);

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
    private void Update() {
        
            restTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
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