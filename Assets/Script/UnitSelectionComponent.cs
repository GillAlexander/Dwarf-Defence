using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour {
    bool isSelecting = false;
    Vector3 mousePosition1;
    public GameObject selectionCirclePrefab;
    public List<NavMeshAgent> agents;

    void Start() {

    }
    public List<SelectableUnitComponent> selectedObjects;

    void Update() {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0)) {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>()) {
                if (selectableObject.selectionCircle != null) {
                    Destroy(selectableObject.selectionCircle.gameObject);
                    selectableObject.selectionCircle = null;
                }
            }
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0)) {
            selectedObjects = new List<SelectableUnitComponent>();
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>()) {
                if (IsWithinSelectionBounds(selectableObject.gameObject)) {
                    selectedObjects.Add(selectableObject);
                }
            }
            agents = new List<NavMeshAgent>();
            foreach (var agent in FindObjectsOfType<NavMeshAgent>()) {
                if (IsWithinSelectionBounds(agent.gameObject)) {
                    if (agent.GetComponent<TreasureChest>()?.GetMyState() == TreasureChest.treasureStates.DefenceMode) {
                        continue;
                    }
                    if (agent.GetComponent<Dwarf>()?.GetMyState() == Dwarf.dwarfMajorStates.FollowMode) {
                        continue;
                    }
                    agents.Add(agent);
                }
            }

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if (isSelecting) {
            foreach (SelectableUnitComponent selectableObject in FindObjectsOfType<SelectableUnitComponent>()) {
                if (IsWithinSelectionBounds(selectableObject.gameObject)) {
                    if (selectableObject.selectionCircle == null) {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
                else {
                    if (selectableObject.selectionCircle != null) {
                        Destroy(selectableObject.selectionCircle.gameObject);
                        selectableObject.selectionCircle = null;
                    }
                }
            }
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject) {
        if (!isSelecting)
            return false;
        var camera = Camera.main;
        var viewportBounds = GUIScript.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    void OnGUI() {
        if (isSelecting) {
            // Create a rect from both mouse positions
            var rect = GUIScript.GetScreenRect(mousePosition1, Input.mousePosition);
            GUIScript.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            GUIScript.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }


    }
}