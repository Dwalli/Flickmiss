using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearActions : BaseHazzards
{
    public override void ABMovements()
    {
        base.ABMovements();
        if (transform.position == pointA.transform.position)
        {
            transform.eulerAngles = Vector3.zero; // have 180 flip
        }

        if (transform.position == pointB.transform.position)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }


    }

}
