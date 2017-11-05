using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateStuff : EditorWindow {
    public List<GameObject> ObjectsToTurn;

    [MenuItem("Custom Tools/Rotate Objects", false, 2000)]
    static void OpenWindow() {
        EditorWindow.GetWindow<RotateStuff>(true);
    }

    //default fields
    private Vector2 size = new Vector2(300, 115);
    bool x = false;
    bool y = false;
    bool z = false;

    //advanced fields
    bool advanced = false;
    float xMax = 360f;
    float yMax = 360f;
    float zMax = 360f;

    void OnGUI() {
        titleContent = new GUIContent("Rotate Objects");
        maxSize = size;
        minSize = size;

        GUILayout.BeginHorizontal();
        GUILayout.Label("Rotate");
        x = EditorGUILayout.ToggleLeft("X", x, GUILayout.Width(50));
        y = EditorGUILayout.ToggleLeft("Y", y, GUILayout.Width(50));
        z = EditorGUILayout.ToggleLeft("Z", z, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        advanced = EditorGUILayout.ToggleLeft("Advanced", advanced);
        if (advanced) {
            //TODO: Make min slider work

            xMax = EditorGUILayout.Slider(xMax, 0, 360);
            yMax = EditorGUILayout.Slider(yMax, 0, 360);
            zMax = EditorGUILayout.Slider(zMax, 0, 360);
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotate Objects")) {
            if (Selection.gameObjects == null || Selection.gameObjects.Length < 1) {

                EditorUtility.DisplayDialog("No object(s) selected", "Please select at least one object.", "Ok");
                return;
            } else {
                Rotate();
            }
        }
        if (GUILayout.Button("Reset Rotations")) {
            if (Selection.gameObjects == null || Selection.gameObjects.Length < 1) {

                EditorUtility.DisplayDialog("No object(s) selected", "Please select at least one object.", "Ok");
                return;
            } else {
                ResetRotations();
            }
        }
        GUILayout.EndHorizontal();
    }

    public void Rotate() {
        foreach (var gameObject in Selection.gameObjects) {
            var euler = gameObject.transform.eulerAngles;
            if (x)
                euler.x += Random.Range(0, xMax);
            if (y)
                euler.y += Random.Range(0, yMax);
            if (z)
                euler.z += Random.Range(0, zMax);
            gameObject.transform.eulerAngles = euler;
        }
    }

    public void ResetRotations() {
        foreach (var gameObject in Selection.gameObjects) {
            var euler = gameObject.transform.eulerAngles;
            euler.z = 0;
            euler.z = 0;
            euler.z = 0;
            gameObject.transform.eulerAngles = euler;
        }
    }
}