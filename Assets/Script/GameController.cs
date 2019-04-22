using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GameController : MonoBehaviour {
        public GameObject playerObj;
        public GameObject creeperObj;
        public GameObject skeletonObj;
        public GameObject treasureChest;
        public GameObject goblin;
        //A list that will hold all enemies
        List<EnemyAI> enemies = new List<EnemyAI>();
        List<Transform> friendlies = new List<Transform>();

        void Start() {
            //Add the enemies we have
            //enemies.Add(new Creeper(creeperObj.transform));
            //enemies.Add(new Skeleton(skeletonObj.transform));
            enemies.Add(new Goblin(goblin.transform));
        }


        void Update() {
            //Update all enemies to see if they should change state and move/attack player
            for (int i = 0; i < enemies.Count; i++) {
                enemies[i].UpdateEnemy(playerObj.transform, treasureChest.transform);
            }
        }
    }

