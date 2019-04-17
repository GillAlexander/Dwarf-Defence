using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineWithMouse : MonoBehaviour {

    //public Camera camera;
    public Material lineMaterial;
    public float lineWidth;
    public float depth = 5;

    private Vector3? lineStartPoint = null;
    void Start() {
        GetComponent<Camera>().GetComponent<Camera>();
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            lineStartPoint = GetMouseCameraPoint();



            
        }
        else if (Input.GetMouseButtonUp(1)) {
            Debug.Log("Release Mouse Button");
            if (!lineStartPoint.HasValue) {
                return;
            }


            var lineEndPoint = GetMouseCameraPoint();
            var GameObject = new GameObject();
            var lineRenderer = GameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            //lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { lineStartPoint.Value, lineEndPoint });
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 20;
            lineStartPoint = null;
        }
    }

    private Vector3 GetMouseCameraPoint() {
        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;

    }
}
