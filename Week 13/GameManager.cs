using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyThreePrefab;
    public GameObject cloudPrefab;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject healthPrefab;
    public GameObject coin;
    public GameObject powerupPrefab;
    public GameObject audioPlayer;

    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public AudioClip coinSound;
    public AudioClip healthSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;
    
    public float horizontalScreenSize;
    public float verticalScreenSize;




    public int score;
    public int cloudMove;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        cloudMove = 1;
        gameOver = false;
        AddScore(0);
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateEnemyTwo", 1, 7.5f);
        InvokeRepeating("CreateEnemyThree", 1, 4.5f);
        InvokeRepeating("CreateHealth", 1, 12f);
        InvokeRepeating("CreateCoin", 1, 10f);
        StartCoroutine(SpawnPowerup());
        powerupText.text = "No powerups yet!";
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateEnemyThree()
    {
        Instantiate(enemyThreePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateHealth()
    {
        //values based on players restricted move zone instead
        Instantiate(healthPrefab, new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 0.9f), 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateCoin()
    {
        //values based on players restricted move zone instead
        Instantiate(coin, new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 0.9f), 0), Quaternion.Euler(180, 0, 0));
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 0.9f), 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }


    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No powerups yet!";
                break;
        }
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5); 
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
            case 3:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(coinSound);
                break;
            case 4:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(healthSound);
                break;

        }
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMove = 0;
    }

}
