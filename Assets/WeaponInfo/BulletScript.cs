using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float bulletVelocity;

    Rigidbody bulletRB;

    private GameObject activeEnemy;

    public bool hitRay;
    public float rayDistance = 4f;
    Ray rayBullet;
    RaycastHit rayInfo;

    public GameObject hitParticle, BlastParticle;
    private Intelligence enemyIntelligence;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        
        if(enemy != null)
        {
            enemyIntelligence = enemy.GetComponent<Intelligence>();
        }


    }

    private void Update()
    {
        rayBullet = new Ray(transform.position, transform.forward);

        Physics.Raycast(rayBullet, out rayInfo, 100);
        
        if (Physics.Raycast(rayBullet, out rayInfo, 1))
        {
            StatsManager intelStats = rayInfo.collider.GetComponentInChildren<StatsManager>();
            if (intelStats != null) // Check if the raycast hits an enemy with the intelligence script attached
            {
                intelStats.currentHP -= 1.5f; // Subtract health from the specific enemy instance
                Destroy(this.gameObject);
                Debug.Log("Enemy HP: " + intelStats.currentHP);

                if (intelStats.currentHP <= 0)
                {
                    // Enemy defeated
                    Destroy(rayInfo.collider.gameObject);
                }
            }
            Debug.Log("Hit enemy");
        }



    }

    void FixedUpdate()
    {
        Vector3 bulletForce = transform.forward * bulletVelocity;
        bulletRB.AddForce(bulletForce * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject)
        {
            

            GameObject tempParticle = Instantiate(hitParticle, rayInfo.point, Quaternion.FromToRotation(transform.forward, -transform.forward));


            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "Enemy")
        {
            // On Collision with enemy, instance the enemy hit particle 

            GameObject tempEnemyParticle = Instantiate(BlastParticle, rayInfo.point, Quaternion.identity);


        }
        
    }
}
