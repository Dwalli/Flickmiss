using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    RaycastHit2D findNodeHit;
    public GameObject node;
    private Vector2 newLocation;
    private Vector2 oldLocation;
    private Vector2[] locatedNodes = new Vector2[4];
    private Vector3[] directions = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) }; // right left up down

    // Start is called before the first frame update
    void Start()
    {
        GetNodeLocations();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        createRay();
        DrawRays();
    }

    void createRay()
    {
        findNodeHit = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), transform.TransformDirection(new Vector3(1, 0, 0))); // create the ray

        if (Input.GetKey(KeyCode.D)) newLocation = locatedNodes[0];
        if (Input.GetKey(KeyCode.A)) newLocation = locatedNodes[1];
        if (Input.GetKey(KeyCode.W)) newLocation = locatedNodes[2];
        if (Input.GetKey(KeyCode.S)) newLocation = locatedNodes[3];


        transform.position = Vector2.MoveTowards(transform.position, newLocation, 0.1f);
        Debug.Log("up can move to: " + newLocation);
    }

    // make a loop to find nodes in 4 diffrent directions
    void GetNodeLocations()
    {
        int count = 0;
        foreach (var item in locatedNodes)
        {
           findNodeHit = Physics2D.Raycast(transform.position + directions[count], transform.TransformDirection(directions[count]));
           locatedNodes[count] = findNodeHit.collider.gameObject.transform.position;
           count++;
        }
    }


    // Draw lines to reperent rays
    void DrawRays()
    {

        Debug.DrawRay(transform.position + new Vector3(1, 0, 0), transform.TransformDirection(Vector2.right) * 9, Color.red);  
        Debug.DrawRay(transform.position + new Vector3(-1, 0, 0), -transform.TransformDirection(Vector2.right) * 9, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(Vector2.up) * 9, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, -1, 0), -transform.TransformDirection(Vector2.up) * 9, Color.red);
    }


}
