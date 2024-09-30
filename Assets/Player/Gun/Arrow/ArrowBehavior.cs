using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
  [SerializeField] public float arrowSpeed = 50;
  public float arrowSpeedMultiplier = 0.2f;
  private Rigidbody2D rb;
  private bool isActive;

  public float damage = 10;


  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    SetStraightVelocity();
    isActive = true;
  
  }
  private void SetStraightVelocity()
  {
    rb.AddForce(transform.right * arrowSpeed * arrowSpeedMultiplier);
    
  }

  private void FixedUpdate()
  {
    if (isActive)
    {
      transform.right = rb.velocity;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
    {
      isActive = false;
       IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
      if (iDamageable != null)
      {
        //damage thing
        iDamageable.Damage(damage);
      }
    }
}
