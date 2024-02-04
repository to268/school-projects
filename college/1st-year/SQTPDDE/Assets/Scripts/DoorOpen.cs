using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    private Transform rayPoint;
    
    [SerializeField]
    private LayerMask playerMask;

    [SerializeField]
    private float checkDistance = 25f;
    
    [SerializeField]
    private String bossKey;
    
    private Animator animator;
    private bool isNearDoor;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (bossKey == "FinnalBoss")
        {
            if (!(GameData.GetHasBeatenBoss("FireBoss") && GameData.GetHasBeatenBoss("WindBoss") &&
                  GameData.GetHasBeatenBoss("EarthBoss") && GameData.GetHasBeatenBoss("WaterBoss")))
                Destroy(this);
        }

        if (GameData.GetHasBeatenBoss(bossKey))
            Destroy(this);
        
        isNearDoor = Physics.Raycast(rayPoint.position, rayPoint.forward, checkDistance, playerMask);

        
        if (isNearDoor)
            animator.SetBool("isOpened", true);
        else
            animator.SetBool("isOpened", false);
    }
}
