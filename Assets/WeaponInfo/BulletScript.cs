using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float bulletVelocity;

    Rigidbody bulletRB;


    public bool hitRay;
    public float rayDistance = 4f;
    Ray rayBullet;
    RaycastHit rayInfo;

    public GameObject hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {


    }

    void FixedUpdate()
    {
        Vector3 bulletForce = transform.forward * bulletVelocity;
        bulletRB.AddForce(bulletForce * Time.deltaTime, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
