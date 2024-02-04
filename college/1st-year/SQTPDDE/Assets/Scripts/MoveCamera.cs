using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform FPSCameraPosition;
    
    [SerializeField]
    private Transform TPSCameraPosition;
    
    private bool isInFPSMode;

    public bool IsInFPSMode
    {
        get { return isInFPSMode; }
    }
    
    void Start()
    {
        transform.position = FPSCameraPosition.position;
        isInFPSMode = true;
    }

    void Update()
    {
        if (FPSCameraPosition == null)
            return;
        
        if (Input.GetKeyDown(KeyCode.V))
            isInFPSMode = !isInFPSMode;

        if (isInFPSMode)
        {
            transform.position = FPSCameraPosition.position;
            transform.rotation = FPSCameraPosition.rotation;
        }
        else
        {
            transform.position = TPSCameraPosition.position;
        }
    }
}
