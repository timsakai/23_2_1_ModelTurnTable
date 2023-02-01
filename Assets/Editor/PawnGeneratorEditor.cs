using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PawnGenerator))]
public class PawnGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Œ³‚ÌInspector•”•ª‚ð•\Ž¦
        base.OnInspectorGUI();
        PawnGenerator pawnGenerator = (PawnGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            pawnGenerator.Generate();
        }
    }
}
