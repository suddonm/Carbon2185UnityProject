using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : Interactable
{
    public override void Interact()
    {
        gameManager.ShowDialogBox();
        Debug.Log("Interacted With: " + this.name);
    }

    public override void EndInteraction()
    {
        gameManager.CloseDialogBox();
        Debug.Log("Ended Interaction With: " + this.name);
    }
}