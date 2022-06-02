using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpSpline))]
    public class BmLerpSplineEditor : BmLerpBaseEditor
    {
        bool isEdit;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            isEdit = EditorGUILayout.Toggle("编辑", isEdit);

            var script = target as BmLerpSpline;
            if (GUILayout.Button("添加"))
            {
                script.points.Add(Vector3.zero);
                int id = script.points.Count - 1;
                script.points[id - 2] = script.points[id - 1];
            }

            for(int i=1; i< script.points.Count-1; i++)
            {
                EditorGUILayout.BeginHorizontal();
                script.points[i] = EditorGUILayout.Vector3Field(string.Format("Point_{0}", i - 1), script.points[i]);
                if (GUILayout.Button("移除"))
                {
                    script.points.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            if(GUI.changed)
            {
                FixPoints();
            }
        }


        private void OnSceneGUI()
        {
            var script = target as BmLerpSpline;

            Handles.color = Color.red;

            

            if (!isEdit) return;

            Handles.BeginGUI();
            for(int i=1; i<script.points.Count-1; i++)
            {
                script.transform.Handles_Label(script.points[i] + new Vector3(0, 1, 0), "Point_" + (i - 1), GUI.skin.customStyles[564]);                
                script.points[i] = script.transform.Handles_Position(script.points[i], Quaternion.identity);
            }
            Handles.EndGUI();

            FixPoints();
        }

        private void FixPoints()
        {
            var script = target as BmLerpSpline;
            int id = 0;
            //script.points[id] = script.points[id+1];

            id = script.points.Count - 1;
            script.points[id] = script.points[id-1];
        }
    }
}