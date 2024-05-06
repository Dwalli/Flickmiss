using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHazzards : MonoBehaviour
{
    [SerializeField]protected int damage;
    [SerializeField]protected float speed;

    [SerializeField]protected GameObject pointA, pointB;

    protected Vector3 nextLocation;


    //public BaseHazzards(int damage, float speed) //contructor
    //{
    //    this.damage = damage;
    //    this.speed = speed;
    //} 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.transform.position;
    }

    private void FixedUpdate()
    {
        ABMovements();
    }


    public virtual void ABMovements()
    {
        if (transform.position == pointA.transform.position)
        {
            nextLocation = pointB.transform.position;
        }

        if (transform.position == pointB.transform.position)
        {
            nextLocation = pointA.transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, nextLocation, speed);

    }
}
