using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private GameLevel gameLevel;
    private float rotationSpeed;
    private Rigidbody2D rb;
    public float speed;
    private Vector2 direction;
    private SpaceShip ship;
    private float shift;
    public bool astrd1, astrd2, astrd3, astrd4;
    public GameObject brokenA1, brokenA2, brokenA3, brokenA4;
    void Start()
    {
        gameLevel = FindObjectOfType<GameLevel>();
        rb = GetComponent<Rigidbody2D>();
        ship = GameObject.FindObjectOfType<SpaceShip>();

        direction = ship.transform.position - transform.position;
        shift = Random.Range(-7, 7);
        rb.AddForce(new Vector2(direction.x+shift , direction.y+shift) * speed);

        rotationSpeed = Random.Range(-25, 25);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        checkPosition();
    }

    void checkPosition()
    {
        if(transform.position.x>10 || transform.position.x<-10)
        {
            Destroy(gameObject);
        }

        if (transform.position.y > 6.5 || transform.position.y < -6.5)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name=="bullet(Clone)")
        {
            Destroy(col.gameObject);

            if (astrd1)
            {
                ScoreManager.score += 3;
               GameObject obj= Instantiate(brokenA1, transform.position, Quaternion.identity);
                Destroy(obj, 2f);
            }
            else if (astrd2)
            {
                ScoreManager.score += 7;
                GameObject obj=Instantiate(brokenA2, transform.position, Quaternion.identity);
                Destroy(obj, 2f);
            }
            else if (astrd3)
            {
                ScoreManager.score += 10;
                GameObject obj= Instantiate(brokenA3, transform.position, Quaternion.identity);
                Destroy(obj, 2f);
            }
            else if (astrd4)
            {
                ScoreManager.score += 15;
                GameObject obj= Instantiate(brokenA4, transform.position, Quaternion.identity);
                Destroy(obj, 2f);
            }

            Destroy(gameObject);
            gameLevel.LevelMessage();
        }
    }
}
