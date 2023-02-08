using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusManager : MonoBehaviour
{
    [SerializeField]int current = 0;
    [SerializeField] List<PawnController> pawns;
    [SerializeField] Transform LeftCam;
    [SerializeField] Transform RightCam;
    float noControllTimeCurrent = 0;
    [SerializeField] float noControllTime = 10;
    [SerializeField] InfomationDisplay infomationDisplay;
    public float focusChangeTimeCurrent { get; private set; } = 0;
    [HideInInspector] public int unfocusLayer;
    [HideInInspector] public int focusLayer;

    [SerializeField] Texture2D mapFocusedBox;
    [SerializeField] Texture2D mapBox;

    [SerializeField] GUIStyle mapBoxStyle;
    // Start is called before the first frame update
    void Start()
    {
        infomationDisplay.SetPawn(pawns[current]);

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pawns.Count; i++)
        {
            pawns[i].SetFocus(i == current);
            pawns[i].SetModelLayer(i == current ? focusLayer : unfocusLayer);
        }
        LeftCam.position = pawns[PrevIdx()].GetFrontFixCam().position;
        LeftCam.rotation = pawns[PrevIdx()].GetFrontFixCam().rotation;
        RightCam.position = pawns[NextIdx()].GetFrontFixCam().position;
        RightCam.rotation = pawns[NextIdx()].GetFrontFixCam().rotation;
        noControllTimeCurrent += Time.deltaTime;
        focusChangeTimeCurrent += Time.deltaTime;
        if (Input.GetMouseButton(0) || noControllTime <= 0)
        {
            noControllTimeCurrent = 0;
        }
        if (noControllTimeCurrent >= noControllTime)
        {
            noControllTimeCurrent = 0;
            Next();
        }
    }

    public void Next()
    {
        current = NextIdx();
        focusChangeTimeCurrent = 0;
        infomationDisplay.SetPawn(pawns[current]);
    }

    public void Prev()
    {
        current = PrevIdx();
        focusChangeTimeCurrent = 0;
        infomationDisplay.SetPawn(pawns[current]);

    }

    int NextIdx()
    {
        int idx = current;
        idx++;
        if (idx >= pawns.Count)
        {
            idx = 0;
        }
        return idx;
    }

    int PrevIdx()
    {
        int idx = current;
        idx--;
        if (idx < 0)
        {
            idx = pawns.Count -1;
        }
        return idx;
    }
    public void SetPawns(List<GameObject> objects)
    {
        pawns.Clear();
        foreach (var item in objects)
        {
            PawnController pawn = item.GetComponent<PawnController>();
            if (pawn != null)
            {
                pawns.Add(pawn);
            }
        }
    }

    private void OnGUI()
    {
        Rect map = new Rect(1920 / 2 - 300, 1080 - 30, 600, 30);
        Vector2 size = new Vector2(map.size.x / pawns.Count, map.size.y);
        float gap = 10;
        GUILayout.BeginArea(map);
        Texture2D image;
        for (int i = 0; i < pawns.Count; i++)
        {
           
            if (i == current)
            {
                image = mapFocusedBox;
            }
            else
            {
                image = mapBox;
            }
            GUI.Box(new Rect((size.x * i) + (gap), 0, size.x - gap, size.y), image, mapBoxStyle);
        }
        GUILayout.EndArea();
    }
}
