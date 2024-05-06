using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHazzard : BaseHazzards
{

    [SerializeField] private float RayDistance = 100f;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;

    RaycastHit2D anyHit;

    public enum SelectDirection { Right, Left, Up, Down } // i'll will forget the numbers

    private static readonly Vector3[] value = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) };
    private Vector3[] directions = value;

    private Vector3[] up = { new Vector3(0, 1, 0), new Vector3(0, -1, 0) };


    private void FixedUpdate()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        anyHit = Physics2D.Raycast(transform.position, transform.position + up[1]);
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
