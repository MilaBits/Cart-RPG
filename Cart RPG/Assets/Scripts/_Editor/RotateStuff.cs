using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotateStuff : MonoBehaviour
{
    public List<GameObject> ObjectsToTurn;
	
    public void Rotate()
    {
        foreach (var gameObject in ObjectsToTurn) {
            var euler = gameObject.transform.eulerAngles;
            euler.z += Random.Range(0, 360);
            gameObject.transform.eulerAngles = euler;
        }
    }
}