using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 
public class GameControllers : MonoBehaviour
{
    private bool connected = false;
    public Transform PlayerSpawn;
    public GameObject Player;
   

    public void Update()
    {
        StartCoroutine(CheckForControllers());
    }

    IEnumerator CheckForControllers() {
         {
            var controllers = Input.GetJoystickNames();

            if (!connected && controllers.Length > 0) {
                connected = true;
                Debug.Log("Connected");
                Player = Instantiate(Player,PlayerSpawn.position, PlayerSpawn.transform.rotation);
            
            } else if (connected && controllers.Length == 0) {         
                connected = false;
                Debug.Log("Disconnected");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void Awake() {
        StartCoroutine(CheckForControllers());
    }
}