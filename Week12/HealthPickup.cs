using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I used enemy logic as the base for my pickup, spawning logic is based in the gameManager
public class EnemyHealth : Enemy
{
    int num1 = 0;
    private int timer = 1;
    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //removes health pickup after time provided
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Bounce(), 0, 0) * Time.deltaTime * 3f);
        if (timer == 1)
        {
        //logic for despawning goes here
        }
    }

    int Bounce()
    {
        //checks if enemy isnt moving sideways
        if (num1 == 0)
        {
            num1 = Random.Range(-10, 10);
            if (num1 > 0)
                num1 = 1;
            else
                num1 = -1;
        }
        if (transform.position.x >= 9)
        {
            num1 = -1;
        }
        else
        {
            if (transform.position.x <= -9)
            {
                num1 = 1;
            }
        }
        return num1;

    }




    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().GainALife();
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } 
        //This could potentially be chahnged so that shooting a health pickup does something
        //else if(whatDidIHit.tag == "Weapons")
        //{
        //    Destroy(whatDidIHit.gameObject);
        //    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //    gameManager.AddScore(5);
        //    Destroy(this.gameObject);
        //}
    }
}
