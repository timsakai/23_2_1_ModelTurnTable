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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pawns.Count; i++)
        {
            pawns[i].SetFocus(i == current);
        }
        LeftCam.position = pawns[PrevIdx()].GetFrontFixCam().position;
        LeftCam.rotation = pawns[PrevIdx()].GetFrontFixCam().rotation;
        RightCam.position = pawns[NextIdx()].GetFrontFixCam().position;
        RightCam.rotation = pawns[NextIdx()].GetFrontFixCam().rotation;
        noControllTimeCurrent += Time.deltaTime;
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
    }

    public void Prev()
    {
        current = PrevIdx();
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
}
