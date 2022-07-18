using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    public class BmLerpBlendShapeNodeWindow : EditorWindow
    {
        public BmLerpBlendShapeNode node;
        public SkinnedMeshRenderer renderer;
        public float _left;
        public float _right;

        public string[] blendShapeName;
        public void Init()
        {
            _left = node.curve.Evaluate(0);
            _right = node.curve.Evaluate(1);
        }
        private void OnGUI()
        {
            node.Id = EditorGUILayout.Popup("ID:", node.Id, blendShapeName);
         
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

        public static void Open(BmLerpBlendShapeNode _node, string[] _blendShapeName)
        {
            BmLerpBlendShapeNodeWindow window = EditorWindow.GetWindow(typeof(BmLerpBlendShapeNodeWindow)) as BmLerpBlendShapeNodeWindow;
            window.node = _node;
            window.blendShapeName = _blendShapeName;
            window.Init();
        }
    }
}

