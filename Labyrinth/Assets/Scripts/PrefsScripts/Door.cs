using Assets.Scripts.Door;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : Interacable
{
    [Header("Door Properties")]
    [SerializeField] private bool canBeInteractedWith = true;
    [SerializeField] public GameObject door;
    private DoorOpenController mediator;

    private void Start()
    {
        mediator = GetComponent<DoorOpenController>();     
    }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (canBeInteractedWith)
        {

            print("INTERACT WITH AT " + gameObject.name);
            // Call ToggleDoor method of the mediator
            mediator.ToggleDoor();
        }
    }

    public override void OnLoseFocus()
    {

    }


}
