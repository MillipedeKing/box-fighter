using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gunn : MonoBehaviour
{
    [SerializeField] private GameObject GrappleGun;
    [SerializeField] private GameObject Grappler;
    [SerializeField] private Transform GrapplerSpawn;
    private GameObject Grapplerinst;
    public GameObject Arrowinst;
    public GameObject Arrow;
    private Vector2 worldPosition;
    private Vector2 direction;
    public Transform Aimer;
    public float maxGrapplersleft = 1;
    public float Grapplersleft = 1;
    private Transform player;
    public float arrowSpeed;
    public PlayerController playerController;
    public float shootTime;

    [Header("Triggers")]
    public bool rightTriggerWasPressed;
    public bool rightTriggerIsHeld;
    public bool rightTriggerReleased;
    //
    public bool leftTriggerWasPressed;
    public bool leftTriggerIsHeld;
    public bool leftTriggerReleased;

    private void Start()
    {
        player = transform.parent.transform;
        Grapplersleft = maxGrapplersleft;
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
  
    }

    private void HandleGunShooting()
    {
        if (leftTriggerWasPressed && Grapplersleft > 0)
        {
            //Spawning Grappler
            shootTime = Time.time;
            Grapplerinst = Instantiate(Grappler, GrapplerSpawn.position, GrappleGun.transform.rotation);
            Grapplerclipper grapplerclipper = Grapplerinst.GetComponent<Grapplerclipper>();
            grapplerclipper.player = player.gameObject;
            grapplerclipper.playerController = playerController;
            Grapplersleft -= 1;
        }
           
        if ( leftTriggerWasPressed)
        {
           Destroy(Grapplerinst);
           playerController.isConnected = false;
           Grapplersleft += 1;
        }

        if (rightTriggerIsHeld)
        {
           arrowSpeed += 0.1f;
           if (arrowSpeed > 20f)
           {
            arrowSpeed = 20;
           }
        }
           
        if (rightTriggerReleased)
        {
            //Spawning Grappler
            Arrowinst = Instantiate(Arrow, GrapplerSpawn.position, GrappleGun.transform.rotation);
            Arrowinst.GetComponent<ArrowBehavior>().arrowSpeedMultiplier = arrowSpeed;
            arrowSpeed = 0;
        }
    }
    public void Look(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        //
        worldPosition = Aimer.position;
        direction = (worldPosition - (Vector2)GrappleGun.transform.position).normalized;
        GrappleGun.transform.right = direction;
        //
        Aimer.position = player.position + (Vector3) input;
    }

    public void SetRightTrigger(bool isPressed)
    {
        if (!isPressed && rightTriggerIsHeld) {
            rightTriggerReleased = true;
            rightTriggerWasPressed = false;
        } else if (isPressed && !rightTriggerIsHeld) {
            rightTriggerWasPressed = true;
            rightTriggerWasPressed = false;
        } else {
            rightTriggerReleased = false;
            rightTriggerWasPressed = false;
        }

        rightTriggerIsHeld = isPressed;
    }
        public void SetLeftTrigger(bool isPressed)
    {
        
    }
}

