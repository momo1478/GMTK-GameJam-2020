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
    public ProjectileData? data = null;

    public int destroyAfterSeconds = 60;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // For now?
        StartCoroutine(DestroyOutsideofBounds());
    }

    private IEnumerator DestroyOutsideofBounds()
    {
        float duration = 0f;
        while(duration < destroyAfterSeconds && new Rect(-65, -65, 150, 150).Contains(gameObject.transform.position))
        {
            duration += 1;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
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
        if (transform.position.x > 50 || transform.position.x < -50 ||
            transform.position.y > 50 || transform.position.y < -50 )
        {

        }
    }
}
