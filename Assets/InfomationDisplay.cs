using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InfomationDisplay : MonoBehaviour
{
    [SerializeField] FocusManager focusManager;
    [SerializeField] float characterUnit = 0.1f;
    float prefocustime = 0;
    float charUnitCurrent = 0;
    int charcount = 0;
    [SerializeField] PawnInfomationHandler pawn;
    PawnInfomationData info;
    [SerializeField] ToolIconDefine toolIconDefine;
    [SerializeField] GUIStyle ModelNameStyle;
    [SerializeField] GUIStyle DescriptionStyle;
    [SerializeField] GUIStyle DateStyle;
    [SerializeField] GUIStyle AuthorNameStyle;
    [SerializeField] GUIStyle AuthorLinkStyle;
    [SerializeField] GUIStyle ToolIconsStyle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        charUnitCurrent += focusManager.focusChangeTimeCurrent - prefocustime;
        if (charUnitCurrent >= characterUnit)
        {
            charcount++;
            charUnitCurrent = 0;
        }
        prefocustime = focusManager.focusChangeTimeCurrent;
    }

    public void SetPawn(PawnController pcont)
    {
        pawn = pcont.GetComponent<PawnInfomationHandler>();
        charcount = 0;
        prefocustime = 0;
    }

    private void OnGUI()
    {
        if (pawn != null)
        {
            info = pawn.info.data;
        }
        else
        {
            info = new PawnInfomationData();
        }
        GUI.Box(new Rect(0, 0, 100, 100), info.ModelName.Substring(0,Math.Min(charcount, info.ModelName.Length)),   ModelNameStyle);
        GUI.Box(new Rect(0, 0, 100, 100), info.Description.Substring(0, Math.Min(charcount, info.Description.Length)), DescriptionStyle);
        GUI.Box(new Rect(0, 0, 100, 100), info.AuthorName.Substring(0, Math.Min(charcount, info.AuthorName.Length)), AuthorNameStyle);
        GUI.Box(new Rect(0, 0, 100, 100), info.Date.Substring(0, Math.Min(charcount, info.Date.Length)), DateStyle);
        GUI.Box(new Rect(0, 0, 100, 100), info.AuthorLink.Substring(0, Math.Min(charcount, info.AuthorLink.Length)), AuthorLinkStyle);

        List<Texture2D> icontexs = new List<Texture2D>();
        foreach (var item in toolIconDefine.icons)
        {
            if (info.UsedTools.Contains(item.chara))
            {
                icontexs.Add(item.icon);
            }
        }
        for (int i = 0; i < icontexs.Count; i++)
        {

            Rect rect_icon = new Rect(ToolIconsStyle.contentOffset.x + i * ToolIconsStyle.fontSize* 1.1f, ToolIconsStyle.contentOffset.y, ToolIconsStyle.fontSize, ToolIconsStyle.fontSize);
            GUI.DrawTexture(rect_icon, icontexs[i]);
        }
    }

    [ContextMenu("Redraw")]
    private void Redraw()
    {
        charcount = 0;
    }

    [ContextMenu("Draw")]
    private void Draw()
    {
        charcount = 1000;
    }
}
