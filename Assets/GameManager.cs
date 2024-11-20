using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<PlayerController> players = new() {};
    private List<String> levels= new() {"Multiplayer","Forest 1",};
    public MultipleTargetCamera cam;
    public float events;


    public void OnPlayerJoined(PlayerInput newPlayer) {
        PlayerController newPlayerController = newPlayer.gameObject.GetComponent<PlayerController>();
        players.Add(newPlayerController);
        newPlayerController.playerId = "P" + players.Count;
        cam.targets.Add(newPlayer.transform);

    }

    public void Update()
    {
        RandomEvent();
    }
    public void OnPlayerDied()
    {
        int playersAlive = 0;
        foreach (var player in players)
        {
            if (player.isDead == false)
            {
                playersAlive += 1;
            }
        }
            if (playersAlive <= 1)
            {
                LoadNextLevel();
            }
    }

    public void LoadNextLevel()
    {
        Physics2D.gravity = new Vector2(0, -9.8f);
        int sceneIndex = (int)MathF.Round(UnityEngine.Random.Range( 0 , levels.Count));
        Debug.Log(sceneIndex);
        SceneManager.LoadScene(levels[sceneIndex]);

    }

    public void Awake()
    {
        instance = this;
        cam = Camera.main.GetComponent<MultipleTargetCamera>();
        Physics2D.gravity = new Vector2(0, -9.8f);
    }

    public void RandomEvent()
    {
        
        events = MathF.Round(UnityEngine.Random.Range(0,1000));
        if (events == 1)
        {
            //event happens!
            // LevelEvents.StartEvent();
            Debug.Log(events);    
        }
        
    }
}
