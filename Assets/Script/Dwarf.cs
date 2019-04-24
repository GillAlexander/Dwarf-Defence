using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dwarf : MonoBehaviour
{
    public float health;
    public Transform treasureChest;
    
    public NavMeshAgent dwarfAgent;

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
        DefenceMode
    }

    void UpdateState(NavMeshAgent dwarfAgent,Transform treasureChest )
    {
        switch (currentDwarfState)
        {
            case dwarfStates.FollowMode:
                if (Input.GetKeyDown(KeyCode.G))
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
                Debug.Log("You can now controll your units");
                break;
            default:
                break;
        }

        //UpdateState(dwarfAgent, treasureChest);
    }


    void Update()
    {
        UpdateState(dwarfAgent, treasureChest);
        Debug.Log(currentDwarfState);
    }
}
