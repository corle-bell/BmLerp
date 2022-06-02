using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    public class BmLerpGroupNodeWindow : EditorWindow
    {
        public BmLerpGroupNode node;
       

        public float _left;
        public float _right;
        public void Init()
        {
            _left = node.curve.Evaluate(0);
            _right = node.curve.Evaluate(1);
        }
        private void OnGUI()
        {
            EditorGUILayout.LabelField(node.lerp.ToString());

            EditorGUILayout.BeginHorizontal();
            node.minInGroup = EditorGUILayout.FloatField("区间左值", node.minInGroup);
            node.maxInGroup = EditorGUILayout.FloatField("区间右值", node.maxInGroup);
            EditorGUILayout.EndHorizontal();

            node.curve = EditorGUILayout.CurveField(node.curve, GUILayout.MinWidth(80), GUILayout.MinHeight(80));

            _left = EditorGUILayout.Slider("Left", _left, 0, 1);
            _right = EditorGUILayout.Slider("Right", _right, 0, 1);

            if (GUILayout.Button("保存为线性曲线"))
            {
                node.curve = AnimationCurve.Linear(0, _left, 1, _right);
            }

            if (GUILayout.Button("保存"))
            {
                this.Close();
            }
        }

        public static void Open(BmLerpGroupNode _node)
        {
            BmLerpGroupNodeWindow window = EditorWindow.GetWindow(typeof(BmLerpGroupNodeWindow)) as BmLerpGroupNodeWindow;
            window.node = _node;
            window.Init();
        }
    }
}

