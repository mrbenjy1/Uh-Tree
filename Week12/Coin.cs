using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour  // Changed class name from Enemy to Coin
{
    // Remove explosionPrefab - coins typically don't explode

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")  // Only check for Player collision
        {
            gameManager.AddScore(1);  // Add 1 point instead of 5
            Destroy(this.gameObject);  // Destroy the coin
        }
    }
}
