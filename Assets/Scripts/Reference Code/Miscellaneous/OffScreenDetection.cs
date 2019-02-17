using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OffScreenDetection : MonoBehaviour
{

    public Transform targetPoint;
    public GameObject Player;
   //grabs player position and game object  
    private Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();//grabs camera component 
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (GameObject.Find("Player") != null)
            {
                Vector3 screenPoint = camera.WorldToViewportPoint(targetPoint.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;//if player is on screen or not. 

                if (!onScreen)
                {
                    if (gameManager.instance.lives > 0)
                    {
                        gameManager.instance.lives--;
                        Player.transform.position = SpriteMovement.originalPosition;
                        Debug.Log("You now have a total of " + gameManager.instance.lives + " lives.");//how many lives the player has 
                    }
                    else
                    {
                        Debug.LogWarning("Player Ship got destroy by being out of camera view!!!!");//player destroyed for being out of camera view 
                        Destroy(Player);
                    }
                }
            }
        } catch
        {
            //Do nothing
        }
    }
}