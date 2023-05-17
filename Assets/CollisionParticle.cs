using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour
{
    public GameObject hitParticle, blastParticle;

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")
        {

            // Grab the hit enemy's intelligence script && apply damage
            StatsManager intelStats = other.GetComponentInChildren<StatsManager>();
            if (intelStats != null) // Check if the raycast hits an enemy with the intelligence script attached
            {
                intelStats.currentHP -= 1.5f; // Subtract health from the specific enemy instance
                Debug.Log("Enemy HP: " + intelStats.currentHP);
                GameObject tempParticle = Instantiate(hitParticle, other.transform.position, Quaternion.FromToRotation(transform.forward, -transform.forward));

                if (intelStats.currentHP <= 4)
                {
                    GameObject tempEnemyParticle = Instantiate(blastParticle, other.transform.position, Quaternion.identity);
                }
                
            }
            Debug.Log("Hit enemy");


        }
    }
}
