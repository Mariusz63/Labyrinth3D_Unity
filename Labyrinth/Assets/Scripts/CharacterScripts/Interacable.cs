using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interacable : MonoBehaviour
{
    public virtual void Awake()
    {
        // interactable object have layer number 9
        gameObject.layer = 9; 
    }

    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();

}
