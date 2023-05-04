using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float bulletSpeed;

    Rigidbody rb;

    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        laser = GameObject.FindGameObjectWithTag("laser");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, bulletSpeed);

    }
}
