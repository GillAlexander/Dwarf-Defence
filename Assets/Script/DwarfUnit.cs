using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfUnit : FriendlyUnitAI {
    friendlyUnitStates dwarfState = friendlyUnitStates.DefenceMode;

    public DwarfUnit(Transform dwarfObj) {
        base.allyObj = dwarfObj;
    }
    void Start() {
        health = 150;
    }

    public override void UpdateFriendlyTroops(Transform treasureChest) {

        switch (dwarfState) {
            case friendlyUnitStates.FollowMode:

                
                break;
            case friendlyUnitStates.DefenceMode:
                if (Input.GetKey(KeyCode.F)) {
                    Debug.Log("ORder is given!");
                    dwarfState = friendlyUnitStates.FollowMode;
                }
                break;
            default:
                break;
        }
    }
}
