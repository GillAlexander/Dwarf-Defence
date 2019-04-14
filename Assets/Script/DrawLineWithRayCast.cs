using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawLineWithRayCast : MonoBehaviour {

    public float distance = 50f;
    public List<Vector3> positionList;
    public GameObject debugBall;
    float time;
    public bool readySetGo = false;
    void Start() {
        //positionList.Clear();
        positionList = new List<Vector3>();
    }

    void Update() {
        time += Time.deltaTime;

        Debug.Log(readySetGo);
        if (time > 0.2) {
            
            //StartCoroutine(SavePoint());
            GeneratePositions();
            time = 0;
        }

        if (Input.GetMouseButtonUp(1)) {
            //Move the selected units to the generated vector3 positions in the positionslist

            readySetGo = true;
            Debug.Log("Move the selected units to the generated vector3 positions in the positionslist");
        }
        if (Input.GetKeyDown("space")) {
            positionList.Clear();
        }
    }

    /*
    IEnumerator SavePoint() {
        
        //yield return new WaitForSeconds(1);
        if (Input.GetMouseButton(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            yield return null;

            if (Physics.Raycast(ray, out hit, distance)) {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);

                Instantiate(debugBall, hit.point, transform.rotation);

                positionList.Add(hit.point);
            }
        }
            
        //positionList.Add(position);
    }
    */

    

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
                positionList.Add(hit.point);
            }
        }


    }

    
}
