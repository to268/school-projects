using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class 
    BoneBounce : MonoBehaviour
{
    [SerializeField]
    private float bounceMultiplier = 5f;
    
    [SerializeField]
    private int maxBounces = 6;

    [SerializeField]
    private float damageAmount = 4;

    [SerializeField]
    [Range(1f, 4f)]
    private float damageMultiplier = 1.2f;
    
    [SerializeField]
    private GameObject bone;

    private float timeSpawn;
    private Rigidbody rb;
    private Vector3 velocity;
    private int bouncesCount;
    private float damage;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bouncesCount = 0;
        damage = damageAmount;
        timeSpawn = Time.unscaledTime;
    }

    void Update()
    {
        if ((Time.unscaledTime - timeSpawn) >= 5f)
            Destroy(bone);
    }

    void FixedUpdate()
    {
        velocity = transform.InverseTransformDirection(rb.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 7)
            collision.gameObject.GetComponent<EnemyAi>().TakeDamage(damage);
        
        if (bouncesCount >= maxBounces)
            Destroy(bone);
        
        Vector3 bounceTarget = collision.contacts[0].normal + (collision.contacts[0].normal - velocity);
        rb.AddForce(bounceTarget * bounceMultiplier);
        
        bouncesCount++;
        damage *= damageMultiplier;
    }
}
