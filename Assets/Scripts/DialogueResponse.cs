// Response object in to a DialogueLine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueResponse : MonoBehaviour
{
    public string Response { get; set; }
    public string Flag { get; set; }
    public DialogueLine nextLine;

    public void CreateResponse(string response, DialogueLine nextLine, string flag = "")
    {
        Response = response;
        this.nextLine = nextLine;
        Flag = flag;
    }

    public void Choose()
    {
        // Apply Flag

        DialogueSystem.instance.LoadLine(nextLine);
    }
}