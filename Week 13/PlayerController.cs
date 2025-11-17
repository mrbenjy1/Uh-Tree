using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    //shield 0 = no shield shield =1 is yes shield
    private int shield = 0;
    private float speed;
    private int weaponType;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;
   // private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 1f;
    private float verticalScreenbottomLimit = -3.5f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        weaponType = 1;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        if (shield == 1)
        {
            shield = 0;
            shieldPrefab.SetActive(false);
        }
            
        else
        {
            //Do I have a shield? If yes: do not lose a life, but instead deactivate the shield's visibility
            //If not: lose a life
            //lives = lives - 1;
            //lives -= 1;
            lives--;
            gameManager.ChangeLivesText(lives);
            if (lives == 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                gameManager.GameOver();
                Destroy(this.gameObject);
            }
        }
        
    }
    public void GainALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        gameManager.PlaySound(4);
        if (lives < 3)
        {
            lives++;
            gameManager.ChangeLivesText(lives);
        }
        else
        if (lives >= 3)
        {
            gameManager.AddScore(1);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    // Picked up speed
                    speed = 10f;
                    thrusterPrefab.SetActive(true);

                    // Local coroutine (unique name)
                    IEnumerator ThrusterPowerDown()
                    {
                        yield return new WaitForSeconds(5f);
                        speed = 5f;                      // reset speed
                        thrusterPrefab.SetActive(false); // turn off thruster
                    }

                    StartCoroutine(ThrusterPowerDown());

                    gameManager.ManagePowerupText(1);
                    break;
                case 2:
                    weaponType = 2; //Picked up double weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3; //Picked up triple weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    //Picked up shield
                    shield = 1;
                    //Do I already have a shield?
                    //If yes: do nothing
                    //If not: activate the shield's visibility
                    StartCoroutine(SpeedPowerDown());
                    shieldPrefab.SetActive(true);
                   //hrusterPrefab.SetActive(false);
                    gameManager.ManagePowerupText(4);

                    //waiting
                    IEnumerator SpeedPowerDown()
                    {
                        yield return new WaitForSeconds(5f);
                        shield = 0;
                        shieldPrefab.SetActive(false);
                    }
                    break;
            }
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
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
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

    }
}
