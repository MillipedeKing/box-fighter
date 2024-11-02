using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Player move")]
    public Vector2 movementValues = Vector2.zero;
    public float horizontal;
    public float vertical;
    public float speed = 8f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    Collider2D myCollider;
    public Vector2 moveInput;

    [Header("Player jump")]
    //Jump
    [SerializeField] public float jumpingPower = 18f;

    [SerializeField] public float JumpTime = 0.5f;

    public bool isJumping;
    public bool isFalling;
    public bool isJumpPressed;
    public bool wasJumpJustPressed;
    public float JumpTimeCounter;
    [SerializeField] private float ExtraHieght = 0.25f;

    private RaycastHit2D groundhit;

    [Header("Dashing")]
    private bool dashButtonPressed = false;
    private bool candash = true;
    private bool isdashing;
    public float dashingpower = 500f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    // Start is called before the first frame update
    [Header("Grappling hook")]
    public bool isConnected = false;

    [Header("Health")]
    public float HP;
    public float maxHp;
    public bool isDead;
    [Header("gun")]
    public Gunn gun;

    void Start()
    {
        HP = 100;
        maxHp = HP;
        myCollider = gameObject.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        gun = gameObject.GetComponentInChildren<Gunn>();
    }

    // Update is called once per frame
    void Update()
    {

        if (HP < 1)
        {
            isDead = true;
            if (HP < 0)
            {
                HP = 0;
            }
        }

        if (!isDead)
        {
            if (isdashing)
            {
                return;
            }
            Move();
            Jump();
            if (dashButtonPressed && candash)
            {
                StartCoroutine(HandleDash());
            }
        }

        if (isDead)
        {
            // GHOST MODE!!!
            ghostMode();
        }
    }

    public void OnDashbutton(InputValue value){
        if (value != null && value.Get<float>() > 0.5f)
        {
            dashButtonPressed = value.isPressed;
        }
    }

    public void OnMove(InputValue value) {
        movementValues = value.Get<Vector2>();
        horizontal = movementValues.x;
        vertical = movementValues.y;
        if (gun != null)
        {
            gun.movement = movementValues;
        }
    }

    private void Move()
    {
        if (!isConnected)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            rb.AddForce(new Vector2(horizontal * (speed/2), 0));
        }

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            // localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void OnLook(InputValue value)
    {
        if (gun) {
            gun.look = value.Get<Vector2>();
        }
    }
    
    public void OnJump(InputValue value) {
        isJumpPressed = value.isPressed;
        wasJumpJustPressed = isJumpPressed;
    }

    public void Jump()
    {
        if (isGrounded() && wasJumpJustPressed)
        {
            //button was just pushed
            isJumping = true;
            JumpTimeCounter = JumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        
        if (isJumpPressed)
        {
            
            if (JumpTimeCounter > 0 && isJumping)
            {
                //button is being held
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            
            wasJumpJustPressed = false;
        }

        if (!isJumpPressed)
        {
            //button released
            isJumping = false;
        }
    }

    private bool isGrounded()
    {
        if (isConnected) {
            return true;
        }
        
        groundhit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size, 0f, Vector2.down, ExtraHieght, groundLayer);

        if (groundhit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private IEnumerator HandleDash()
    {

        if (isFacingRight)
        {
            candash = false;
            isdashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashingpower, 0f);
            yield return new WaitForSeconds(dashingTime);
            rb.gravityScale = originalGravity;
            isdashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            candash = true;
        }
        else
        {
            candash = false;
            isdashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * -dashingpower, 0f);
            yield return new WaitForSeconds(dashingTime);
            rb.gravityScale = originalGravity;
            isdashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            candash = true;
        }
    }
    public void Damage(float damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            rb.gravityScale = 0;
            myCollider.enabled = false;
            isDead = true;

            
        }
    }
    private void ghostMode()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);

    }

    public void OnLeftTrigger(InputValue value)
    {
        // Debug.Log(value.Get<float>());
        if (!isDead){
        if (value != null && value.Get<float>() > 0.5f) {
            gun.ShootGrappler();
        } else {
            gun.RecallGrappler();
        }
        }
    }
    public void OnRightTrigger(InputValue value)
    {
        // Shoot Arrow
        //   Debug.Log(value.Get<float>());
        if (!isDead){
        if (value != null && value.Get<float>() > 0.5f) {
            gun.crossBowButtonHeld = true;
        } else {
            gun.crossBowButtonHeld = false;
            gun.HandleCrossBowRelease();
        }
        }
    }
}


