using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyUnitAI : baseUnit
{

    protected Transform allyObj;
    public bool canControllUnits = false;


    //Friendly unit states
    public enum friendlyUnitStates
    {
        FollowMode,
        DefenceMode,
    }



    public virtual void UpdateFriendlyTroops(Transform treasureChest, NavMeshAgent dwarfAgent)
    {

    }

    protected void UpdateState(Transform treasureChest, friendlyUnitStates allyUnitState, NavMeshAgent dwarfAgent)
    {

        switch (allyUnitState)
        {
            case friendlyUnitStates.FollowMode:
                canControllUnits = false;

                //allyObj.rotation = Quaternion.LookRotation(treasureChest.position - allyObj.position);
                //allyObj.Translate(allyObj.forward * 2 * Time.deltaTime);
                //dwarfAgent.SetDestination(treasureChest.position);
                break;

            case friendlyUnitStates.DefenceMode:
                //Player can controll the units 
                canControllUnits = true;
                
                break;

            default:
                break;
        }
    }
}