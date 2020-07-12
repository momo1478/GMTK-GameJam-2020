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
    public ProjectileData? data = null;

    public int destroyAfterSeconds = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // For now?
        Destroy(this.gameObject, destroyAfterSeconds);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (data != null)
        {
            rb.velocity = data.Value.velocity;
        }
        
    }
}
