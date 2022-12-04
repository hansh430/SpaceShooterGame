using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float moveSpeed;
    public float rotationSpeed;
    public bool CanShoot;
    public float shootRate;
    private float nextShoot;
    public AudioSource audioSource;

    public JoyStick MoveJoyStick;
    public JoyStick ShootJoyStick;
    public GameObject bullet;
    public GameObject engine1, engine2;
    private ParticleSystem p1, p2;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        p1 = engine1.GetComponent<ParticleSystem>();
        p2 = engine2.GetComponent<ParticleSystem>();
        p1.Stop();
        p2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            p1.Play();
            p2.Play();
        }
        if(Input.GetMouseButtonUp(0))
        {
            p1.Stop();
            p2.Stop();
        }

        CheckPosition();

        if(Input.GetMouseButton(0) && CanShoot)
        {
            if(nextShoot>0)
            {
                nextShoot -= Time.deltaTime;
            }
            if(nextShoot<=0)
            {
                Shoot();
            }
        }
        Movement();
        Rotation();
    }
    void Movement()
    {
         Vector2 InputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
         rb.AddForce(InputDir * moveSpeed);  
         rb.AddForce(MoveJoyStick.InputDir * moveSpeed);
    }

    void Rotation()
    {
        float angle = Mathf.Atan2(ShootJoyStick.InputDir.y, ShootJoyStick.InputDir.x) * Mathf.Rad2Deg+90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        nextShoot = shootRate;
        GameObject bulletClone = Instantiate(bullet, new Vector2(bullet.transform.position.x, bullet.transform.position.y), transform.rotation);
        bulletClone.SetActive(true);
        bulletClone.GetComponent<Bullet>().killTheBullet();
    }

    void CheckPosition()
    {
        float screenWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float screenHight = mainCam.orthographicSize * 2;

        float rightEdge =screenWidth/2;
        float leftEdge = rightEdge*-1;
        float topEdge =screenHight/2;
        float bottomEdge = topEdge*-1;
       

        if (transform.position.x > rightEdge)
        {
            transform.position = new Vector2(leftEdge, transform.position.y);
        }

        if (transform.position.x < leftEdge)
        {
            transform.position = new Vector2(rightEdge, transform.position.y);
        }

        if (transform.position.y > topEdge)
        {
            transform.position = new Vector2(transform.position.x, bottomEdge);
        }

        if (transform.position.y < bottomEdge)
        {
            transform.position = new Vector2(transform.position.x, topEdge);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        gameManager.GameOver();
        ScoreManager.score = 0;
    }
}
