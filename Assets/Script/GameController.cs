using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject creeperObj;
    public GameObject skeletonObj;
    public GameObject treasureChest;
    public GameObject goblin;
    public GameObject troll;
    public GameObject dwarf;

    //A list that will hold all enemies
    List<EnemyAI> enemies = new List<EnemyAI>();
    List<FriendlyUnitAI> friendlies = new List<FriendlyUnitAI>();
    public List<NavMeshAgent> dwarfAgent = new List<NavMeshAgent>();
    public List<Transform> dwarfTransform = new List<Transform>();

    void Start()
    {
        //Add the enemies we have
        enemies.Add(new Goblin(goblin.transform));
        enemies.Add(new Troll(troll.transform));

        for (int i = 0; i < dwarfTransform.Count; i++)
        {
            friendlies.Add(new DwarfUnit(dwarfTransform[i]));
        }
    }

    void Update()
    {
        //Update all enemies to see if they should change state and move/attack player
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateEnemy(playerObj.transform, treasureChest.transform, dwarfTransform);
        }
        for (int i = 0; i < friendlies.Count; i++)
        {
            friendlies[i].UpdateFriendlyTroops(treasureChest.transform, dwarfAgent[i]);
        }
    }
}

