using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gunn : MonoBehaviour
{
    [SerializeField] private GameObject GrappleGun;
    [SerializeField] private GameObject Grappler;
    [SerializeField] private Transform GrapplerSpawn;
    private GameObject grapplerInst;
    public GameObject Arrowinst;
    public GameObject Arrow;
    private Vector2 worldPosition;
    private Vector2 direction;
    public Transform Aimer;
    public float maxGrapplersleft = 1;
    public float grapplersLeft = 1;
    private Transform player;
    public float arrowSpeed;
    public PlayerController playerController;
    public float shootTime;
    [Header("Inputs")]
    public Vector2 movement;
    public Vector2 look;

    [Header("Triggers")]
    public bool isGrapplerButtonHeld;

    private void Start()
    {
        player = transform.parent.transform;
        grapplersLeft = maxGrapplersleft;
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        Aim();
        HandleGunShooting();
    }

    private void Aim() {
        Vector2 input = look != Vector2.zero ? look : movement;
        worldPosition = Aimer.position;
        direction = (worldPosition - (Vector2)GrappleGun.transform.position).normalized;
        GrappleGun.transform.right = direction;
        Aimer.position = player.position + (Vector3) input;
    }

    private void HandleGunShooting()
    {
           
        // If isGrapplerButtonHeld and shootTime is greater than x recall the grappler?

        // if (crossBowButtonHeld)
        // {
        //    arrowSpeed += 0.1f;
        //    if (arrowSpeed > 20f)
        //    {
        //     arrowSpeed = 20;
        //    }
        // }
           
        // if (crossBowButtonReleased)
        // {
        //     //Spawning Arrow
        //     Arrowinst = Instantiate(Arrow, GrapplerSpawn.position, GrappleGun.transform.rotation);
        //     Arrowinst.GetComponent<ArrowBehavior>().arrowSpeedMultiplier = arrowSpeed;
        //     arrowSpeed = 0;
        // }
    }
    public void Look(InputValue value, Vector2 movementVAlues)
    {
        
    }

    public void ShootGrappler() {
        isGrapplerButtonHeld = true;
        if (grapplersLeft > 0) {
            shootTime = Time.time;
            grapplerInst = Instantiate(Grappler, GrapplerSpawn.position, GrappleGun.transform.rotation);
            Grapplerclipper grapplerclipper = grapplerInst.GetComponent<Grapplerclipper>();
            grapplerclipper.player = player.gameObject;
            grapplerclipper.playerController = playerController;
            grapplersLeft -= 1;
        }
    }

    public void RecallGrappler() {
        isGrapplerButtonHeld =  false;
        if (!grapplerInst.IsUnityNull()) {
            Destroy(grapplerInst);
            playerController.isConnected = false;
            grapplersLeft += 1;
        }
    }

    // Add method for crossBowButton
}

