using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemy;
    public Transform enemyPoint;
    private bool enemyInstanced, canInstance;

    public GameObject player;

    public float timerBeforeInstance;
    public float timerSet;

    public LayerMask playerLayer;

    public float spawnRadius;
    public bool inRangeForInstance;

    private int maxInstances;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inRangeForInstance = Physics.CheckSphere(enemyPoint.position, spawnRadius, playerLayer);

        if (inRangeForInstance && maxInstances < 4)
        {
                timerBeforeInstance += Time.deltaTime;
                if (timerBeforeInstance > timerSet)
                {
                    GameObject tempEnemy = Instantiate(enemy, enemyPoint.transform.position, Quaternion.identity);
                    Intelligence intel = tempEnemy.GetComponent<Intelligence>();
                    intel.player = this.player;
                    maxInstances += 1;
                    timerBeforeInstance = 0;
                }
            }
        }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(enemyPoint.position, spawnRadius);
    }

    }

