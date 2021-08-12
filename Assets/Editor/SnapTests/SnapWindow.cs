using UnityEditor;
using UnityEngine;

public class SnapWindow : EditorWindow
{
    public float x = 1, y = 1, z = 1;

    private bool drawgrid = false;

    private bool radial;

    private SerializedObject so;

    private SerializedProperty propX, propY, propZ;
 private SerializedProperty propDrawGrid;
    // private float drawX, drawY, drawZ;

    [MenuItem("Test/SnapWindow")]
    public static void OpenWindows()
    {
        GetWindow<SnapWindow>();
    }

    private void OnEnable()
    {
        so = new SerializedObject(this);
        propX = so.FindProperty(nameof(x));
        propY = so.FindProperty(nameof(y));
        propZ = so.FindProperty(nameof(z));
        propDrawGrid = so.FindProperty(nameof(drawgrid));

        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DrawGrid;
    }

    private void DrawGrid(SceneView sceneView)
    {
        if (!drawgrid)
        {
            return;
        }

        for (int i = -5; i < 5; i++)
        {
            float xpos = i * x;
            float zpos = i * z;
            float ypos = i * y;
            for (int j = -5; j < 5; j++)
            {
                ypos = j * y;

                for (int k = -5; k < 5; k++)
                {
                    zpos = k * z;
                    Handles.color = Color.blue;
                    Handles.DrawWireCube(new Vector3(xpos, ypos, zpos), Vector3.one * 0.2f);
                }
            }
        }

        DrawSnapPositions();
    }

    private void DrawSnapPositions()
    {
        foreach (GameObject o in Selection.gameObjects)
        {
            Vector3 opos = o.transform.position;
            Vector3 snapPosition = opos.Round(x, y, z);
            Handles.color = Color.yellow;
            Handles.DrawAAPolyLine(Texture2D.whiteTexture,4, opos, snapPosition);
        }
    }


    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DrawGrid;
    }

    private void OnGUI()
    {
        so.Update();            
        GUILayout.Label("Grid Dimensions");
        EditorGUILayout.PropertyField(propX);
        EditorGUILayout.PropertyField(propY);
        EditorGUILayout.PropertyField(propZ);
        // EditorGUILayout.PropertyField(propDrawGrid);
        so.ApplyModifiedProperties();

        drawgrid = EditorGUILayout.Toggle("DrawGrid", drawgrid);
        // radial = EditorGUILayout.Toggle("Radial", radial);
        //
        //     x = GridScaleSlider("x", x);
        //     y = GridScaleSlider("y", y);
        //     z = GridScaleSlider("z", z);
        //
        // using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0))
        // {
        //     if (GUILayout.Button("Snap"))
        //     {
        //             Snapper.SnapSelection(x, y, z);
        //     }
        // }
    }


    private float GridScaleSlider(string name, float inputVariable)
    {
        return EditorGUILayout.Slider(name, inputVariable, 0, 10);
    }
}