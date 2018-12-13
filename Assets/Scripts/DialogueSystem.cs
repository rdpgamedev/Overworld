using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour 
{
    public static DialogueSystem instance;

    public GameObject speakerPanel;
    public GameObject linePanel;
    public GameObject responsePanel;

    public float setSpeed = 10f;

    float speed;

    TextMeshProUGUI speaker;
    TextMeshProUGUI line;

    string[] speakers;
    string[] lines;
    int lineNum = 0;

    DialogueLine currentLine;

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
                    //if (lineNum < lines.Length)
                    if (currentLine.NextLine != null)
                    {
                        //LoadLine(lineNum++);
                        LoadLine(currentLine.NextLine);
                    }
                    else
                    {
                        UnloadDialogue();
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("confirm"))
                {
                    speed = 100f;
                }
            }
        }
    }

    public void LoadDialogue(string[] speakers, string[] lines)
    {
        speakerPanel.SetActive(true);
        linePanel.SetActive(true);
        PlayerInteraction.instance.enabled = false;
        PlayerMovement.instance.enabled = false;
        
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
        PlayerMovement.instance.enabled = false;

        LoadLine(dialogue.First());
    }

    public void LoadLine(int lineNum)
    {
        speaker.text = speakers[lineNum];
        if (speaker.text == "") 
            speakerPanel.SetActive(false);
        else
            speakerPanel.SetActive(true);

        StartCoroutine(LoadText(lines[lineNum]));
    }

    public void LoadLine(DialogueLine line)
    {
        currentLine = line;
        
        // Load Speaker
        speaker.text = line.Speaker;
        if (speaker.text == "")
        {
            speakerPanel.SetActive(false);
        }
        else
        {
            speakerPanel.SetActive(true);
        }
        
        // Load Line
        StartCoroutine(LoadText(line.Line));

        // Load Responses
        DialogueResponse[] responses = line.Responses;
        if (responses != null)
        {
            responsePanel.SetActive(true);
        }
        else
        {
            responsePanel.SetActive(false);
        }

        // Apply Flag
    }

    public IEnumerator LoadText(string text)
    {
        if (text == null || text == "") 
        {
            line.text = "";
            yield return 0;
        }

        float charCount = 0;
        speed = setSpeed;
        inputReady = false;

        while (charCount < text.Length)
        {
            line.text = text.Substring(0, (int)charCount);
            charCount += Time.deltaTime * speed;
            yield return null;
        }

        line.text = text;
        inputReady = true;
        yield return 0;
    }

    public void UnloadDialogue()
    {
        speakerPanel.SetActive(false);
        linePanel.SetActive(false);
        responsePanel.SetActive(false);
        PlayerInteraction.instance.enabled = true;
        PlayerMovement.instance.enabled = true;
        inputReady = false;
    }
}
