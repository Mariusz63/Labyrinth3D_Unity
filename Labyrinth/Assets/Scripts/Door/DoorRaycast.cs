using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [Header("Door Parameters")]
    [SerializeField] private int rayLenght = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
   
    private Door raycastedObject; // Door

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                raycastedObject = hit.collider.gameObject.GetComponent<Door>();
                if (raycastedObject != null)
                {
                    CrosshairChange(true);
                }
            }

            isCrosshairActive = true;
            doOnce = true;

            if (Input.GetKeyDown(openDoorKey) && raycastedObject != null)
            {
                raycastedObject.OnInteract();
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    private void CrosshairChange(bool on)
    {
        if(on && !doOnce)
        {
            crosshair.color = Color.red;
        }else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}
