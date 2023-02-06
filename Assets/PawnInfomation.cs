using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurnTable/PawnInfomation")]
public class PawnInfomation : ScriptableObject
{
    public string ModelName;
    [Multiline(3)]
    public string Description;
    public string Date;
    public string AuthorName;
    public string AuthorLink;
    public string UsedTools;

    public PawnInfomation()
    {
        this.ModelName = "string:ModelName;";
        this.Description = "string:Description\nstring:Description\nstring:Description";
        this.Date = "string:Date;";
        this.AuthorName = "string:AuthorName;";
        this.AuthorLink = "string:AuthorLink;";
        this.UsedTools = "_bl_ps";
    }
}