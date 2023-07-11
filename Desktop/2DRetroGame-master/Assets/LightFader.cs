using System.Collections;
using UnityEngine;


public class LightFader : MonoBehaviour
{
    public float fadeDuration = 2.0f; // duration of the fade in seconds
    private UnityEngine.Rendering.Universal.Light2D myLight;

    void Start()
    {
        myLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        StartCoroutine(FadeLight());
    }

    IEnumerator FadeLight()
    {
        float startTime = Time.time;
        float endTime = startTime + fadeDuration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / fadeDuration;
            float intensity = Mathf.Lerp(1.0f, 0.0f, progress);
            myLight.intensity = intensity;
            yield return null;
        }
    }
}
