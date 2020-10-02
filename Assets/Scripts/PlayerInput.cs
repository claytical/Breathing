using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Transform[] hurdleTarget;
    public Transform[] bounceSpot;
    public Transform StartingPoint;
    public Transform Goal;
    public float breathCaptured;
    public float timeBetweenBreaths = 5;
    public float speed = 1f;

    private int currentIndex = 0;
    private bool exhaling = false;

    private int score = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (breathCaptured <= timeBetweenBreaths)
            {
                Inhale();
            }
            else if(!exhaling)
            {
                //breath limit reached
                exhaling = true;
            }
        }

        if (!Input.GetMouseButton(0) && exhaling)
        {
            Exhale();
        }
    }

    public void Inhale()
    {
        breathCaptured += Time.deltaTime;
        float distCovered = ExtensionMethods.Remap(breathCaptured, 0, timeBetweenBreaths, 0, 1);
        if(currentIndex == 0)
        {
            //STARTING
            transform.position = Vector3.Lerp(StartingPoint.position, hurdleTarget[currentIndex].position, distCovered);
        }
        else if(currentIndex < bounceSpot.Length)
        {
            transform.position = Vector3.Lerp(bounceSpot[currentIndex - 1].position, hurdleTarget[currentIndex].position, distCovered);

        }
        else
        {
            Debug.Log("CURRENT INDEX IS LONGER THAN BOUNCE LENGTH! SCORE A GOAL!");
        }

    }

    public void Exhale()
    {

        float distCovered = ExtensionMethods.Remap(breathCaptured, 0, timeBetweenBreaths, 1, 0);
        if(currentIndex - 1 == bounceSpot.Length)
        {
            transform.position = Vector3.Lerp(bounceSpot[currentIndex].position, Goal.position, distCovered);

        }
        else
        {
            transform.position = Vector3.Lerp(hurdleTarget[currentIndex].position, bounceSpot[currentIndex].position, distCovered);

        }
        breathCaptured -= Time.deltaTime;

        if(breathCaptured <= 0)
        {
            Debug.Log("Breath Ran Out");
            exhaling = false;
            breathCaptured = 0;
            currentIndex++;
            score++;

            if (currentIndex > hurdleTarget.Length)
            {
                Debug.Log("Hit target limit, resetting");
                currentIndex = 0;
            }
        }
    }

    public float getBreathsCaptured()
    {
        return breathCaptured;
    }

}

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}