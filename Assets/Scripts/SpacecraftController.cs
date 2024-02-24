using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SpacecraftController : MonoBehaviour
{
    public TMP_Text ControlModeUI;
    public TMP_Text VelocityIndicator;

    public GameObject Afterburner1;
    public GameObject Afterburner2;

    public float thrust = 0.3f;
    public float torque = 0.1f;

    public int ControllMode = 0;


    public float LX;
    public float LY;
    public float RX;
    public float RY;

    float LB;
    float RB;
    public float BUTTONS;

    float LT;
    float RT;
    public float TRIGGERS;

    public float yCoordinateToFly;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Afterburner1.SetActive(false);
        Afterburner2.SetActive(false);
        if (ControllMode == 0)
        {
            ControlModeUI.text = "VTOL FULL control";
        }
        else if (ControllMode == 1) 
        {
            ControlModeUI.text = "VTOL ARCADE control";
        }
        else if (ControllMode == 2)
        {
            ControlModeUI.text = "JET control";
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();
        VelocityIndicator.text = Math.Round(rb.velocity.magnitude).ToString() + " m/s";
    }

    void HandleControls()
    {
        LX = (float)(Math.Round(Input.GetAxis("LX"), 1));
        LY = (float)(Math.Round(-Input.GetAxis("LY"), 1));
        RX = (float)(Math.Round(Input.GetAxis("RX"), 1));
        RY = (float)(Math.Round(-Input.GetAxis("RY"), 1));

        

        if (Input.GetButton("LB"))
        {
            LB = -1f;
        }
        else
        {
            LB = 0f;
        }
        if (Input.GetButton("RB"))
        {
            RB = 1f;
        }
        else
        {
            RB = 0f;
        }
        BUTTONS = (float)(Math.Round(LB + RB, 1));

        LT = -Input.GetAxis("LT");
        RT = Input.GetAxis("RT");

        TRIGGERS = (float)(Math.Round(LT + RT, 1));


        if (Input.GetButtonDown("START"))
        {
            ControllMode++;
            if (ControllMode > 2)
            {
                ControllMode = 0;
            }
        }
        if (ControllMode == 0)
        {
            rb.AddForce(transform.right * thrust * LY);
            rb.AddForce(-transform.forward * thrust * LX);
            rb.AddForce(transform.up * thrust * TRIGGERS);

            rb.AddTorque(-transform.forward * torque * RY);
            rb.AddTorque(-transform.right * torque * RX);
            rb.AddTorque(transform.up * torque * BUTTONS);
        }
        else if (ControllMode == 1)
        {
            rb.AddForce(transform.right * thrust * LY);
            rb.AddForce(-transform.forward * thrust * LX);

            rb.AddForce(transform.up * thrust * TRIGGERS);
            rb.AddTorque(transform.up * torque * RX);

        }
        else if (ControllMode == 2)
        {
            rb.AddForce(transform.right * thrust * LY);
            rb.AddTorque(-transform.forward * torque * RY);
            rb.AddTorque(-transform.right * torque * RX);
            rb.AddTorque(transform.up * torque * BUTTONS);

            if(LY > 0.8)
            {
                Afterburner1.SetActive(true); 
                Afterburner2.SetActive(true);
            }
            else
            {
                Afterburner1.SetActive(false); 
                Afterburner2.SetActive(false);
            }

        }

        if (ControllMode == 0)
        {
            ControlModeUI.text = "VTOL FULL control";
        }
        else if (ControllMode == 1)
        {
            ControlModeUI.text = "VTOL ARCADE control";
        }
        else if (ControllMode == 2)
        {
            ControlModeUI.text = "JET control";
        }

    }
}
