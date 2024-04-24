using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static PlayerMovments;

public class BallAction : BaseHazzards
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            if (nextLocation == pointB.transform.position)
            {
                //nextLocation = pointA.transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, nextLocation, speed);
                Debug.Log("Bounce");
            }

            if (nextLocation == pointA.transform.position)
            {
                //nextLocation = pointB.transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, nextLocation, speed);
                Debug.Log("Bounce");
            }
        }

    }
}
