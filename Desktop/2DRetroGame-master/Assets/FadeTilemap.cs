using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeTilemap : MonoBehaviour
{
    public TilemapRenderer tilemapRenderer; // Reference to TilemapRenderer component
    
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        Material material = tilemapRenderer.material;
        Color color = material.color;
        color.a = 0;
        material.color = color; //start off at 0%
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Material material = tilemapRenderer.material;
        Color color = material.color;
        float elapsedTime = Time.time - startTime; // Calculate elapsed time
        float alpha = Mathf.Clamp01(elapsedTime / 3f); // Calculate alpha value between 0 and 1
        color.a = alpha;
        material.color = color;
    }
}