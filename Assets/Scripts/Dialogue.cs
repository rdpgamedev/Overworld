// Class to hold DialogueLines

using System;
using System.Collections.Generic;

public class Dialogue {

    List<DialogueLine> lines;

    public Dialogue()
    {
        lines = new List<DialogueLine>();
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
}
