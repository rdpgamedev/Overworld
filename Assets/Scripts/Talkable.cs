using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        //DialogueSystem.instance.LoadDialogue(speakers, lines);
        Dialogue dialogue = new Dialogue("thisIsAPath");

        // DialogueLine prevLine = null;
        // for (int i = 0; i < speakers.Length; ++i)
        // {
        //     DialogueLine line = new DialogueLine(speakers[i], lines[i]);
        //     if (prevLine != null) prevLine.NextLine = line;

        //     dialogue.AddLine(line);
        //     prevLine = line;
        // }
        
        DialogueSystem.instance.LoadDialogue(dialogue);
    }
}
