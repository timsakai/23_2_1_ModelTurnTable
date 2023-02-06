using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharIcon
{
    public string chara;
    public Texture2D icon;
}
[CreateAssetMenu(menuName = "TurnTable/ToolIconDefine")]
public class ToolIconDefine : ScriptableObject
{
    public List<CharIcon> icons;
}
