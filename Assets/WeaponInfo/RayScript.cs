using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RayScript : MonoBehaviour
{
    public Movement player;
    private Animator anim;
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
    public GameObject rayParticle, pickupParticle;
    public Renderer rayMat;
    
    private float particleTimer;



    public LayerMask collisionLayers;

    private void Awake()
    {
       // hitSurface = false;
    }
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        HandleStates();
    }
   



    void HandleAnimation()
    {
        anim.SetBool("canRotate", player.fireValue > 0 && player.currentHeat > 0);
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
