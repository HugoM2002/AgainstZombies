using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Renderer renderColor;

    private bool aware = false;

    public float fov = 120f;
    public float viewDistance = 15f;
    public float wanderRadius = 5f;

    public Vector3 wanderPoint;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderColor = GetComponent<Renderer>();
        wanderPoint = RandomWanderPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (aware)
        {
            agent.SetDestination(player.transform.position);
            renderColor.material.color = Color.red;
        }
        else
        {
            SearchForPlayer();
            Wander();
            renderColor.material.color = Color.black;
        }
    }

    public void SearchForPlayer()
    {
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2f)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if(Physics.Linecast(transform.position, player.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }               
            }
        }
    }

    public void OnAware()
    {
        aware = true;
    }

    public void Wander()
    {
        if(Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoints();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    public Vector3 RandomWanderPoints()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, transform.position.z);
    }
}
