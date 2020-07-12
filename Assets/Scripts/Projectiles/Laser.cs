using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float shadowTransparency = .5f;
    public float shadowDuration = 1f;
    public float scaleDuration = 10f;
    public float scaleSpeed = 5000f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateLaser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AnimateLaser()
    {
        Vector2 vectorScale = new Vector2(transform.localScale.x * scaleSpeed, transform.localScale.y);
        // shadow laser
        GetComponent<BoxCollider2D>().enabled = false;
        var sR = GetComponent<SpriteRenderer>();
        var baseColor = sR.color;
        var color = new Color(sR.color.r, sR.color.g, sR.color.b, shadowTransparency);
        sR.color = color;
        var oldScale = transform.localScale;
        transform.localScale = vectorScale;
        yield return new WaitForSeconds(shadowDuration);
        transform.localScale = oldScale;
        // fire laser
        sR.color = baseColor;
        GetComponent<BoxCollider2D>().enabled = true;
        float curDuration = 0f;
        while (curDuration < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, vectorScale, Time.deltaTime * 10);
            curDuration += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
