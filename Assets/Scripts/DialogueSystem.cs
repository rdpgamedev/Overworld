using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour 
{
    public static DialogueSystem instance;

    public GameObject speakerPanel;
    public GameObject linePanel;
    public GameObject responsePanel;
    public GameObject responsesObject;

    public GameObject responsePrefab;

    public float setSpeed = 10f;

    float speed;

    TextMeshProUGUI speaker;
    TextMeshProUGUI line;

    string[] speakers;
    string[] lines;
    int lineNum = 0;

    DialogueLine currentLine;

    bool inputReady = false;
    bool choosingResponse = false;
    bool removedResponses = false;

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
            if (inputReady && !choosingResponse)
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
            else if (!inputReady && !removedResponses)
            {
                if (Input.GetButtonDown("confirm"))
                {
                    speed = 100f;
                }
            }
        }

        if (removedResponses)
            removedResponses = false;
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
        // Sets responses at completion of coroutine
        StartCoroutine(LoadText(line.Line));

        // Apply Flag
    }

    public IEnumerator LoadText(string text)
    {
        if (text == null) 
        {
            text = "";
        }

        float charCount = 0;
        speed = setSpeed;
        inputReady = false;

        SetResponses(currentLine.Responses);

        while (charCount < text.Length)
        {
            line.text = text.Substring(0, (int)charCount);
            charCount += Time.deltaTime * speed;
            yield return null;
        }

        line.text = text;

        if (currentLine.Responses != null && currentLine.Responses.Length > 0)
        {
            responsePanel.SetActive(true);
            currentLine.Responses[0].gameObject.GetComponent<Button>().Select();
            choosingResponse = true;
        }
        else
        {
            inputReady = true;
        }

        yield return 0;
    }

    public void SetResponses(DialogueResponse[] responses)
    {
        if (responses == null || responses.Length == 0)
        {
            choosingResponse = false;
            removedResponses = true;
            foreach (Transform childTransform in responsesObject.transform)
            {
                Object.Destroy(childTransform.gameObject);
            }
            responsePanel.SetActive(false);
            return;
        }

        float RESPONSE_MARGIN = 30f;
        
        Vector3 oldPos = responsesObject.transform.position;
        foreach (var response in responses)
        {
            response.transform.SetParent(responsesObject.transform);
            response.transform.position = oldPos + Vector3.down * RESPONSE_MARGIN;
            oldPos = response.transform.position;
        }
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
