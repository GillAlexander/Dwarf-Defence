using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreasureChest : MonoBehaviour
{
    public float gold;
    public NavMeshAgent chestAgent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Troll"))
        {
            removeGold(10);
        }
    }
    public void removeGold(int ammount)
    {

        gold -= ammount;

        if (gold <= 0)
        {
            Debug.Log("You lose the game");
        }
    }
    void Start()
    {
        
    }
    public treasureStates GetMyState() {
        return currentTreasureState;
    }

    treasureStates currentTreasureState = treasureStates.DefenceMode;
    public enum treasureStates {
        FollowMode,
        DefenceMode
    }

    void UpdateState(NavMeshAgent chestAgent) {
        switch (currentTreasureState) {
            case treasureStates.FollowMode:
                if (Input.GetKeyDown(KeyCode.F)) {
                    currentTreasureState = treasureStates.DefenceMode;
                }
                chestAgent.isStopped = false;
                break;

            case treasureStates.DefenceMode:
                if (Input.GetKeyDown(KeyCode.F)) {
                    currentTreasureState = treasureStates.FollowMode;
                }
                chestAgent.isStopped = true;
                break;

            default:
                break;
        }
    }

    void Update() {
        UpdateState(chestAgent);
    }
}