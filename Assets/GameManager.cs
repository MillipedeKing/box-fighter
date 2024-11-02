using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> players = new() {};

    public void OnPlayerJoined(PlayerInput newPlayer) {
        PlayerController newPlayerController = newPlayer.gameObject.GetComponent<PlayerController>();
        players.Add(newPlayerController);
        newPlayerController.playerId = "P" + players.Count;
    }
}
