using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : Interacable
{
    private bool isOpen;
    private bool canBeInteractedWith = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (canBeInteractedWith)
        {
            isOpen = !isOpen;

            Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
            Vector3 playerTransformDirection = FirstPersonController.instance.transform.position - transform.position;
            float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);

            anim.SetFloat("dot", dot);
            anim.SetBool("isOpen", isOpen);
        }
    }

    public override void OnLoseFocus()
    {

    }
}
