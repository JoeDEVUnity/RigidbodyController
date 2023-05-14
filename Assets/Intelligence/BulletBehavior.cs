using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float bulletSpeed;

    Rigidbody rb;

    private GameObject player;
    private Movement playerScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, bulletSpeed);
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Enemy")
        {

            Destroy(gameObject);

            if(other.gameObject.tag == "Player")
            {

                // Reduce player HP
                playerScript.currentHP -= 5;
                playerScript.regenTimer = 0;
            } 

        }
    }
}
