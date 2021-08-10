using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Snapper
{
    const string UNDO_SNAPP  ="Snapped objects";
    
    [MenuItem("Test/Snap %#q", isValidateFunction:true)]
    public static bool SnapSelectable()
    {
        return Selection.gameObjects.Length > 0;
    }
    
    [MenuItem("Test/Snap %#q")]
    public static void SnapTest()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, UNDO_SNAPP);
            gameObject.transform.position = gameObject.transform.position.Round();
        }
    }

    [MenuItem("Test/PrintNames")]
    public static void PrintNames()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Debug.Log(" " + gameObject.name);
        }
    }

    public static Vector3 Round(this Vector3 vector3)
    {
        vector3.x = Mathf.Round(vector3.x);
        vector3.y = Mathf.Round(vector3.y);
        vector3.z = Mathf.Round(vector3.z);

        return vector3;
    }
}