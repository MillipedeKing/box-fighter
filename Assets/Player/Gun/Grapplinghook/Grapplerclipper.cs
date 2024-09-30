using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grapplerclippers : MonoBehaviour
{
    public Collider2D mycollider;
    public Rigidbody2D rb;

    public Boolean isactive = true;
    [SerializeField] private LayerMask groundLayer;
    public GameObject player;
    public DistanceJoint2D springJoint;
    public LineRenderer lineRenderer;
    public bool disable;
    
    public PlayerController playerController;


    void Start()
    {
        // ToDo: replace with player instance for multiplayer.
        player = GameObject.Find("Player");
        mycollider = gameObject.GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(mycollider, player.GetComponent<Collider2D>());
        springJoint = gameObject.AddComponent<DistanceJoint2D>();
        springJoint.connectedBody = player.GetComponent<Rigidbody2D>();
        springJoint.enabled = false;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth  = 0.05f;
        lineRenderer.endWidth = 0.02f;
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        lineRenderer.SetPositions(new Vector3[] {transform.position, player.transform.position});
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        rb.Sleep();
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerController.isConnected = true;
        transform.parent = otherGameObject.transform;
        springJoint.enabled = true;
        springJoint.anchor = transform.position;
        springJoint.distance = 1;
    }
}
