using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    private int time = 0;
    private IEnumerator coroutine;

    void Awake()
    {
        coroutine = DestroyObjectTime();//wait for a duration of time 
    }

    void Update()
    {
        if (gameObject.activeInHierarchy) {
            time++;
            Debug.Log("Seconds passed: " + time);//lets person know how many seconds pass.
            if (time > 60)
                Destroy(gameObject);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator DestroyObjectTime()
    {
        yield return new WaitForSeconds(1);//waits for 1 second
    }
}
