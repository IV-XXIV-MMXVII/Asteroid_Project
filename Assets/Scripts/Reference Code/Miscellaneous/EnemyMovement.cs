using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 direction;

    public Vector3 originPosition;

    // Use this for initialization
    void Awake()
    {
        direction = new Vector3(1, 0, 0);
        originPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            Follow(gameManager.instance.player);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player")//this references the exact game object if it hits the enemy or the player
        {
            gameManager.instance.activeEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!gameManager.instance.removingEnemies)
        {
            if (other.gameObject.tag == "Board")
            {
                gameManager.instance.activeEnemies.Remove(this.gameObject);//if its out of the scene this destroys object. 
                Debug.Log("Astroid got destoryed!!!");
                Destroy(this.gameObject);
            }
        }
    }

    void Follow(GameObject target)
    {
        direction = new Vector3(1, 0, 0);
        transform.Translate(direction * Time.deltaTime * gameManager.instance.enemySpeed);//moves forward 
        direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, gameManager.instance.enemyShipRotation * Time.deltaTime);
        transform.Translate(Time.deltaTime * gameManager.instance.enemySpeed, 0, 0);
    }//gradually hones in on player its a honing device pretty much 
}

