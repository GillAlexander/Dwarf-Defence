using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DwarfUnit : FriendlyUnitAI {
    friendlyUnitStates dwarfState = friendlyUnitStates.DefenceMode;

    public DwarfUnit(Transform dwarfObj) {
        base.allyObj = dwarfObj;
    }
    void Start() {
        health = 150;
    }

    public override void UpdateFriendlyTroops(Transform treasureChest, NavMeshAgent dwarfAgent) {

        float distanceToTreasureChest = (allyObj.transform.position - treasureChest.transform.position).magnitude;

        switch (dwarfState) {
            case friendlyUnitStates.FollowMode:
                if (Input.GetKeyDown(KeyCode.G)) {
                    dwarfState = friendlyUnitStates.DefenceMode;
                    Debug.Log("Defence order!");
                }
                if (distanceToTreasureChest < 5) {
                    dwarfState = friendlyUnitStates.DefenceMode;
                }
                break;
            case friendlyUnitStates.DefenceMode:
                if (Input.GetKeyDown(KeyCode.F)) {
                    Debug.Log("Follow order!");
                    dwarfState = friendlyUnitStates.FollowMode;
                }
                break;
            default:
                break;
        }
        UpdateState(treasureChest, dwarfState, dwarfAgent);
    }
}
