using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovments : MonoBehaviour
{
    RaycastHit2D findNodeHit;
    public Transform player;

    private Vector3 newLocation;
    [SerializeField]private bool canMove = true;

    [SerializeField] private float clock;
    [SerializeField] private float resetClcok = 1;

    private Vector2[] locatedNodes = new Vector2[4]; // array of new possible node locations
    private Vector3[] directions = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) }; // right left up down

    public float speed = 0.2f;

    public enum ActiveNodes { Normal, Inverted, Teleport }
    public ActiveNodes node = ActiveNodes.Normal;

    // Start is called before the first frame update
    void Start()
    {
        GetNodeLocations();
        clock = resetClcok;
    }


    private void FixedUpdate()
    {
        DrawRays();
        ActivatedNode(node);

    }

 
    private void ActivatedNode(ActiveNodes activeNodes)
    {
        switch (activeNodes)
        {
            case ActiveNodes.Normal: PlayerMove(); break;

            case ActiveNodes.Inverted: PlayerMoveInverted(); break;

            case ActiveNodes.Teleport: PlayerMoveTeleport(); break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inverted")) { ActivatedNode(node = ActiveNodes.Inverted); Debug.Log("Inverted"); }

        if (collision.gameObject.CompareTag("Teleport")) { ActivatedNode(node = ActiveNodes.Teleport); Debug.Log("Teleport"); }

        if (collision.gameObject.CompareTag("Normal")) { ActivatedNode(node = ActiveNodes.Normal); Debug.Log("Normal"); }
    }

    void PlayerMove()
    {
        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; }
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; }

        if (player.position == newLocation)
        {
            clock -= Time.deltaTime;
            if (clock <= 0) 
            {  
                clock = resetClcok;
                canMove = true;
            }
      
            GetNodeLocations();
        }

        transform.position = Vector2.MoveTowards(transform.position, newLocation, speed);

    }

    // has the player teleport to a selected node
    void PlayerMoveTeleport()
    {
        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; }
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; }

        if (player.position == newLocation)
        {
            clock -= Time.deltaTime;
            if (clock <= 0)
            {
                clock = resetClcok;
                canMove = true;
            }
            GetNodeLocations();
        }

        transform.position = newLocation;

    }

    // inverted controls
    void PlayerMoveInverted()
    {
        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; }
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; }

        if (player.position == newLocation)
        {
            clock -= Time.deltaTime;
            if (clock <= 0)
            {
                clock = resetClcok;
                canMove = true;
            }
            GetNodeLocations();
        }

        transform.position = Vector2.MoveTowards(transform.position, newLocation, speed);

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
