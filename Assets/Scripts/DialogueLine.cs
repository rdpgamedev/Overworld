// Narrative unit that holds a speaker, what is spoken, 
// what is spoken next, possible responses, and a flag

public class DialogueLine
{
    public string Speaker { get; set; }               // Name of speaker of line
    public string Line { get; set; }                  // Line spoken or read
    public DialogueLine NextLine { get; set; }        // Next line in the dialogue, null if last line in dialogue
    public DialogueResponse[] Responses { get; set; } // Responses to this line, usually null
    public string Flag { get; set; }                  // Optional flag to be placed on player when entering this line


/*** Constructors ***/

    public DialogueLine(string speaker, 
                        string line, 
                        DialogueLine nextLine = null, 
                        DialogueResponse[] responses = null, 
                        string flag = "")
    {
        Speaker = speaker;
        Line = line;
        NextLine = nextLine;
        Responses = responses;
        Flag = flag;
    }

/*** Selectors ***/

/*** Modifiers ***/
}