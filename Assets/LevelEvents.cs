using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector2 defaultgravity;
    public static float gravity;
    public void Start()
    {
        
        Physics2D.gravity = new Vector2(0, -9.8f);
    }
    public static void StartEvent()
    {
          gravity = System.MathF.Round(UnityEngine.Random.Range( -15 , 4));
          defaultgravity = Physics2D.gravity;
          Physics2D.gravity = new Vector2(0, gravity);
          Debug.Log("Gravity was " + defaultgravity.ToString() + " and is now " + Physics2D.gravity.ToString());
    }
  
}
