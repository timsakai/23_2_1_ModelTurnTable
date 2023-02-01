using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PawnGenerator))]
public class PawnGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //����Inspector������\��
        base.OnInspectorGUI();
        PawnGenerator pawnGenerator = (PawnGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            pawnGenerator.Generate();
        }
    }
}
