using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroundTracker))]
public class GroundTrackerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GroundTracker gt = (GroundTracker) target;
        if (GUILayout.Button("Snap Ground Cubes to Grid")) {
            gt.snapAllOnjects();
        }
    }
}
