using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RayScript : MonoBehaviour
{
    public Movement player;
    private float timer;
    public float bulletTimer;

    public GameObject bullet;
    public GameObject tempBullet;

    public enum ShootingTypes { Automatic, Manual };

    public ShootingTypes shootingMode;
    public ShootAuto shootAuto;
    public ShootManual shootManual;
    
    public bool ableToSwitch = false;
    public float stateTimer;

    public float bulletRadius = .4f;
    public GameObject pointOut;


    public int weaponDamage;


    public List<GameObject> tempBullets = new List<GameObject>();

    public int tempInt;

    Ray rayBullet;
    public bool hitRay, hitSurface;
    RaycastHit rayInfo;
    public float rayDistance;
    public GameObject hitParticle, blastGO;
    private float particleTimer;

    public LayerMask collisionLayers;

    private void Awake()
    {
        hitSurface = false;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        HandleBullets();
        HandleStates();
    }
   
    void HandleBullets()
    {
        if (tempBullets != null)
        {
            if (tempBullets.Count > 0)
            {
                timer += Time.deltaTime;
            }

            if (timer > bulletTimer)
            {
                if (tempBullets.Count >= 3)
                {
                    for (tempInt = 1; tempInt < 4; tempInt++)
                    {
                        if (tempInt < 2)
                        {
                            Destroy(tempBullets[tempInt]);
                            tempBullets.Remove(tempBullets[tempInt]);
                        }
                        // print(tempInt);
                    }
                }
                timer = 0f;
            }
            // Make way for every object to have detection (possibly by accessing bullet script itself?)


            hitRay = Physics.Raycast(rayBullet, out rayInfo, rayDistance, collisionLayers);
        }

        if (hitRay)
        {
            GameObject tempHitParticle = Instantiate(hitParticle, rayInfo.point, Quaternion.identity);
            GameObject tempEmissiveBlast = Instantiate(blastGO, rayInfo.point, Quaternion.LookRotation(rayInfo.normal));
            particleTimer = 0f;
        }

    }



    void HandleStates()
    {
        

        if(stateTimer > 3)
        {
            ableToSwitch = true;
        }
        else
        {
            ableToSwitch = false;
        }

        while(player.isSwitching > 0 && ableToSwitch)
        {

            if(shootingMode == ShootingTypes.Automatic)
            {
                Debug.Log("Switched mode to MANUAL");
                shootingMode = ShootingTypes.Manual;
            }
            else if(shootingMode == ShootingTypes.Manual)
            {
                Debug.Log("Switched mode to AUTOMATIC");
                shootingMode = ShootingTypes.Automatic;
            }
            stateTimer = 0f;
            break;
        }

        switch (shootingMode)
        {

            case ShootingTypes.Automatic:
                shootAuto.enabled = true;
                shootManual.enabled = false;
                break;

            case ShootingTypes.Manual:
                shootManual.enabled = true;
                shootAuto.enabled = false;
                break;
                         
        }

        // Managing shooting modes 


    }




    private void OnDrawGizmos()
    {
    }
}