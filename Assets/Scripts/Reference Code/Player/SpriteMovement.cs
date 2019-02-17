using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{

    public static Vector3 originalPosition;

    //Initializing speed and the speed of rotation
    [HideInInspector] public float speed, rotationSpeed; //Current Speed and Rotation Speed
    public float maxSpeed, maxRotationSpeed; //The Max Speed and the Max Speed of Rotation
    public float rateOfSpeed, rateOfRotation; //The amount of speed and rotation speed you gain over time.

    public bool enableRevertControl = true;
    

    private float clockwise; //Reads values 1 and -1; -1 for counter-clockwise, 1 for clockwise rotation
    private float rotation; //Rotational controls: It will return the value of the controller or keyboard (-1, 0, 1)

    private bool ThrottleDown, reverseThrottleDown;

    private Rigidbody2D rb; //Giving an identifier (or name) that'll reference our RigidBody!!

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Grab the component of the local RigidBody
        originalPosition = gameObject.transform.position;
    }

    void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal"); //The left, right, A, or D key
    }

    void FixedUpdate()
    {
        switch (enableRevertControl) {
            case false:

                Rotate(-rotation); //The variable r is used as a parameter for the Rotate function

                if (Input.GetKey(KeyCode.W))
                    Propel();
                else if (Input.GetKey(KeyCode.S))
                    Reverse();

                break;

            case true:

                Rotate(rotation);

                if (Input.GetKey(KeyCode.W))
                    Reverse();
                else if (Input.GetKey(KeyCode.S))//inputes for the S key
                    Propel();

                break;

        }
    }

    float Rotate(float dir)
    {
        

        if (Mathf.Abs(rotationSpeed) < Mathf.Abs(maxRotationSpeed))
        {
            rotationSpeed += rateOfRotation; //The rotation speed is not assigned a number (except perhaps 0).
                                             //It will increment by the rateOfRotation times dir (which has values of 1, -1)
        }


        transform.Rotate(dir * Vector3.forward, rotationSpeed); //We then update the rotation of the GameObject every frame, the rotationSpeed being how fast the rotation is.

        return dir; //Retunring the dir for the Update function.
    } //Controls the rotation of the player

    void Propel()
    {
        switch (enableRevertControl)
        {
            case false:
                ThrottleDown = Input.GetKey(KeyCode.W);
                break;
            case true:
                ThrottleDown = Input.GetKey(KeyCode.S);
                break;
        }//inputes for (W) and (S). 

        //Going fowards
        if (ThrottleDown == true)
        {

            if (Mathf.Abs(speed) * -1 > Mathf.Abs(maxSpeed) * -1)
                speed += rateOfSpeed;


            rb.AddForce(speed * (transform.localRotation * new Vector2(rotationSpeed, 0f)));

        }
        else
        {
            if (Mathf.Abs(speed) * -1 < 0)
                speed -= rateOfSpeed * Mathf.Sign(speed);//slowes down when key is released 
        }
    } //Moves the player sprite foward
    
    void Reverse()
    {
        switch(enableRevertControl)
        {
            case false:
                reverseThrottleDown = Input.GetKey(KeyCode.S);
                break;
            case true:
                reverseThrottleDown = Input.GetKey(KeyCode.W);
                break;
        }//revert botton for going backwards 

        //moving sprite backwards 
        if (reverseThrottleDown == true) {

            if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
                speed += rateOfSpeed;


            rb.AddForce(speed * (transform.localRotation * new Vector2(-rotationSpeed, 0f)));//adds force and applys rotation

        } else
        {
            if (Mathf.Abs(speed) > 0)
                speed -= rateOfSpeed * Mathf.Sign(-speed);
        }
    } //Noves in opposite direction 


    private void OnTriggerEnter2D(Collider2D varProp)
    {
        if (varProp.tag == "Enemy" || varProp.tag == "Asteroid")//if player is hitting enemy or astroid
        {
            if (gameManager.instance.lives > 1)
            {
                --gameManager.instance.lives;
                gameObject.transform.position = originalPosition;
                Debug.Log("You now have a total of " + gameManager.instance.lives + " lives.");//lets dev know you have a total amount of lives.
            }
            else
            {
                Debug.LogWarning("Player Ship got Destoryed.");//lets you know player ship is destroyed 
                Destroy(gameObject);
            }
        }
    }
}
