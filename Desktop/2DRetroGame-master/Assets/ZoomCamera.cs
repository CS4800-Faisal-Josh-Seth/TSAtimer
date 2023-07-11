using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomCamera : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomSpeed = 0.1f;
    public float minOrthoSize = 5f;
    public float maxOrthoSize = 10f;
    public string playerTag = "Player";

    private GameObject player;
    private float lastGroundedY;

    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag(playerTag); // find the player game object by tag
        }

        if (player != null)
        {
            if (PlayerController.grounded)
            {
                lastGroundedY = player.transform.position.y;
            }

            float distanceFromGround = Mathf.Abs(player.transform.position.y - lastGroundedY);
            float targetOrthoSize = Mathf.Clamp(minOrthoSize + distanceFromGround, minOrthoSize, maxOrthoSize);

            // Update the orthographic size of the CinemachineVirtualCamera
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, targetOrthoSize, zoomSpeed);
        }
    }
}
