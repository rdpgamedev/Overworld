// Class to hold DialogueLines

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dialogue {

    List<DialogueLine> lines;

    public Dialogue()
    {
        lines = new List<DialogueLine>();
    }

    public Dialogue(string file)
    {
        lines = new List<DialogueLine>();
        // Read file
        LoadDialogueFile(file);
    }

    public DialogueLine First()
    {
        return (lines.Count > 0 ? lines[0] : null);
    }

    public string[] GetSpeakers()
    {
        string[] speakers = new string[lines.Count];
        for (int i = 0; i < lines.Count; ++i)
        {
            speakers[i] = lines[i].Speaker;
        }
        return speakers;
    }

    public string[] GetLines()
    {
        return new string[0];
    }

    public void AddLine(DialogueLine line)
    {
        lines.Add(line);
    }

    private void LoadDialogueFile(string path)
    {
        lines.Clear();
        // TODO read in dialogue file and populate dialogue object

        // Placeholder dialogue creation to test response stuff
        string speaker = "Professor";
        string[] lineText = 
        {
            "I am the professor.", 
            "Are you a $ or a &?", 
            "This shouldn't be read. Stop it.", 
            "Oh, a $ huh? Weird.", 
            "Oh, a & huh? Odd.",
            "Thank you for answering that."
        };

        DialogueLine prevLine = null;
        for (int i = 0; i < lineText.Length; ++i)
        {
            DialogueLine line = new DialogueLine(speaker, lineText[i]);            
            if (prevLine != null) prevLine.NextLine = line;

            AddLine(line);
            prevLine = line;
        }

        // Tweak lines
        lines[3].NextLine = lines[5];

        // Populate responses
        DialogueResponse[] responses = new DialogueResponse[2];

        GameObject responsePrefab = DialogueSystem.instance.responsePrefab;

        responses[0] = UnityEngine.Object.Instantiate(responsePrefab).GetComponent<DialogueResponse>();
        responses[0].CreateResponse("I am a $.", lines[3]);
        
        responses[1] = UnityEngine.Object.Instantiate(responsePrefab).GetComponent<DialogueResponse>();
        responses[1].CreateResponse("I am a &, man.", lines[4]);

        lines[1].Responses = responses;
    }
}
