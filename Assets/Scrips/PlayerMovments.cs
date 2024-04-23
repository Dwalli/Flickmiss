using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    RaycastHit2D findNodeHit;
    public GameObject node;
    public Transform player;

    private Vector3 newLocation;
    private Vector2 CurrentLocation;
    [SerializeField]private bool canMove = true;

    private Vector2[] locatedNodes = new Vector2[4]; // array of new possible node locations
    private Vector3[] directions = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) }; // right left up down

    public float speed = 0.2f;

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
        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; } 
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; }

        transform.position = Vector2.MoveTowards(transform.position, newLocation, speed);

        Debug.Log("can move to: " + newLocation);


        if (player.position == newLocation)
        {
            canMove = true;
            GetNodeLocations();
        }
    }

    // make a loop to find nodes in 4 diffrent directions
    void GetNodeLocations()
    {
        int count = 0;
        foreach (var item in locatedNodes)
        {
           findNodeHit = Physics2D.Raycast(transform.position + directions[count], transform.TransformDirection(directions[count]));  // create the ray
            if (findNodeHit.collider != null)
            {
                locatedNodes[count] = findNodeHit.collider.gameObject.transform.position;
            }
            else
            {
                locatedNodes[count] = player.position; // to provent the player moving to past location
            }

            count++;
        }

        Debug.Log("found node" + locatedNodes[0] + locatedNodes[1] + locatedNodes[2] + locatedNodes[3]);
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
