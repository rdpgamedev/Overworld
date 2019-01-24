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

        /*******************************/
        /*** .dlg File Documentation ***/
        /*******************************/

        // A Dialogue File is groups of lines split by solo newline character
        // Groups of single lines are Dialogue Lines
        // Groups with multiple lines are Dialogue-Choices:
        //    A Dialogue Line followed by a list of Choices

        // Each Dialogue Line has this structure:
        //
        // [LINE_LABEL] "SPEAKER": "CONTENT" <FLAGS, TO, MODIFY> [NEXT_LINE_1]:<FLAGS, FOR, SELECTION, ...> [NEXT_LINE_2]:<FLAGS, ...> ... [NEXT_LINE_DEFAULT]
        //

        // Each Dialogue-Choice has this structure:
        //
        // [LINE_LABEL] "SPEAKER": "CONTENT" <FLAGS, TO, MODIFY> 
        // "CHOICE_PROMPT_1" <FLAGS, TO, MODIFY> [NEXT_LINE_A1]:<FLAGS, FOR, SELECTION, ...> ... [NEXT_LINE_DEFAULT_A]
        // "CHOICE_PROMPT_2" <FLAGS, TO, MODIFY> [NEXT_LINE_B1]:<FLAGS, ...> ... [NEXT_LINE_DEFAULT_B]
        // ...
        //

        // Special Requirements:
        // 
        // All strings (words) are case sensitive
        //
        // All [LINE_LABELS] can be any combination of letters and spaces except for Special [LINE_LABELS] listed below.
        // They must be wrapped in square brackets []
        //
        // All FLAGS must be one string (of characters, i.e. a word) without spaces or commas
        // Lists of flags are separated via commas ,
        // Each list of flags (even lists of one) must be wrapped in angle brackets <>
        //
        // All standalone Dialogue Lines and Dialogue Choices must end with at least a default [NEXT_LINE]
        //

        // Special [LINE_LABELS]:
        //
        // []
        // An empty line label signals the end of the dialogue when used as a [NEXT_LINE]
        // A line labeled as [] will be ignored.
        //
        // [REPEAT] 
        // A line labeled as [REPEAT] will be used as the entry line for when the player has already spoken with the dialogue agent.
        // There should only be one of these lines, so any line labeld as [REPEAT] after the first one in the dialogue will be ignored.
        //

        // Special Conditions:
        //
        // Conditional Dialogue Paths
        // A [LINE] followed immediately by a colon : and a list of <FLAGS> is a conditional dialogue path that the dialogue
        // will take if the flags are present
        //
        // Logic Lines
        // A line with no speaker and no dialogue line goes directly into the next line logic
        // This can be used to control which dialogue path to take on the first dialogue line, for instance
        //
        // Random Line Selection
        // A line with multiple default next lines will randomly choose from them.
        //

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
