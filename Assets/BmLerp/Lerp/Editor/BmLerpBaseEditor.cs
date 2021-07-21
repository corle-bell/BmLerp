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

        protected bool isPreview;
        [Range(0, 1.0f)]
        protected float percent;
        public override void OnInspectorGUI()
        {
            var data = target as BmLerpBase;
            base.OnInspectorGUI();

            isPreview = EditorGUILayout.Toggle("是否预览:", isPreview);
            if (isPreview)
            {
                percent = EditorGUILayout.Slider(percent, 0, 1);
                data.Lerp(percent);

                EditorUtility.SetDirty(data);
            }
        }
    }

}
