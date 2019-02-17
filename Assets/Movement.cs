using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public static Vector3 startPosition;

    //initializing speed and speed of ratation
    [HideInInspector] public float speed, rotationSpeed;
    public float maxSpeed, maxRotationSpeed; //max speed also for rotation speed
    public float rateOfSpeed, rateOfRotation;//the amount of speed gained over time

    public bool enableRevertControl = true;


    private float clockwise;//reads values 1 & -1; -1 is for counter clockwise 1 is oposite 
    private float rotation;// rotation controls

    private bool throttleDown, reverseThrottleDown;

    private Rigidbody2D rb;//Identifier 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        switch (enableRevertControl)
        {
            case false:

                Rotate(-rotation);

                if (Input.GetKey(KeyCode.W))
                    Throttle();
                else if (Input.GetKey(KeyCode.S))
                    ReverseThrottle();

                    break;

            case true:

                Rotate(-rotation);

                if (Input.GetKey(KeyCode.W))
                    ReverseThrottle();
                else if (Input.GetKey(KeyCode.S))
                    Throttle();

                break;
        }
    }
    float Rotate(float dir)
    {
        if (Mathf.Abs(maxRotationSpeed) < Mathf.Abs(maxRotationSpeed))
            rotationSpeed += rateOfRotation; // the rotation speed does not have a number assigned

        transform.Rotate(dir * Vector3.forward, rotationSpeed);

        return dir;

    }
    void Throttle()
    {
        switch (enableRevertControl)
        {
            case false:
                throttleDown = Input.GetKey(KeyCode.W);
                break;
            case true:
                throttleDown = Input.GetKey(KeyCode.S);
                break;
        }

        if (throttleDown == true)
        {

            if (Mathf.Abs(speed) * -1 > Mathf.Abs(maxSpeed) * -1)
                speed += rateOfSpeed;

            rb.AddForce(speed * (transform.localRotation * new Vector2(rotationSpeed, 0f)));
        }

        else
        {
            if (Mathf.Abs(speed) * -1 < 0)
                speed -= rateOfSpeed * Mathf.Sign(speed);
        }
        //pushes the ship forward 
    }

    void ReverseThrottle()
    {
        switch (enableRevertControl)
        {
            case false:
                reverseThrottleDown = Input.GetKey(KeyCode.S);
                break;
            case true:
                reverseThrottleDown = Input.GetKey(KeyCode.W);
                break;
        }
        // moving in reverse
        if (reverseThrottleDown == true)



        {
            if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
                speed += rateOfSpeed;

            rb.AddForce(speed * (transform.localRotation * new Vector2(-rotationSpeed, 0f)));

        }
        else
        {
            if (Mathf.Abs(speed) > 0)
                speed -= rateOfSpeed * Mathf.Sign(-speed);
        }
    }

    // Moves the ship in the opposite direction
    /*private void OnTriggerEnter2D(Collider2D varObject)
    {
        if (varObject.tag == "Enemy" || varObject.tag == "Astroid")
        {
            if (gameManager.instance.lives > 0)
            {
                gameManager.instance.lives--;
                gameObject.transform.position = startPosition;
                Debug.Log("You now have a total of" + gameManager.instance.lives + "lives.");
            }
            else
            {
                Debug.LogWarning("Player Ship got Destroyed.");

                DestructionAreaPrefab.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
        }
    }*/
}

