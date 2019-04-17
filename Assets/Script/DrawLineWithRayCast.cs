using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineWithRayCast : MonoBehaviour {

    public float distance = 50f;
    public List<Vector3> positionList;
    public List<GameObject> debugBallList;
    public GameObject debugBall;
    float time;
    public bool readySetGo = false;
    void Start() {
        //positionList.Clear();
        positionList = new List<Vector3>();
    }

    void Update() {
        time += Time.deltaTime;

        if (time > 0.08f) {
            
            //StartCoroutine(SavePoint());
            GeneratePositions();
            time = 0;
        }

        if (Input.GetMouseButtonUp(1)) {
            //Move the selected units to the generated vector3 positions in the positionslist

            readySetGo = true;
        }
        if (Input.GetMouseButtonDown(0)) {

            positionList.Clear();
            
            //debugBallList.Clear();
            

        }
    }
    
    void GeneratePositions() {
        //if right mouse buttonis pressed instantiate a raycast
        if (Input.GetMouseButton(1)) {
            readySetGo = false;

            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance)) {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);
                //log hit area to the console
                //Debug.Log(hit.point);

                Instantiate(debugBall, hit.point, transform.rotation);
                debugBallList.Add(debugBall);
                positionList.Add(hit.point);
            }
        }


    }

    
}
