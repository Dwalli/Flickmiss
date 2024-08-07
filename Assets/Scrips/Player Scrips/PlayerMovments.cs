using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovments : PlayerSystems
{
    [SerializeField] private PlayerHealth playerHealth;

    RaycastHit2D findNodeHit;
    RaycastHit2D findWallHit;
    public Transform player;

    private Vector3 newLocation;
    [SerializeField] private bool canMove = true;

    //Movement systems
    [SerializeField] private float playerSpeed = 1;

    [SerializeField] private float clock;
    [SerializeField] private float resetClcok = 1;

    private float castTime = 0.1f;
    private float cast;

    [SerializeField] private int range = 20;

    private Vector2[] locatedNodes = new Vector2[4]; // array of new possible node locations
    private Vector3[] directions = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) }; // right left up down

    public enum ActiveNodes { Normal, Inverted, Teleport, Blank } // Enum of posible nodes
    private ActiveNodes node = ActiveNodes.Normal; 

    // Start is called before the first frame update
    void Start()
    {
        GetNodeLocations();
        newLocation = playerHealth.SpawnPoint();
        clock = resetClcok;
    }


    private void FixedUpdate()
    {
        DrawRays();
        if (playerHealth?.Health() <= 0)
        {
            ResetPlayer();
        }
        else { ActivatedNode(node); }

        Debug.Log(node);

    }

    private void ResetPlayer() //when player health reached 0 reset the game
    {
        newLocation = playerHealth.SpawnPoint();
        transform.position = newLocation;
        playerHealth.ResetHealth();
       // canMove = false;
    }

    private void ActivatedNode(ActiveNodes activeNodes)
    {
        switch (activeNodes)
        {
            case ActiveNodes.Normal: PlayerMove(); break;

            case ActiveNodes.Inverted: PlayerMoveInverted(); break;

            case ActiveNodes.Teleport: PlayerMoveTeleport(); break;

            default: PlayerMove(); break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inverted")) { node = ActiveNodes.Inverted; Debug.Log("Inverted"); }

        if (collision.gameObject.CompareTag("Teleport")) { node = ActiveNodes.Teleport; Debug.Log("Teleport"); }

        if (collision.gameObject.CompareTag("Normal")) { node = ActiveNodes.Normal; Debug.Log("Normal"); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        node = ActiveNodes.Blank; Debug.Log("Blank");
    }

    void NewNodeCoundoun()
    {
        if (player.position == newLocation)
        {
            clock -= Time.deltaTime;
            if (clock <= 0)
            {
                canMove = true;
            }

            GetNodeLocations();
        }
        else
        {
            clock = resetClcok;
        }
    }

    void PlayerMove()
    {
        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; }
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; }

        NewNodeCoundoun();

        transform.position = Vector2.MoveTowards(transform.position, newLocation, playerSpeed);

    }


    // has the player teleport to a selected node
    void PlayerMoveTeleport()
    {

        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; cast = castTime; }
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; cast = castTime; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; cast = castTime; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; cast = castTime; }

        NewNodeCoundoun();


        cast -= Time.deltaTime;
        if (cast < 0) transform.position = newLocation;

    }

    // inverted controls
    void PlayerMoveInverted()
    {
        // getting the user inputs
        if (Input.GetKey(KeyCode.D) && canMove == true) { newLocation = locatedNodes[1]; canMove = false; }
        if (Input.GetKey(KeyCode.A) && canMove == true) { newLocation = locatedNodes[0]; canMove = false; }
        if (Input.GetKey(KeyCode.W) && canMove == true) { newLocation = locatedNodes[3]; canMove = false; }
        if (Input.GetKey(KeyCode.S) && canMove == true) { newLocation = locatedNodes[2]; canMove = false; }

        NewNodeCoundoun();

        transform.position = Vector2.MoveTowards(transform.position, newLocation, playerSpeed);

    }

    // make a loop to find nodes in 4 diffrent directions
    void GetNodeLocations()
    {
        int count = 0;
        foreach (var item in locatedNodes)
        {
            findWallHit = Physics2D.Raycast(transform.position + directions[count], transform.TransformDirection(directions[count]), range, LayerMask.GetMask("Wall"));
            if (findNodeHit.collider != null && findNodeHit.collider.gameObject.layer == 8)
            {
                locatedNodes[count] = player.position;
            }
            else
            {
                findNodeHit = Physics2D.Raycast(transform.position + directions[count], transform.TransformDirection(directions[count]), range, LayerMask.GetMask("Node's"));  // create the rays only on layer where nodes are at
                if (findNodeHit.collider != null && findNodeHit.collider.gameObject.layer == 6)
                {
                    locatedNodes[count] = findNodeHit.collider.gameObject.transform.position;
                }
                else
                {
                    locatedNodes[count] = player.position; // to provent the player moving to past location
                }
            }

            count++;
        }

     //   Debug.Log("found node" + locatedNodes[0] + locatedNodes[1] + locatedNodes[2] + locatedNodes[3]); // report all of the node found
    }


    // Draw lines to reperent rays
    void DrawRays()
    {

        Debug.DrawRay(transform.position + new Vector3(1, 0, 0), transform.TransformDirection(Vector2.right) * range, Color.red);  
        Debug.DrawRay(transform.position + new Vector3(-1, 0, 0), -transform.TransformDirection(Vector2.right) * range, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(Vector2.up) * range, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, -1, 0), -transform.TransformDirection(Vector2.up) * range, Color.red);
    }


}
