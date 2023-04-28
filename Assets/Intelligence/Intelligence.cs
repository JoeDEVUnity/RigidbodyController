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

    public GameObject main, objectOfRotation;
    public float speedOfRotation = 10f;

    private int startPoint;

    public List<GameObject> wanderPoints = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        materialRenderer = main.GetComponent<Renderer>();
        passiveColor = materialRenderer.material.color;
        awareColor = new Color(1f, .65f, .11f);
        aggressiveColor = new Color(1.0f, .6f, .5f);

        startPoint = currentWanderPoint;
        

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
        activePoint = wanderPoints[currentWanderPoint];

        currentWanderPoint = Random.Range(0, wanderPoints.Count);
        
        if (timerBetweenWander > timeWanderSet)
        {

            print("wanderPoint: " + activePoint);
            timerBetweenWander = 0f;

            Quaternion lookAt = Quaternion.LookRotation(activePoint.transform.position - transform.position);

            Quaternion rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * speedOfRotation);

            transform.rotation = rotation;


            agent.SetDestination(activePoint.transform.position);
            //transform.LookAt(activePoint.transform.position, Vector3.forward);

        }
        
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
            LookAt();
        }
        else if (isAware)
        {
            timerBetweenWander = 0f;
            materialRenderer.material.color = awareColor;
            LookAt();
        }
        else
        {
            materialRenderer.material.color = passiveColor;
        }
    }
    void LookAt()
    {
        Quaternion lookAt = Quaternion.LookRotation(player.transform.position - objectOfRotation.transform.position);

        Quaternion rotation = Quaternion.Slerp(objectOfRotation.transform.rotation, lookAt, Time.deltaTime * speedOfRotation);

        objectOfRotation.transform.rotation = rotation;
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
