using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShootAuto : MonoBehaviour
{
    public RayScript playerRay;
    public Movement player;
    public TMP_Text tmp;

    public float fireTimer, fireRate;

    public bool setActive;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "ATTACK MODE";

        playerRay.stateTimer += Time.deltaTime;

        // Create an instance of ray particle and disintegrate  
        playerRay.rayParticle.gameObject.SetActive(player.fireValue > 0 && player.currentHeat > 0);

        
    }
 





    }

