using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Emitter : MonoBehaviour {
    public float spawnRate; // proj/sec
    public float rotationRate = 100; // degree/sec
    public float projectileSpeed;

    public float angle = 0f;
    public float angleSpray = 45f;

    // Pulse
    public float pulseSize;

    private GameObject player;

    public enum Behavior {
        Spin,
        Random,
        Oscillate,
        Pulse,
        Aimed
    }

    public Behavior behavior = Behavior.Spin;

    public Projectile projectile;

    private float tillNextAction = 0f;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static Vector3 GetVelocity(float speed, float degree) {
        float radians = degree * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * speed;
    }

    public void SpawnBullet() {
        Projectile clone = null;
        switch (behavior) {
            case Behavior.Spin:
                clone = Instantiate(projectile, transform.position, transform.rotation);
                float spinOffset = Time.time * rotationRate % 360f;
                clone.data = new Projectile.ProjectileData(GetVelocity(projectileSpeed, spinOffset));
                break;
            case Behavior.Oscillate:
                clone = Instantiate(projectile, transform.position, transform.rotation);
                float offset = Mathf.PingPong(Time.time * rotationRate, angleSpray);
                clone.data =
                    new Projectile.ProjectileData(GetVelocity(projectileSpeed, angle - angleSpray / 2 + offset));
                break;
            case Behavior.Pulse:
                for (int i = 0; i < pulseSize; i++) {
                    clone = Instantiate(projectile, transform.position, transform.rotation);
                    float curAngle = angleSpray / pulseSize * i;
                    clone.data =
                        new Projectile.ProjectileData(GetVelocity(projectileSpeed, angle - angleSpray / 2 + curAngle));
                }

                break;
            case Behavior.Random:
                clone = Instantiate(projectile, transform.position, transform.rotation);
                float ranAngle = Random.Range(angle - angleSpray / 2, angle + angleSpray / 2);
                clone.data = new Projectile.ProjectileData(GetVelocity(projectileSpeed, ranAngle));
                break;
            case Behavior.Aimed:
                clone = Instantiate(projectile, transform.position, transform.rotation);
                var aimedVector = (Vector2) transform.position -
                                  ((Vector2) player.transform.position + (Random.insideUnitCircle * 5));
                clone.data = new Projectile.ProjectileData(aimedVector.normalized *
                                                           Random.Range(projectileSpeed- 2f,
                                                               projectileSpeed + 2f));
                break;
            default:
                break;
        }

        clone.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update() {
        float localRate = spawnRate;
        if (behavior == Behavior.Pulse) {
            localRate = localRate / pulseSize;
        }

        if (tillNextAction > 1.0f / localRate) {
            SpawnBullet();
            tillNextAction = 0f;
        }
        else {
            tillNextAction += Time.deltaTime;
        }
    }
}