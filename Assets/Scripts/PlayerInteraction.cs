using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public static PlayerInteraction instance;

	void Awake () 
    {
		instance = this;
	}
	
	void Update () 
    {
        if (Input.GetButtonDown("confirm"))
        {
            GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
            Vector3 checkPos = GetComponent<PlayerMovement>().GetForwardPos();
            
            foreach (GameObject interactable in interactables)
            {
                Interactable interaction = interactable.GetComponent<Interactable>();
                if (interaction.InPoint(checkPos))
                {
                    interaction.Interact();
                }
            }
        }
	}
}
