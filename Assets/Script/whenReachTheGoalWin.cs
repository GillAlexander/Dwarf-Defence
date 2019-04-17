using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class whenReachTheGoalWin : MonoBehaviour
{

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name=="Winplate") {
            SceneManager.LoadScene("LINE RENDERING TEST");
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
