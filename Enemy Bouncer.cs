using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{
    int num1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Bounces when touching the corners
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
    // Update is called once per frame
    void Update()
    {
        //f changes speed
        transform.Translate(new Vector3(Bounce(), -0.6f, 0) * Time.deltaTime * 5f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
