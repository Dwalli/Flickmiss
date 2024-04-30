using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearActions : BaseHazzards
{
    public SpearActions(int damage, float speed) : base(damage, speed)
    {
    }

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

    private void OnTriggerEnter2D(Collider2D collision) // when this object colied with the player
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealthComponent))
        {
            playerHealthComponent.takeDamage(damage);
        }

    }

}
