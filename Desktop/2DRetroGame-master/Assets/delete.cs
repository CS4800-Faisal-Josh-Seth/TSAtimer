using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class delete : MonoBehaviour
{
    public Light2D light2D;

    private float startingRadius = 0.1f;
    private float targetRadius = 1.5f;
    private float duration = 0.2f;
    private float elapsedTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyGO());

        StartCoroutine(IncreaseLightRadius());

    }


    public IEnumerator destroyGO()
    {
        yield return new WaitForSeconds(.2f);

        Destroy(gameObject);
    }

    IEnumerator IncreaseLightRadius()
    {
        light2D.pointLightOuterRadius = startingRadius;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float radius = Mathf.Lerp(startingRadius, targetRadius, t);
            light2D.pointLightOuterRadius = radius;
            yield return null;
        }

        light2D.pointLightOuterRadius = targetRadius;
        Destroy(gameObject);
    }

}
