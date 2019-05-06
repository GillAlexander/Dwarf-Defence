﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitsToRayCastLine : MonoBehaviour {
    DrawLineWithRayCast list;
    UnitSelectionComponent unit;
    SelectableUnitComponent component;

    void Start() {
        list = GetComponent<DrawLineWithRayCast>();
        unit = GetComponent<UnitSelectionComponent>();
        component = GetComponent<SelectableUnitComponent>();
    }

    void Update() {
        foreach (var selectableObject in unit.selectedObjects)
        {
            for (int i = 0; i < list.positionList.Count; i++)
            {
                unit.agents[i].SetDestination(list.positionList[i]);
            }
        }
    }
}