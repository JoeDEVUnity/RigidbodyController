using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    //[SerializeField] private Slider intensitySlider;

    [SerializeField] private Intelligence intel;
    [SerializeField] private GameObject player;

    public float currentHP;
    public float hpMax, regenRate, intensityTimer;
    private float currentIntensity, coolOffTimer;
    private float timeBeforeRegen;
  


    [SerializeField] private LaserScript laserScript;


    private void Start()
    {
        SetParameters();
    }

    // Function made to set parameters of gathered components
    void SetParameters()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hpSlider = GetComponentInChildren<Slider>();

        //hpSlider.transform.localScale = new Vector3(hpSlider.transform.localScale.x / 20, hpSlider.transform.localScale.y / 20, hpSlider.transform.localScale.z / 10);


        //this.intensitySlider.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        //intensitySlider.transform.localScale = new Vector3(intensitySlider.transform.localScale.x / 45, intensitySlider.transform.localScale.y / 30, intensitySlider.transform.localScale.z / 10);
        //intensitySlider.maxValue = intensityTimer;

        hpSlider.maxValue = hpMax;
        currentHP = hpMax;


    }

    private void Update()
    {
        SetUpdateParameters();
    }

    void SetUpdateParameters()
    {

        hpSlider.transform.LookAt(player.transform, Vector3.up);
        //intensitySlider.transform.LookAt(player.transform, Vector3.up);
       

        hpSlider.value = currentHP;

        if (currentHP < hpMax)
        {
            timeBeforeRegen += Time.deltaTime;

            if (timeBeforeRegen > 3)
            {
                // Start regen slowly
                currentHP += regenRate * Time.deltaTime;
            }
        }
        else
        {
            timeBeforeRegen = 0f;
        }

        hpSlider.gameObject.SetActive(currentHP < hpMax);


    }

    


   

   

}
