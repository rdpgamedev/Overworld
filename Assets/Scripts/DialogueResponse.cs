// Response object to a DialogueLine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueResponse : MonoBehaviour
{
    public GameObject textObject;

    public string Response { get; set; }
    public string Flag { get; set; }
    public DialogueLine nextLine;

    public void CreateResponse(string response, DialogueLine nextLine, string flag = "")
    {
        Response = response;
        textObject.GetComponent<Text>().text = response;
        this.nextLine = nextLine;
        Flag = flag;
    }

    public void Choose()
    {
        // Apply Flag

        DialogueSystem.instance.LoadLine(nextLine);
    }
}