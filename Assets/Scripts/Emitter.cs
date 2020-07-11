using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{

    public float rate = 10;
    public float speed = 5;

    public bool randomAngle = true;
    public float minAngle = 0f;
    public float maxAngle = 360f;

    public Pool pool;

    public Projectile projectile;

    private float tillNextAction = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 GetVelocity(float speed, float degree)
    {
        float radians = degree * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * speed;
    }

    public void SpawnBullet()
    {
        Projectile clone = Instantiate(projectile, transform.position, transform.rotation);
        float angle = Random.Range(minAngle, maxAngle);
        clone.data = new Projectile.ProjectileData(GetVelocity(speed, angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (tillNextAction > 1.0f/rate)
        {
            SpawnBullet();
            tillNextAction = 0f;
        }
        else
        {
            tillNextAction += Time.deltaTime;
        }
    }
}
