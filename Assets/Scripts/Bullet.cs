using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Start()
    {
       
        GetComponent<Rigidbody2D>().AddForce(transform.up * 400f*(-1));
    }

    // Update is called once per frame
    public void killTheBullet()
    {
        Destroy(gameObject, 2f);
       
    }
}
