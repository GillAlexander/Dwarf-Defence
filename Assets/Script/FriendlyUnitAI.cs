using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitAI : baseUnit {

    protected Transform allyObj;

    //Friendly unit states
    protected enum friendlyUnitStates {
        FollowMode,
        DefenceMode,
    }

    public virtual void UpdateFriendlyTroops(Transform treasureChest) {

    }

    protected void UpdateState(Transform treasureChest, friendlyUnitStates allyUnitState) {


        switch (allyUnitState) {
            case friendlyUnitStates.FollowMode:
                allyObj.Translate(treasureChest.position * 2 * Time.deltaTime);
                Debug.Log("FOLLOW THE CHEST");
                break;
            case friendlyUnitStates.DefenceMode:
                //Player can controll the units 
                break;
            default:
                break;
        }
    }
}