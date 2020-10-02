using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{

    public Transform ball;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();        
    }

    void MoveBall()
    {
        float speed = 3f;
        //        transform.position = ball.position + offset;
        transform.position = Vector3.MoveTowards(transform.position, ball.position + offset, Time.deltaTime * speed);
    }
}
