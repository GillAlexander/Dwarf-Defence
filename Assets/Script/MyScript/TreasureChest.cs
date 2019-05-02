using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreasureChest : MonoBehaviour
{
    public float gold;
    public NavMeshAgent chestAgent;
    private float distanceToTrolls;


    void Start()
    {

    }
    public treasureStates GetMyState()
    {
        return currentTreasureState;
    }

    treasureStates currentTreasureState = treasureStates.DefenceMode;
    public enum treasureStates
    {
        FollowMode,
        DefenceMode
    }

    public void UpdateState(List<Transform> trollTransform)
    {

        distanceToTrolls = (transform.position - GetClosestEnemy(trollTransform).position).magnitude;

        switch (currentTreasureState)
        {
            case treasureStates.FollowMode:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentTreasureState = treasureStates.DefenceMode;
                }
                Debug.Log("FollowMode");
                if (distanceToTrolls <= 4)
                {
                    Debug.Log("RemovedGold");
                    removeGold(10);
                }
                break;

            case treasureStates.DefenceMode:
                
                if (distanceToTrolls <= 4)
                {
                    Debug.Log("RemovedGold");
                    removeGold(10);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentTreasureState = treasureStates.FollowMode;
                }

                Debug.Log("DefenceMode");
                break;

            default:
                break;
        }


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
    public void removeGold(int ammount)
    {
        gold -= ammount;

        if (gold <= 0)
        {
            Debug.Log("You lose the game");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("winArea"))
        {
            Debug.Log("YOU HAVE WIN THE GAME");
        }
    }

    void Update()
    {

    }
}