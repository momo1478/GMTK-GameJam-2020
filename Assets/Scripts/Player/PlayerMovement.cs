using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    [Range(1f,50f)]
    public float MovementSpeed = 20f;
    [Range(1f,10f)]
    public float DashMultiplierMax = 4f;
    [Range(0f, 1f)]
    public float DashMultiplierDecay = 0.04f;

    Rigidbody2D rig;
    float dashMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: Cooldown for dash
            StartCoroutine(HandleDashMultiplier());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var posX = Input.GetAxis("Horizontal");
        var posY = Input.GetAxis("Vertical");

        var motion = new Vector3(posX, posY, 0);
        
        rig.MovePosition(transform.position + motion.normalized * (MovementSpeed * dashMultiplier) * Time.deltaTime);
    }

    IEnumerator HandleDashMultiplier()
    {
        // Max Dash Multiplier
        dashMultiplier = DashMultiplierMax;

        // While there is a dash bonus.
        while (dashMultiplier > 1f)
        {
            dashMultiplier = Mathf.Lerp(dashMultiplier, 1f - 0.05f, DashMultiplierDecay);
            yield return new WaitForEndOfFrame();
        }
        dashMultiplier = 1f;
    }
}
