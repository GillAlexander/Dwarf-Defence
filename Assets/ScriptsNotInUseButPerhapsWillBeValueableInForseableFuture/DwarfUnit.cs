using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DwarfUnit : FriendlyUnitAI
{
    friendlyUnitStates dwarfState = friendlyUnitStates.DefenceMode;

    public DwarfUnit(Transform dwarfObj)
    {
        base.allyObj = dwarfObj;
    }


    public friendlyUnitStates GetMyState()
    {
        return dwarfState;
    }

    void Start()
    {
        health = 150;
    }
    int unitCommand = 0;
    public override void UpdateFriendlyTroops(Transform treasureChest, NavMeshAgent dwarfAgent)
    {

        float distanceToTreasureChest = (allyObj.transform.position - treasureChest.transform.position).magnitude;

        switch (dwarfState)
        {
            case friendlyUnitStates.FollowMode:
                if (Input.GetKeyDown(KeyCode.G))
                {
                    unitCommand--;
                }
                if (distanceToTreasureChest < 5)
                {
                    dwarfState = friendlyUnitStates.DefenceMode;
                }
                if (unitCommand == 0)
                {
                    dwarfState = friendlyUnitStates.DefenceMode;
                }
                break;

            case friendlyUnitStates.DefenceMode:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    unitCommand++;
                }
                if (unitCommand == 1)
                {
                    dwarfState = friendlyUnitStates.FollowMode;
                }
                break;

            default:
                break;
        }
        UpdateState(treasureChest, dwarfState, dwarfAgent);
    }
}