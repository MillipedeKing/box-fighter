using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public float damage = 20;
    public Component collision;
    void Start()
    {
        collision = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if (collision.gameObject.tag == "player")
      {
       IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
     
      if (iDamageable != null)
      {
        iDamageable.Damage(damage);
      }
      }
   
    }
    }
    

