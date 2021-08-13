using System;
using UnityEditor;
using UnityEngine;

public class SnapWindow : EditorWindow
{
    public float x = 1, y = 1, z = 1;
    public float radius = 1;
    public int radialSegments;
    private bool drawGrid = false;
    private bool radial => drawRadialGrid;
    private SerializedObject so;
    private SerializedProperty propX, propY, propZ, propR, propS;
    private SerializedProperty propDrawGrid;
    private bool drawRadialGrid;
    float gridSize = 0.2f;


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
        propR = so.FindProperty(nameof(radius));
        propS = so.FindProperty(nameof(radialSegments));
        propDrawGrid = so.FindProperty(nameof(drawGrid));


        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DrawGrid;
        SceneView.duringSceneGui += DrawRadialGrid;
    }


    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DrawGrid;
        SceneView.duringSceneGui -= DrawRadialGrid;
    }

    private void DrawRadialGrid(SceneView obj)
    {
        if (!drawRadialGrid)
        {
            return;
        }

        Handles.DrawWireCube(new Vector3(0, 0, 0), Vector3.one * gridSize);
        float segmentSize =  360 / radialSegments;
        for (int i = 0; i < 5; i++)
        {
            float r = i * radius;
            for (int j = 0; j < radialSegments; j++)
            {
                
                Vector3 point = Quaternion.Euler(0, segmentSize * j, 0)* Vector3.right* r;
                Handles.DrawWireCube(point, Vector3.one * gridSize);
            }
        }
    }

    private void DrawGrid(SceneView sceneView)
    {
        if (!drawGrid)
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
                    Handles.DrawWireCube(new Vector3(xpos, ypos, zpos), Vector3.one * gridSize);
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
            Handles.DrawAAPolyLine(Texture2D.whiteTexture, 4, opos, snapPosition);
        }
    }


    private void OnGUI()
    {
        so.Update();
        GUILayout.Label("Grid Dimensions");
        EditorGUILayout.PropertyField(propX);
        EditorGUILayout.PropertyField(propY);
        EditorGUILayout.PropertyField(propZ);
        EditorGUILayout.PropertyField(propR);
        EditorGUILayout.PropertyField(propS);
        // EditorGUILayout.PropertyField(propDrawGrid);

        drawGrid = EditorGUILayout.Toggle("DrawGrid", drawGrid);
        drawRadialGrid = EditorGUILayout.Toggle("DrawRadialGrid", drawRadialGrid);
        // radial = EditorGUILayout.Toggle("Radial", radial);
        //
        //     x = GridScaleSlider("x", x);
        //     y = GridScaleSlider("y", y);
        //     z = GridScaleSlider("z", z);
        //
        using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button("Snap"))
            {
                if (!radial)
                {
                    Snapper.SnapSelection(x, y, z);
                }
                else
                {
                    Snapper.SnapSelectionRadially(radius, radialSegments);
                }
            }
        }

        so.ApplyModifiedProperties();
    }


    private float GridScaleSlider(string name, float inputVariable)
    {
        return EditorGUILayout.Slider(name, inputVariable, 0, 10);
    }
}