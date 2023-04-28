using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float bulletVelocity;

    Rigidbody bulletRB;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 bulletForce = transform.forward * bulletVelocity;
        bulletRB.AddForce(bulletForce * Time.deltaTime, ForceMode.Impulse);
    }

}
