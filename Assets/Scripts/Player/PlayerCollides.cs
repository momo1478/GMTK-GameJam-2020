using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollides : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
            print("ded0");
            Destroy(collision.gameObject);
            GameManager.instance.Damage(1);
        }
    }
}
