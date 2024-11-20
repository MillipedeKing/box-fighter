using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector2 defaultgravity;
    public void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.8f);
    }
    public static void StartEvent()
    {
          defaultgravity = Physics2D.gravity;
          Physics2D.gravity = new Vector2(0, -8.8f);
    }
  
}
