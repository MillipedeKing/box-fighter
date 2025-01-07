using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D myCollider;
    SpriteRenderer myRender;
    void Start()
    {
          myCollider = gameObject.GetComponent<Collider2D>();
          myRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "blue")
         {
            myRender.color = new Color(0, 0, 207, 255); 
         }
         if (collision.gameObject.tag == "Green")
         {
            myRender.color = new Color(0, 147, 0, 255); 
         }
           if (collision.gameObject.tag == "yellow")
         {
            myRender.color = new Color(233, 238, 0, 255); 
         }
            if (collision.gameObject.tag == "red")
         {
            myRender.color = new Color(255, 0, 0, 255); 
         }
              if (collision.gameObject.tag == "pink")
         {
            myRender.color = new Color(255, 0, 255, 255); 
         }
    }
}
