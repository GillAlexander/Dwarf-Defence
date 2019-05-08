using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour {
    public Transform treasureChest;
    public TreasureChest treasureChestScript;

    private GameObject[] trollsArray;
    public List<GameObject> trollsList;
    public List<Transform> trollTransform = new List<Transform>();
    public List<Troll> trollScriptList = new List<Troll>();

    public GameObject[] dwarfUnitArray;
    public List<GameObject> dwarfUnitList = new List<GameObject>();
    public List<Transform> dwarfUnitTransform = new List<Transform>();
    public List<DwarfUnit> dwarfUnitScriptList = new List<DwarfUnit>();
    
    

    void Start() {
        //Add the enemies we have
        trollsArray = GameObject.FindGameObjectsWithTag("Troll");
        dwarfUnitArray = GameObject.FindGameObjectsWithTag("newDwarf");
        
        trollsList.AddRange(trollsArray);
        dwarfUnitList.AddRange(dwarfUnitArray);

        for (int i = 0; i < dwarfUnitList.Count; i++)
        {
            dwarfUnitTransform.Add(dwarfUnitList[i].transform);
            dwarfUnitScriptList.Add(dwarfUnitList[i].GetComponent<DwarfUnit>());
        }
        for (int i = 0; i < trollsList.Count; i++)
        {
            trollTransform.Add(trollsList[i].transform);
            trollScriptList.Add(trollsList[i].GetComponent<Troll>());
        }
    }

    void Update() {
        //if (Time.frameCount % 3)
        //{

        //}
        treasureChestScript.UpdateState(trollTransform);

        for (int i = 0; i < trollsList.Count; i++)
        {
            trollTransform[i] = trollsList[i].transform;
        }

        //Remove object from lists if the object is dead
        for (int i = 0; i < trollScriptList.Count; i++)
        {
            if (trollScriptList[i].isDead == true)
            {
                trollScriptList.RemoveAt(i);
                trollTransform.RemoveAt(i);
                trollsList.RemoveAt(i);
            }
        }
        for (int i = 0; i < dwarfUnitScriptList.Count; i++)
        {
            if (dwarfUnitScriptList[i].isDead == true)
            {
                dwarfUnitScriptList.RemoveAt(i);
                dwarfUnitTransform.RemoveAt(i);
                dwarfUnitList.RemoveAt(i);
            }
        }

        //Update all enemies to see if they should change state and move/attack player
        for (int i = 0; i < trollScriptList.Count; i++)
        {
            trollScriptList[i].UpdateState(treasureChest, dwarfUnitTransform);
        }
        for (int i = 0; i < dwarfUnitScriptList.Count; i++)
        {
            dwarfUnitScriptList[i].UpdateState(treasureChest, trollTransform);
        }
    }

    private void OnGUI() {
        //if (GUI.Button(new Rect(10, 450, 150, 100), "I am a button"))
        //{
        //    print(trollScriptList);
        //}
    }
}

//void Start() {
//    //Add the enemies we have
//    trollsArray = GameObject.FindGameObjectsWithTag("Troll");
//    dwarfsArray = GameObject.FindGameObjectsWithTag("Dwarf");

//    dwarfsList.AddRange(dwarfsArray);
//    trollsList.AddRange(trollsArray);

//    for (int i = 0; i < dwarfsArray.Length; i++)
//    {
//        dwarfTransform.Add(dwarfsList[i].transform);
//        dwarfScriptList.Add(dwarfsList[i].GetComponent<Dwarf>());
//    }
//    for (int i = 0; i < trollsArray.Length; i++)
//    {
//        trollTransform.Add(trollsList[i].transform);
//        trollScriptList.Add(trollsList[i].GetComponent<Troll>());
//    }
//}

//void Update() {
//    for (int i = 0; i < dwarfsList.Count; i++)
//    {
//        if (dwarfsList[i] != null)
//        {
//            dwarfTransform[i] = dwarfsList[i].transform;
//        }
//        else
//        {
//            dwarfsList.RemoveAt(i);
//            Debug.Log("Debuged list");
//        }
//    }
//    for (int i = 0; i < trollsList.Count; i++)
//    {
//        if (trollTransform[i] != null)
//        {
//            trollTransform[i] = trollsList[i].transform;
//        }
//        else
//        {
//            trollTransform.RemoveAt(i);
//        }
//    }

//    //Update all enemies to see if they should change state and move/attack player
//    for (int i = 0; i < trollScriptList.Count; i++)
//    {
//        if (trollScriptList[i] != null)
//        {
//            trollScriptList[i].UpdateState(treasureChest, dwarfTransform);
//        }
//        else
//        {
//            trollScriptList.RemoveAt(i);
//        }
//    }
//    for (int i = 0; i < dwarfScriptList.Count; i++)
//    {
//        dwarfScriptList[i].UpdateState(treasureChest, trollTransform);
//    }

//    //Null check
//    for (int i = 0; i < trollsList.Count; i++)
//    {
//        if (trollsList[i] == null)
//        {
//            trollsList.RemoveAt(i);
//        }
//    }
//    for (int i = 0; i < trollTransform.Count; i++)
//    {
//        if (trollTransform[i] == null)
//        {
//            trollTransform.RemoveAt(i);
//        }
//    }
//    for (int i = 0; i < trollScriptList.Count; i++)
//    {
//        if (trollScriptList[i] == null)
//        {
//            trollScriptList.RemoveAt(i);
//        }
//    }
//    for (int i = 0; i < dwarfTransform.Count; i++)
//    {
//        if (dwarfTransform[i] == null)
//        {
//            dwarfTransform.RemoveAt(i);
//            Debug.Log("Debuged transform");
//        }
//    }
//    for (int i = 0; i < dwarfsList.Count; i++)
//    {
//        if (dwarfsList[i] == null)
//        {
//            dwarfsList.RemoveAt(i);
//            Debug.Log("Debuged list");
//        }
//    }
//    for (int i = 0; i < dwarfScriptList.Count; i++)
//    {
//        if (trollScriptList[i] == null)
//        {
//            dwarfScriptList.RemoveAt(i);
//            Debug.Log("Debuged scriptlist");
//        }
//    }
//}