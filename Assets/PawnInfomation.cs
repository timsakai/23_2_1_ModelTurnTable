using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnInfomationData 
{
    public string ModelName = "string:ModelName;";
    [Multiline(3)]
    public string Description = "string:Description\nstring:Description\nstring:Description";
    public string Date = "string:Date;";
    public string AuthorName = "string:AuthorName;";
    public string AuthorLink = "string:AuthorLink;";
    public string UsedTools = "_bl_ps";
}
[CreateAssetMenu(menuName = "TurnTable/PawnInfomation")]
public class PawnInfomation : ScriptableObject
{
    public PawnInfomationData data;

}