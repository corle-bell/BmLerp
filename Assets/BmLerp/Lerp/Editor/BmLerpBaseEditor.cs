using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpBase), true)]
    public class BmLerpBaseEditor : Editor
    {
       
        bool isPreview;
        [Range(0, 1.0f)]
        float previewP;
        public override void OnInspectorGUI()
        {
            var data = target as BmLerpBase;
            base.OnInspectorGUI();

            isPreview = EditorGUILayout.Toggle("是否预览:", isPreview);
            if (isPreview)
            {
                previewP = EditorGUILayout.Slider(previewP, 0, 1);
                data.Lerp(previewP);

                EditorUtility.SetDirty(data);
            }
        }
    }

}
