using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class faceCollection : MonoBehaviour
{
   // Start is called before the first frame update
    Collider2D myCollider;
    void Start()
    {
          myCollider = gameObject.GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
         {
           transform.position = collision.transform.position;
           transform.parent = collision.transform;
         }
    }

}
