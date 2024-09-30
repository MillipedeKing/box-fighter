using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class GrapplerBehavior : MonoBehaviour
{
  [SerializeField] public float normalGrapplerSpeed = 50;
  private Rigidbody2D rb;
  

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    SetStraightVelocity();
  }
  private void SetStraightVelocity()
  {
    rb.AddForce(transform.right * normalGrapplerSpeed);
  }

}
