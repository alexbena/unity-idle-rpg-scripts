using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject player;
    int current = -1;
    public float speed;
    float WP_radius = 0.4f;

    void Update()
    {
        if(current != -1) { 
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, step);

            if (Vector3.Distance(transform.position, waypoints[current].transform.position) < WP_radius)
            {
                current++;
                if (current >= waypoints.Length) {
                    current = -1;
                }
            }
        }

    }

    public void Move() {
        this.current = 0;
    }
}
