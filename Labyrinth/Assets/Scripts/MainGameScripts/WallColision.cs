using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method executed for: " + gameObject.name);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.35f);

        foreach (Collider collider in colliders)
        {
            Debug.Log("WallCollision: " + gameObject.name);

            if (collider.tag == "Wall")
            {
                Debug.Log("Destroy: " + gameObject.name);
                Destroy(gameObject);
                return;
            }
        }

        // Enable the collider if no "Wall" was found
        GetComponent<Collider>().enabled = true;
    }

}