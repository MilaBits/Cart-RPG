using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RotateStuff))]
public class RotateStuffEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        RotateStuff rotateStuff = (RotateStuff)target;
        if (GUILayout.Button("Rotate Objects")) {
            rotateStuff.Rotate();
        }
    }
}