using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PawnGenerator))]
public class PawnGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();
        PawnGenerator pawnGenerator = (PawnGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            pawnGenerator.Generate();
        }
    }
}
