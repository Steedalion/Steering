using UnityEditor;
using UnityEngine;


public static class Snapper
{
    const string UNDO_SNAPP = "Snapped objects";

    [MenuItem("Test/Snap %#q", isValidateFunction: true)]
    public static bool SnapSelectable()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("Test/Snap %#q")]
    public static void SnapSelection()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, UNDO_SNAPP);
            gameObject.transform.position = gameObject.transform.position.Round();
        }
    }

     public static void SnapSelection(float x, float y, float z)
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, UNDO_SNAPP);
            gameObject.transform.position = gameObject.transform.position.Round(x,y,z);
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
        return vector3.Round(1,1,1);
    }
    
      public static Vector3 Round(this Vector3 vector3,float x, float y, float z)
    {
        vector3.x = Round(vector3.x,x);
        vector3.y = Round(vector3.y,y);
        vector3.z = Round(vector3.z,z);
        return vector3;
    }
      public static Vector3 RadialRound(this Vector3 vector3,float radius, int segments)
      {
          vector3 = vector3.normalized * radius;
        return vector3;
    }

    public static float Round(float value, float increment)
    {
        if (increment ==0)
        {
            return 0;
        }
        float wholes = Mathf.Round(value / increment);
        return wholes * increment;
    }
}