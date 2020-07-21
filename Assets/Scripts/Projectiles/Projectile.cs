using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public struct ProjectileData
    {
        public Vector3 velocity;

        public ProjectileData(Vector3 velocity) 
        {
            this.velocity = velocity;
        }
    }

    public Rigidbody2D rb;
    public ProjectileData? data;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (data != null)
        {
            rb.velocity = data.Value.velocity;
        }
    }
}
