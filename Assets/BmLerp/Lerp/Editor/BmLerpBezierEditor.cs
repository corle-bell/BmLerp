using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpBezier))]
    public class BmLerpBezierEditor : BmLerpBaseEditor
    {
        bool isEdit;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            isEdit = EditorGUILayout.Toggle("编辑", isEdit);
        }


        private void OnSceneGUI()
        {
            var script = target as BmLerpBezier;

            Handles.color = Color.red;
            Vector3 _start = script.transform.TransformPoint(script.start);
            Vector3 _control = script.transform.TransformPoint(script.control);
            Vector3 _end = script.transform.TransformPoint(script.end);

            Handles.DrawLine(_start, script.CalcPoint(_start, _control, _end, 0.01f));
            for (int i=1; i<100; i++)
            {
                float t = i;
                var pos = script.CalcPoint(_start, _control, _end, (t-1) /100);
                var pos1 = script.CalcPoint(_start, _control, _end, t/ 100);
                Handles.DrawLine(pos, pos1);
            }

            if (!isEdit) return;

            Handles.BeginGUI();
            Handles.Label(_start + new Vector3(0, 0.15f, 0), "Start", GUI.skin.customStyles[564]);
            script.start = HandleLocal(_start);

            Handles.Label(_control + new Vector3(0, 0.15f, 0), "Control", GUI.skin.customStyles[564]);
            script.control = HandleLocal(_control);

            Handles.Label(_end + new Vector3(0, 0.15f, 0), "End", GUI.skin.customStyles[564]);
            script.end = HandleLocal(_end);
            Handles.EndGUI();
        }

        private Vector3 HandleLocal(Vector3 _src)
        {
            Transform p = (target as BmLerpBezier).transform;
            return p.InverseTransformPoint(Handles.PositionHandle(_src, Quaternion.identity));
        }
    }
}