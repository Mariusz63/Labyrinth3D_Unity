//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MinimapScript : MonoBehaviour
//{
//    public Transform player;

//    private void LateUpdate()
//    {
//        Vector3 newPosition = transform.position;
//        newPosition.y = transform.position.y;
//        transform.position = newPosition;
//    }
//}


// observer (Minimap)
using Assets.Scripts.CharacterScripts;
using UnityEngine;

public class MinimapScript : MonoBehaviour, IPlayerObserver
{
    private void Start()
    {
        // Assuming there is only one player in the scene
        PlayerSubject player = FindObjectOfType<PlayerSubject>();
        if (player != null)
        {
            player.RegisterObserver(this);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        Vector3 updatedPosition = transform.position;
        updatedPosition.x = newPosition.x;
        updatedPosition.z = newPosition.z; // Assuming Y is the up axis
        transform.position = updatedPosition;
    }
}