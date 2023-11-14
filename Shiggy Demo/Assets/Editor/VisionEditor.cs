
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VisionEditor))]
public class VisionEditor : Editor
{
    private void OnSceneGUI()
    {
        VisionScript fov = (VisionScript)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
    }
}
