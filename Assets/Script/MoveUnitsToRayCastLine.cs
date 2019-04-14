using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitsToRayCastLine : MonoBehaviour {
    DrawLineWithRayCast list;
    UnitSelectionComponent unit;


    void Start() {
        list = GetComponent<DrawLineWithRayCast>();
        unit = GetComponent<UnitSelectionComponent>();
    }

    void Update() {
        if (list.positionList.Count == 0) {
            //for (int i = 0; i < unit.selectedObjects.Count; i++) {
            //    //unit.selectedObjects;
            //}
            Debug.Log("The list is empty");
        }
        else {
            Debug.Log("The list is not empty");
        }
        if (list.readySetGo == false) {

        }
        else {
            foreach (var selectableObject in unit.selectedObjects) {

                for (int i = 0; i < list.positionList.Count; i++) {
                    unit.agents[i].SetDestination(list.positionList[i]);
                }
            }
        }
    }
}
