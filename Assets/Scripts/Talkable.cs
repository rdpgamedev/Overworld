using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : Interactable 
{

    public string[] speakers;
    public string[] lines;

	void Start () 
    {
		
	}
    
    public override void Interact()
    {
        DialogueSystem.instance.LoadDialogue(speakers, lines);
    }
}
