using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLController : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;

    public Transform tt1;
    public Transform tt2;
    public Transform tt3;

    public GameObject t1green;
    public GameObject t1red;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t3green;
    public GameObject t3red;

    public float stateTimer;
    public static int state;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");

        tt1 = transform.Find("TLT1");
        tt2 = transform.Find("TLT2");
        tt3 = transform.Find("TLT3");


        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;

        stateTimer = 10.0f;
        SetState(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (stateTimer >= 0)
        {
            stateTimer -= Time.deltaTime;
        }
        else
        {
            stateTimer = 10.0f;
            if (state > 0)
            {
                SetState(0);
            }
            else
            {
                SetState(1);
            }
        }
    }

    void SetState(int c)
    {
        state = c;
        if (c == 1)
        {
            t1green.SetActive(true);
            t1red.SetActive(false);
            t2green.SetActive(false);
            t2red.SetActive(true);
            t3green.SetActive(false);
            t3red.SetActive(true);

            //1&2
            tt1.position = new Vector3(-2f, 0.5f, -9f);
            tt2.position = new Vector3(2f, 0.5f, 9f);
            tt3.position = new Vector3(9f, -10f, -2f);
        }
        else
        {
            t1green.SetActive(false);
            t1red.SetActive(true);
            t2green.SetActive(true);
            t2red.SetActive(false);
            t3green.SetActive(true);
            t3red.SetActive(false);

            //3
            tt1.position = new Vector3(-2f, -10f, -9f);
            tt2.position = new Vector3(2f, -10f, 9f);
            tt3.position = new Vector3(9f, 0.5f, -2f);
        }
    }
}
