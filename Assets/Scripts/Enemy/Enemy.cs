using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    private bool aware = false;

    public float fov = 120f;
    public float viewDistance = 15f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aware)
        {

        }
        else
        {
            SearchForPlayer();
        }
    }

    public void SearchForPlayer()
    {
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < 100f)
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
