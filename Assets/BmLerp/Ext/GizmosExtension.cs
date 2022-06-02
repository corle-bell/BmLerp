using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosExtension
{
#if UNITY_EDITOR
    public static void DrawLineLocal(this Transform _script, Vector3 p0, Vector3 p1)
    {
        Gizmos.DrawLine(_script.TransformPoint(p0), _script.TransformPoint(p1));
    }
    public static void DrawSphere(this Transform _script, Vector3 p0, float _radius)
    {
        Gizmos.DrawSphere(_script.TransformPoint(p0), _radius);
    }

    public static void DrawWireSphere(this Transform _script, Vector3 p0, float _radius)
    {
        Gizmos.DrawWireSphere(_script.TransformPoint(p0), _radius);
    }

    public static Vector3 Handles_Position(this Transform _script, Vector3 p0, Quaternion quaternion)
    {
        return _script.InverseTransformPoint(UnityEditor.Handles.PositionHandle(_script.TransformPoint(p0), quaternion));
    }
    public static void Handles_Label(this Transform _script, Vector3 p0, string text, GUIStyle style)
    {
        UnityEditor.Handles.Label(_script.TransformPoint(p0), text, style);
    }

    public static void Handles_Label(this Transform _script, Vector3 p0, string text)
    {
        UnityEditor.Handles.Label(_script.TransformPoint(p0), text);
    }
#endif
}
