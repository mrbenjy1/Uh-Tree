using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //how to define a variable
    //1. access modifier: public or private
    //2. data type: int, float, bool, string
    //3. variable name: camelCase
    //4. value: optional

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 1f;
    private float verticalScreenbottomLimit = -3.5f;

    public GameObject bulletPrefab;

    void Start()
    {
        playerSpeed = 6f;
        //This function is called at the start of the game
        
    }

    void Update()
    {
        //This function is called every frame; 60 frames/second
        Movement();
        Shooting();

    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");

        //UNCOMMENT TO REINABLE UP + DOWN MOVEMENT

        //Checks if player is above or below screen limit and allows movement if false
        if (transform.position.y >= verticalScreenLimit && Input.GetAxis("Vertical") > 0)
        {
            verticalInput = 0;
        }
        else if (transform.position.y <= verticalScreenbottomLimit && Input.GetAxis("Vertical") < 0)
        {
            verticalInput = 0;
        }
        else
        {
            verticalInput = Input.GetAxis("Vertical");
        }



        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //Player leaves the screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        //OLD CODE FOR TELEPORTING UP AND DOWN
        //Player leaves the screen vertically 
        //if(transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
       // {
        //    transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
       // }
    }

}
