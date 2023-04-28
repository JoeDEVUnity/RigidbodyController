using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Intelligence : MonoBehaviour
{

    private float timerBetweenWander;

    public float timeWanderSet, collisionDistance;

    private NavMeshAgent agent;

    private Renderer materialRenderer;
    private Color passiveColor, awareColor, aggressiveColor;

    private bool inRange, isAware;
    [SerializeField] private float checkRadius;
    public int currentWanderPoint;
    private GameObject activePoint;

    public LayerMask playerLayer;
    public GameObject player;

    public GameObject eyes;

    public List<GameObject> wanderPoints = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        materialRenderer = GetComponent<Renderer>();
        passiveColor = materialRenderer.material.color;
        awareColor = new Color(1f, .65f, .11f);
        aggressiveColor = new Color(1.0f, .6f, .5f);

    }

    // Update is called once per frame
    void Update()
    {
        timerBetweenWander += Time.deltaTime;
        Wander();
        Detections();
    
    }
    void Wander()
    {
        if (timerBetweenWander > timeWanderSet)
        {
            currentWanderPoint += 1;

            print("wanderPoint: " + currentWanderPoint);
            timerBetweenWander = 0f;
            if (currentWanderPoint > wanderPoints.Count - 1)
            {
                currentWanderPoint = 0;
            }
            agent.SetDestination(wanderPoints[currentWanderPoint].transform.position);

        }

        
        activePoint = wanderPoints[currentWanderPoint];

        
    }


    void Detections()
    {
        inRange = Physics.CheckSphere(transform.position, checkRadius, playerLayer);
        isAware =  Vector3.Distance(transform.position, player.transform.position) < collisionDistance;


        if (inRange && isAware)
        {
            timerBetweenWander = 0f;
            materialRenderer.material.color = aggressiveColor;
            agent.SetDestination(player.transform.position);
        }
        else if (isAware)
        {
            timerBetweenWander = 0f;
            materialRenderer.material.color = awareColor;
            transform.LookAt(player.transform.position);
        }
        else
        {
            materialRenderer.material.color = passiveColor;
        }
    }



    private void OnDrawGizmos()
    {
        foreach(GameObject wanderPoint in wanderPoints)
        {
            Gizmos.DrawWireSphere(wanderPoint.transform.position, 1f);  
        }

        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
