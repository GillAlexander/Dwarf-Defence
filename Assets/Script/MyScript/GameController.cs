using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public Transform treasureChest;
    public List<Troll> Trolls = new List<Troll>();
    public List<Goblin> Goblins = new List<Goblin>();
    public List<Dwarf> Dwarfs = new List<Dwarf>();
    //A list that will hold all enemies
    public List<Transform> dwarfTransform = new List<Transform>();
    public List<Transform> trollTransform = new List<Transform>();

    void Start() {
        //Add the enemies we have
    }

    void Update()
    {
        for (int i = dwarfTransform.Count - 1; i > -1; i--) {
            if (dwarfTransform[i] == null)
                dwarfTransform.RemoveAt(i);
        }
        for (int i = 0; i < Trolls.Count; i++) {
            if (Trolls[i] == null)
                Trolls.RemoveAt(i);
        }
        for (int i = 0; i < Goblins.Count; i++) {
            if (Goblins[i] == null)
                Goblins.RemoveAt(i);
        }

        //Update all enemies to see if they should change state and move/attack player
        for (int i = 0; i < Trolls.Count; i++)
        {
            Trolls[i].UpdateState(treasureChest, dwarfTransform);
        }
        for (int i = 0; i < Goblins.Count; i++) {
            Goblins[i].UpdateState(treasureChest, dwarfTransform);
        }
        for (int i = 0; i < Dwarfs.Count; i++)
        {
            Dwarfs[i].UpdateState(treasureChest, trollTransform);
        }

    }
}

