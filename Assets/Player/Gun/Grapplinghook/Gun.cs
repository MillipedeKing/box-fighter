using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
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
    public float Grapplersleft;
    private Transform player;
    public float arrowSpeed;
    public PlayerController playerController;
    public float shootTime;
    public Grapplerclipper Grapplerclipper;
    private void Start()
    {
        player = transform.parent.transform;
        Grapplersleft = maxGrapplersleft;
        playerController = player.GetComponent<PlayerController>();
       
    }
    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
        SetAimerPosition();
    }

    private void HandleGunShooting()
    {
        if (Userinput.instance.controls.Gameplay.Trigger.WasPressedThisFrame() && Grapplersleft > 0)
        {
            //Spawning Grappler
            shootTime = Time.time;
            Grapplerinst = Instantiate(Grappler, GrapplerSpawn.position, GrappleGun.transform.rotation);
            Grapplerclipper grapplerclipper = Grapplerinst.GetComponent<Grapplerclipper>();
            grapplerclipper.playerController = playerController;
            Grapplersleft -= 1;
        }
           
        if (Userinput.instance.controls.Gameplay.Trigger.WasReleasedThisFrame()) //&& Grapplersleft > 0)
        {
           Destroy(Grapplerinst);
           playerController.isConnected = false;
           Grapplersleft += 1;
        }

        if (Userinput.instance.controls.Gameplay.RightTrigger.IsPressed())
        {
           arrowSpeed += 0.1f;
           if (arrowSpeed > 15f)
           {
            arrowSpeed = 15;
           }
        }
           
        if (Userinput.instance.controls.Gameplay.RightTrigger.WasReleasedThisFrame())
        {
            //Spawning Grappler
            Arrowinst = Instantiate(Arrow, GrapplerSpawn.position, GrappleGun.transform.rotation);
            Arrowinst.GetComponent<ArrowBehavior>().arrowSpeedMultiplier = arrowSpeed;
            arrowSpeed = 0;
        }
    }
    private void HandleGunRotation()
    {
    
        worldPosition = Aimer.position;// Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)GrappleGun.transform.position).normalized;
        GrappleGun.transform.right = direction;

        // angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Vector3 localScale = new Vector3(1f, 1f, 1f);
        // if ( angle > 90 || angle < -90)
        // {
        //     localScale.y = -1f;
        // }
        // else
        // {
        //     localScale.y = 1f;
        // }
        // GrappleGun.transform.localScale = localScale;
    }
    private void SetAimerPosition()
    {
        
        Vector2 input = Userinput.instance.controls.Gameplay.Look.ReadValue<Vector2>();
        Aimer.position = player.position + (Vector3) input;
    }
}

