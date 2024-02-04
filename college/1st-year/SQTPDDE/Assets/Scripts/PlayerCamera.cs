using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivityX = 250f;
    
    [SerializeField]
    private float sensitivityY = 250f;
    
    [SerializeField]
    private Transform skeleton;
    
    [SerializeField]
    private Transform orientation;
    
    [SerializeField]
    private MoveCamera moveCamera;

    private float rotationX;
    private float rotationY;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (skeleton == null)
            return;
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;
        
        if (!moveCamera.IsInFPSMode)
        {
            mouseY = Mathf.Clamp(mouseY, -35f, 60f);
            transform.LookAt(skeleton);
        }

        rotationY += mouseX;
        rotationX -= mouseY;

        if (!moveCamera.IsInFPSMode)
            rotationX = orientation.rotation.x;
            
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
        skeleton.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
