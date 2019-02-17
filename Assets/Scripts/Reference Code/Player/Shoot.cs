using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform pointOfFire; //We assign the gameObject that is the child to our Player GameObject
    public GameObject bulletPrefab; //We assign a Prefab to the slot in order to spawn it when we shoot

    public bool automaticMode;
    [Range(1, 20)] public int recoilSpeed;

    private bool isKeyReleased;
    private IEnumerator coroutine;

    void Update()
    {
        //When the player shots lazers
        if (Input.GetKeyDown(KeyCode.Space) || isKeyReleased == true)
        {
            coroutine = Recoil();
            switch (automaticMode) {
                case false:
                    isKeyReleased = false;
                    Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation); //A bullet will spawn with a set direction based on the player's direction
                    break;

                case true:
                    isKeyReleased = false;
                    Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation); //A bullet will spawn with a set direction based on the player's direction
                    StartCoroutine(coroutine);
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && automaticMode == true) StopCoroutine(coroutine);// stop firing 

    }


    private IEnumerator Recoil()
    {
        float value = (float)recoilSpeed;
        yield return new WaitForSeconds(1 / value);//waits until another shot come
        isKeyReleased = true;
    }

}
