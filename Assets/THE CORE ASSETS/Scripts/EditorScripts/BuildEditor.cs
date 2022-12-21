using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Character_AI))]
public class BuildEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Character_AI aiscript = (Character_AI)target;
        if (GUILayout.Button("voeg Wander script toe"))
        {
            aiscript.addwander();
        }
        if (GUILayout.Button("voeg Actie (enkel) script toe"))
        {
            aiscript.addActionSingle();
        }
        if (GUILayout.Button("voeg Actie (geloopt) script toe"))
        {
            aiscript.addActionLoop();
        }
        if (GUILayout.Button("Voeg Interactie script toe"))
        {
            aiscript.addInteraction();
        }
    }
}
