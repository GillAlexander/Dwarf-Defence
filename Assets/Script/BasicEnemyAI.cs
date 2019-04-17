using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject treasureChest;
    
    void Update()
    {
        agent.SetDestination(treasureChest.transform.position);
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Unit")) {
            Debug.Log("Collision");
            Destroy(gameObject);
        }
    }

}
