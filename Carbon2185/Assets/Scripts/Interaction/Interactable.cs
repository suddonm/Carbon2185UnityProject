using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 2f;
    public Transform interactionTransform;

    private bool isFocus = false;
    private Transform player;

    private bool hasInteracted = false;

    protected GameManager gameManager;

    public Color mouseOverColor;

    public virtual void Interact()
    {
        //This method is meant to be overwritten
    }

    public virtual void EndInteraction()
    {
        //This method is meant to be overwritten
    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius + 0f)
            {
                Interact();
                hasInteracted = true;

                Debug.Log("interacting");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused ()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;

        EndInteraction();
    }
}
