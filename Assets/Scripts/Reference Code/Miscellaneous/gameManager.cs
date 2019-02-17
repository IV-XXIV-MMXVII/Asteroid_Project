using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject[] spawnPoint;

    public float astroidVelocity;

    public List<GameObject> enemies;

    public float enemySpeed;
    public float enemyShipRotation;

    public List<GameObject> activeEnemies;
    public bool removingEnemies;
    public int maximumNumberOfActiveObstacles;

    public GameObject player;

    public int lives;
    // handles enimy spawn points and controls. also keeps track of lives 
    IEnumerator coroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        activeEnemies = new List<GameObject>();//Spawns enemies 
        removingEnemies = false;
    }

    void Update()
    {
         KeyCommand();
         for (int i = 0; i < maximumNumberOfActiveObstacles; i++)//spawns enemies smaller than the max capacity
            AddEnemy();

        coroutine = WaitForGameEnd(6);

         if (lives < 1)
            StartCoroutine(coroutine);// wait for Game Over
        
    }

    void KeyCommand()
    {
        //quits game
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    void AddEnemy()
    {

        if (activeEnemies.Count < maximumNumberOfActiveObstacles)
        {
            //  spawn point
            int id = Random.Range(0, spawnPoint.Length);
            GameObject point = spawnPoint[id];

            //  enemy to spawn
            GameObject enemy = enemies[Random.Range(0, enemies.Count)];

            // spawn enemy
            GameObject enemyInstance = Instantiate<GameObject>(enemy, point.transform.position, Quaternion.identity);

            if (enemyInstance.GetComponent<Asteroid>() != null)
            {
                Vector2 directionVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                directionVector.Normalize();
                enemyInstance.GetComponent<Asteroid>().direction = directionVector;
            }

            // Add to enemies list
            activeEnemies.Add(enemyInstance);
        }

    }

    public void RemoveEnemies()
    {
        removingEnemies = true;
        for (int i = 0; i < activeEnemies.Count; i++)//erase all enemies 
        {
            Destroy(activeEnemies[i]);
        }
        activeEnemies.Clear();
        removingEnemies = false;
    }

    IEnumerator WaitForGameEnd(int time)
    {
        yield return new WaitForSeconds(time);//Wait for a certain amount of seconds(game over)
        Application.Quit();
        Debug.Log("GAME OVER!!!");
    }
}