using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Room room = (Room)target;
        if (GUILayout.Button("Update from Texture"))
        {
            room.Regenerate();
        }
    }

}
