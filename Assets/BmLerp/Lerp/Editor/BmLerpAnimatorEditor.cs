using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpAnimator))]
    public class BmLerpAnimatorEditor : Editor
    {
        private string[] stateNames;
        private int selectIndex;

        bool isPreview;
        bool isBake = false;
        [Range(0, 1.0f)]
        float previewP;
        public override void OnInspectorGUI()
        {
            int a = selectIndex;
            selectIndex = EditorGUILayout.Popup("动画列表", selectIndex, stateNames);
            
            base.OnInspectorGUI();

            var data = target as BmLerpAnimator;
            data.state = stateNames[selectIndex];

            if (GUILayout.Button("Bake"))
            {
                data.BakeAnimation();
            }

            isPreview = EditorGUILayout.Toggle("是否预览:", isPreview);
            if (isPreview)
            {
                previewP = EditorGUILayout.Slider(previewP, 0, 1);
                data.Lerp(previewP);
            }
        }


        private void OnEnable()
        {
            var controller = target as BmLerpAnimator;
            AnimatorController animatorController = controller.animator.runtimeAnimatorController as AnimatorController;
            AnimatorStateMachine stateMachine = animatorController.layers[0].stateMachine;
            stateNames = new string[stateMachine.states.Length];
            for (int i = 0; i < stateMachine.states.Length; i++)
            {
                stateNames[i] = stateMachine.states[i].state.name;
                if(controller.state==stateNames[i])
                {
                    selectIndex = i;
                }
            }

            m_PreviousTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += inspectorUpdate;
        }


        private double delta;
        private float m_RunningTime;
        private double m_PreviousTime;
        private void OnDisable()
        {
            
        }

        void OnDestroy()
        {
            EditorApplication.update -= inspectorUpdate;
        }

        private void inspectorUpdate()
        {
            delta = EditorApplication.timeSinceStartup - m_PreviousTime;
            m_PreviousTime = EditorApplication.timeSinceStartup;

            if (!Application.isPlaying && isPreview)
            {
                m_RunningTime = m_RunningTime + (float)delta;
                var controller = target as BmLerpAnimator;
                controller.animator.Update((float)delta);
            }
        }
    }
}
