using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour 
{
    public static DialogueSystem instance;

    public GameObject speakerPanel;
    public GameObject linePanel;

    TextMeshProUGUI speaker;
    TextMeshProUGUI line;

    string[] speakers;
    string[] lines;
    int lineNum = 0;

    bool inputReady = false;

    void Awake ()
    {
        instance = this;
        speaker = speakerPanel.GetComponentInChildren<TextMeshProUGUI>();
        line = linePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

	void Update () 
    {
        if (PlayerInteraction.instance.enabled == false)
        {
            if (inputReady)
            {
                if (Input.GetButtonDown("confirm"))
                {
                    // Check if we're past dialogue line count
                    if (lineNum < lines.Length)
                    {
                        LoadLine(lineNum++);
                    }
                    else
                    {
                        UnloadDialogue();
                    }
                }
            }
            else
            {
                inputReady = true;
            }
        }
    }

    // TODO Remake this with a Dialogue Object later
    public void LoadDialogue(string[] speakers, string[] lines)
    {
        speakerPanel.SetActive(true);
        linePanel.SetActive(true);
        PlayerInteraction.instance.enabled = false;
        
        this.speakers = speakers;
        this.lines = lines;
        lineNum = 0;

        LoadLine(lineNum++);
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        speakerPanel.SetActive(true);
        linePanel.SetActive(true);
        PlayerInteraction.instance.enabled = false;

        this.speakers = dialogue.GetSpeakers();
        this.lines = dialogue.GetLines();
    }

    public void LoadLine(int lineNum)
    {
        speaker.text = speakers[lineNum];
        if (speaker.text == "") 
            speakerPanel.SetActive(false);
        else
            speakerPanel.SetActive(true);

        // TODO Create coroutine to load in text piecemeal
        line.text = lines[lineNum];
    }

    public void UnloadDialogue()
    {
        speakerPanel.SetActive(false);
        linePanel.SetActive(false);
        PlayerInteraction.instance.enabled = true;
        inputReady = false;
    }
}
