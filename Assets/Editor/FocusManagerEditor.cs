using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(FocusManager))]
public class FocusManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        FocusManager focusmanager = (FocusManager)target;

        focusmanager.focusLayer = EditorGUILayout.LayerField("FocusLayer", focusmanager.focusLayer);
        focusmanager.unfocusLayer = EditorGUILayout.LayerField("UnFocusLayer", focusmanager.unfocusLayer);
    }
}
