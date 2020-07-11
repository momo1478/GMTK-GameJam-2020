using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Emitter : MonoBehaviour
{

    public float spawnRate = 50;    // proj/sec
    public float rotationRate = 100;  // degree/sec
    public float speed = 5;

    public float angle = 0f;
    public float angleSpray = 45f;

    public enum Behavior
    {
        Spin,
        Random,
        Oscillate
    }

    public Behavior behavior = Behavior.Spin;

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
        switch (behavior)
        {
            case Behavior.Spin:
                float spinOffset = Time.time * rotationRate % 360f;
                clone.data = new Projectile.ProjectileData(GetVelocity(speed, spinOffset));
                break;
            case Behavior.Oscillate:
                float offset =  Mathf.PingPong(Time.time * rotationRate, angleSpray);
                clone.data = new Projectile.ProjectileData(GetVelocity(speed, angle - angleSpray/2 + offset));
                break;
            case Behavior.Random:
                float ranAngle = Random.Range(angle - angleSpray/2, angle + angleSpray/2);
                clone.data = new Projectile.ProjectileData(GetVelocity(speed, ranAngle));
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tillNextAction > 1.0f / spawnRate)
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
