// Narrative unit that holds a speaker and what is spoken

public class DialogueLine
{
    public DialogueLine nextLine = null;    
    public string speaker { get; set; }
    public string line { get; set; }

    public DialogueLine(string speaker, string line, DialogueLine nextLine)
    {
        this.speaker = speaker;
        this.line = line;
        this.nextLine = nextLine;
    }
}