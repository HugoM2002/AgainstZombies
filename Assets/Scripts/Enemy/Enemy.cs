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


    // Start is called before the first frame update
    void Start()
    {
        agent.GetComponent<NavMeshAgent>();
        renderColor = GetComponent<Renderer>();
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
            renderColor.material.color = Color.black;
        }
    }

    public void SearchForPlayer()
    {
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2f)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                OnAware();
            }
        }
    }

    public void OnAware()
    {
        aware = true;
    }
}
