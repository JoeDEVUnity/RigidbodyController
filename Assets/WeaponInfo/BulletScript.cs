using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float bulletVelocity;

    Rigidbody bulletRB;

    public LayerMask activeEnemy;

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
        hitRay = Physics.Raycast(rayBullet, out rayInfo, 100, activeEnemy);
        if (hitRay)
        {
            
        }



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
