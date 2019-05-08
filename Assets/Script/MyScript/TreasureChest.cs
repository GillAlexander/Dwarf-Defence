using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreasureChest : MonoBehaviour {
    public float gold;
    public NavMeshAgent chestAgent;
    private float distanceToTrolls;
    public Animator dwarfAnimator;
    string descriptiveText;
    string instructionText;
    void Start() {

    }
    public treasureStates GetMyState() {
        return currentTreasureState;
    }

    treasureStates currentTreasureState = treasureStates.DefenceMode;
    public enum treasureStates {
        FollowMode,
        DefenceMode
    }


    public void UpdateState(List<Transform> trollTransform) {
        distanceToTrolls = (transform.position - GetClosestEnemy(trollTransform).position).magnitude;

        switch (currentTreasureState)
        {
            case treasureStates.FollowMode:
                descriptiveText = "You are now in FollowMode";
                instructionText = "Controll your cart";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentTreasureState = treasureStates.DefenceMode;
                }
                if (distanceToTrolls <= 4)
                {
                    Debug.Log("RemovedGold");
                    removeGold(10);
                }
                if (chestAgent.velocity != Vector3.zero)
                {
                    dwarfAnimator.SetBool("dwarfMove", true);
                }
                else
                {
                    dwarfAnimator.SetBool("dwarfMove", false);
                }
                break;

            case treasureStates.DefenceMode:
                if (chestAgent.velocity == Vector3.zero)
                {
                    dwarfAnimator.SetBool("dwarfMove", false);
                }
                descriptiveText = "You are now in DefenceMode";
                instructionText = "Controll your troops";
                if (distanceToTrolls <= 4)
                {
                    Debug.Log("RemovedGold");
                    removeGold(10);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentTreasureState = treasureStates.FollowMode;
                }
                break;

            default:
                break;
        }
    }
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

    public void removeGold(int ammount) {
        gold -= ammount;

        if (gold <= 0)
        {
            Debug.Log("You lose the game");
        }
    }
    bool showWin;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("winArea"))
        {
            showWin = true;
        }
    }

    private void OnGUI() {
        if (showWin)
        {
            GUI.Box(new Rect(40, 40, 300, 40), "YOU WON THE GAME");
        }
        else
        {
            GUI.Box(new Rect(20, 20, 200, 40), descriptiveText);
            GUI.Box(new Rect(20, 60, 200, 40), instructionText);
        }
    }

    void Update() {

    }
}