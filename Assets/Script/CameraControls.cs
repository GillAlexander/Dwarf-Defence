﻿using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
    public float movementSpeed = 0.1f;
    public float rotationSpeed = 4f;
    public float smoothness = 0.85f;
    public float screenBoarderThickness = 10;

    public Quaternion targetRotation;
    float targetRotationY;
    float targetRotationX;
    //float floatY = 1;

    void Start() {
        targetRotation = transform.rotation;
        targetRotationY = transform.localRotation.eulerAngles.y;
        targetRotationX = transform.localRotation.eulerAngles.x;
    }

    void Update() {
        if (Input.GetMouseButton(2)) {
            Cursor.visible = false;
            targetRotationY += Input.GetAxis("Mouse X") * rotationSpeed;
            targetRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;
            targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
        }
        else
            Cursor.visible = true;

        //transform.position = Vector3.Lerp(transform.position, targetPosition, (1.0f - smoothness));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (1.0f - smoothness));

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - screenBoarderThickness) {
            transform.position += Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up) * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.ProjectOnPlane(-Camera.main.transform.right, Vector3.up) * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= screenBoarderThickness) {
            transform.position += Vector3.ProjectOnPlane(-Camera.main.transform.forward, Vector3.up) * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up) * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q)) {
            transform.position -= transform.up * movementSpeed;
        }
            
        if (Input.GetKey(KeyCode.E)) {
            transform.position += transform.up * movementSpeed;
        }

        if (Input.mousePosition.x >= Screen.width - screenBoarderThickness) {
            targetRotationY++;
            targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
        }

        if (Input.mousePosition.x <= screenBoarderThickness) {
            targetRotationY--;
            targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
        }

    }
}
