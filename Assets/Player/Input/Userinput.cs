using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Serialization;
using UnityEngine;

public class Userinput : MonoBehaviour
{
public static Userinput instance;

[HideInInspector] public Controls controls;
public UnityEngine.Vector2 moveinput;

public UnityEngine.Vector2 Vector2;
private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new Controls();

        controls.Gameplay.Move.performed += ctx => ctx.ReadValue<UnityEngine.Vector2>();
    }

    private void OnEnable()
    {
        // controls.Enable();
    }

    private void OnDisable()
    {
        controls.Enable();
    }
}
