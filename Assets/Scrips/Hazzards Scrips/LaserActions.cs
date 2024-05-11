using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovments;

public class LaserActions : BaseHazzards
{
    private Vector3[] dir = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) }; // right left up down
    private RaycastHit2D raycastHit;
    public Transform firePoint;
    public Transform laser;
    private float rayLength = 15;
    public int setDir = 0; // should be between 0 and 3


    [SerializeField]private LineRenderer lineRenderer;

    private void FixedUpdate()
    {
        FireLaser();
        hitPlayer();
    }

    void FireLaser()
    {
        raycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(dir[setDir]), rayLength, LayerMask.GetMask("Player"));
        if (raycastHit.collider != null)
        {
            DrawLine(firePoint.position, raycastHit.point);
        }
        else { DrawLine(firePoint.position, transform.position + transform.TransformDirection(dir[setDir] * rayLength)); }

        Debug.Log(transform.position + transform.TransformDirection(dir[setDir] * rayLength));
    }

    void DrawLine(Vector2 startPos, Vector2 endPos) // to draw a line to represent they ray
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    void hitPlayer()
    {
        if (raycastHit.collider != null)
        {
            if (raycastHit.transform.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealthComponent))
            {
                playerHealthComponent.takeDamage(damage);
            }
        }

        
    }
}
