using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitWithMouse : MonoBehaviour {
    bool isSelecting = false;
    Vector3 mousePosition1;



    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
            Debug.Log("Pressed mouse button");
        }

        if (Input.GetMouseButtonUp(0)) {
            isSelecting = false;
            Debug.Log("Released mouse button");
        }
    }

    void OnGUI() {
        if (isSelecting) {
            var rect = GUIScript.GetScreenRect(mousePosition1, Input.mousePosition);
            GUIScript.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            GUIScript.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }





    public bool IsWithinSelectionBounds(GameObject gameObject) {
        if (!isSelecting) {
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = GUIScript.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        Debug.Log(viewportBounds.Contains(camera.ViewportToWorldPoint(gameObject.transform.position)));
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }





}