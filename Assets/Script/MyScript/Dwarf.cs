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

    void Start()
    {
        
    }

    public dwarfStates GetMyState()
    {
        return currentDwarfState;
    }

    dwarfStates currentDwarfState = dwarfStates.DefenceMode;
    public enum dwarfStates
    {
        FollowMode,
        DefenceMode,
    }


    //, List<Transform> TrollTransform, List<Transform> GoblinTransform

    public void UpdateState(Transform treasureChest, List<Transform> enemyTransform)
    {
        Transform GetClosestEnemy(List<Transform> allEnemyTranform) {
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

        float? distanceToTrolls = (transform.position - GetClosestEnemy(enemyTransform).position).magnitude;
        //float? distanceToGoblins = (transform.position - GetClosestEnemy(GoblinTransform).position).magnitude;

        switch (currentDwarfState)
        {
            case dwarfStates.FollowMode:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentDwarfState = dwarfStates.DefenceMode;
                }
                dwarfAgent.SetDestination(treasureChest.position);
                break;

            case dwarfStates.DefenceMode:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentDwarfState = dwarfStates.FollowMode;
                }
                Vector3 targetDir = GetClosestEnemy(enemyTransform).position - transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5, 0.0f);
                

                if (distanceToTrolls < 5)
                {
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
                break;

            default:
                break;
        }

        
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
    //Buttontest
    public void changeDwarfStateToDefence() {
        currentDwarfState = dwarfStates.DefenceMode;
    }
    public void changeDwarfStateToFollow() {
        currentDwarfState = dwarfStates.DefenceMode;
    }

    /// <summary>
    /// Damage Code
    /// </summary>
    /// <param name="damage"></param>
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

    void Attack(Goblin target) {
        if (Time.time > lastAttackAt + attackDelay)
        {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }
    void Attack(Troll target) {
        if (Time.time > lastAttackAt + attackDelay)
        {
            lastAttackAt = Time.time;
            target.ApplyDamage(Damage);
        }
    }

    void Update() {

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward * 2, out hit))
        //{
        //    if (hit.collider)
        //    {
        //    }
        //    Debug.DrawLine(transform.position, transform.forward * 2);
        //    Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        //}


        hpSlider.value = health;
        hpSlider.transform.LookAt(Camera.main.transform);
    }
}
