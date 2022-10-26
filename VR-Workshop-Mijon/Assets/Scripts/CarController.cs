using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;

    public bool go = false;
    public float initialDelay;

    public bool collision = false;

    Rigidbody rb;
    int targetWP = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("CWP1");  //0
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP2");  //1
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP3");  //2
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP4");  //3
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP5");  //4
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP6");  //5
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP-J1");  //6
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP-J2");  //7
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP-J3");  //8
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP-J4");  //9
        wps.Add(wp.transform);

        initialDelay = Random.Range(2.0f, 10.0f);
        transform.position = new Vector3(0.0f, -10.0f, 0.0f);

        SetRoute();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!collision)
        {
            if (!go)
            {
                initialDelay -= Time.deltaTime;
                if (initialDelay <= 0.0f)
                {
                    go = true;
                    SetRoute();
                }
                else return;
            }

            Vector3 displacement = route[targetWP].position - transform.position;
            displacement.y = 0;
            float dist = displacement.magnitude;

            if (dist < 0.1f)
            {
                targetWP++;
                if (targetWP >= route.Count)
                {
                    SetRoute();
                    return;
                }
            }

            Vector3 velocity = displacement;
            velocity.Normalize();
            velocity *= 5f;

            Vector3 newPosition = transform.position;
            newPosition += velocity * Time.deltaTime;
            rb.MovePosition(newPosition);

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            rb.MoveRotation(rotation);
        }
    }

    void SetRoute()
    {
        //randomise the next route;
        routeNumber = Random.Range(0, 6);

        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1] };
        else if (routeNumber == 1) route = new List<Transform> { wps[3], wps[2] };
        else if (routeNumber == 2) route = new List<Transform> { wps[0], wps[6], wps[5] };
        else if (routeNumber == 3) route = new List<Transform> { wps[3], wps[7], wps[5] };
        else if (routeNumber == 4) route = new List<Transform> { wps[4], wps[9], wps[2] };
        else if (routeNumber == 5) route = new List<Transform> { wps[4], wps[8], wps[1] };

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.54f, route[0].position.z);
        targetWP = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (go)
        {
            if (other.tag == "Pedestrian" || other.tag == "Vehicle" || other.tag == "TrafficLight")
            {
                collision = true;
            }
        }
        else SetRoute();
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }
}
