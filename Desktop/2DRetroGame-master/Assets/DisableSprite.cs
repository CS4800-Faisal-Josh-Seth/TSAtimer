using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableSprite : MonoBehaviour
{

    public float startTime;


    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        
        TilemapRenderer tilemapRenderer = gameObject.GetComponent<TilemapRenderer>();
        Material material = tilemapRenderer.material;
        Color color = material.color;

        while (Time.time < startTime + 3f)
        {
            color.a = Time.time / 3;
            material.color = color; 

        }
    }
}
