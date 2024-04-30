using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class BallAction : BaseHazzards
{
    public BallAction(int damage, float speed) : base(damage, speed)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            if (nextLocation == pointA.transform.position) // To preform the bounce
            {
                nextLocation = pointB.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, nextLocation, speed);
                Debug.Log("Bounce");
            }
            else if (nextLocation == pointB.transform.position)
            {
                nextLocation = pointA.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, nextLocation, speed);
                Debug.Log("Bounce");
            }


            if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealthComponent))
            {
                playerHealthComponent.takeDamage(damage);
            }

        }

    }

}
