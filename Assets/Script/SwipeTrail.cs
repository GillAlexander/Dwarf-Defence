using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    public Plane testPlane;

    public float distance = 50f;
    
    void Start()
    {
        
    }
    
    //void Update()
    //{
    //    if (Input.GetMouseButton(1)) {
    //        Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

    //        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        float rayDistance;
    //        if (testPlane.Raycast(mRay, out rayDistance)) {
    //            this.transform.position = mRay.GetPoint(rayDistance);
    //        }
        
    //    }
    //}

    
}
