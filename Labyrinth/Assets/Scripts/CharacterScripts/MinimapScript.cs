using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }


}
