using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{

    [CustomEditor(typeof(BmLerpGroup), true)]
    public class BmLerpGroupEditor : Editor
    {
        bool isPreview;

        private SerializedProperty tempProperty;
        private UnityEngine.Object tempObj = null;
        private int selectId = -1;

        private const int Dir_Left = 1;
        private const int Dir_Right = 2;
        private float percent;
        public override void OnInspectorGUI()
        {
           

            var group = target as BmLerpGroup;

            EditorGUILayout.BeginHorizontal();
            if (isPreview)
            {
                if (GUILayout.Button("关闭预览"))
                {
                    UnPreview(); 
                }
            }
            else
            {
                if (GUILayout.Button("预览"))
                {
                    Preview();
                }
            }
            EditorGUILayout.EndHorizontal();



            if(isPreview)
            {
                percent = EditorGUILayout.Slider(percent, 0, 1);

                group.Lerp(percent);
            }

            base.OnInspectorGUI();


            GUI.backgroundColor = Color.green;
            GetDrag();

            GUI.backgroundColor = Color.white;
            Rect progressRect = GUILayoutUtility.GetRect(50, 20*(group.groupNode.Count + 1));
            EditorGUI.DrawRect(progressRect, new Color(0.15f, 0.15f, 0.15f));


            float buttonLen = 40;
            progressRect.width -= buttonLen;

            Rect groupRect = new Rect(progressRect);
            groupRect.height = 20;
            EditorGUI.ProgressBar(groupRect, group.percent, "Group");

            
            for (int i=0; i<group.groupNode.Count; i++)
            {
                GUI.backgroundColor = Color.green;
                var node = group.groupNode[i];

                //绘制节点进度条
                Rect t1 = new Rect(progressRect);
                t1.y += 20 * (i + 1);
                t1.height = 20;
                t1.width = (node.maxInGroup - node.minInGroup) * progressRect.width;
                t1.x = progressRect.x+progressRect.width * node.minInGroup;
                EditorGUI.ProgressBar(t1, node.percent, node.name);

                //绘制节点进度条左值滑动帧
                Rect t2 = new Rect(t1);
                t2.width = 5;
                
                MouseCheck(node, t2, i, progressRect, Dir_Left);
                EditorGUI.DrawRect(t2, Color.green);

                //绘制节点进度条右值滑动帧
                t2.x = t1.xMin+t1.width;
                t2.width = 5;
                MouseCheck(node, t2, i, progressRect, Dir_Right);
                EditorGUI.DrawRect(t2, Color.red);

                //绘制节点功能键
                GUI.backgroundColor = Color.red;
                GUI.contentColor = Color.white;
                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2;

                if(GUI.Button(t1, "Del"))
                {
                    group.groupNode.Remove(node);
                }
            }

            

        }

        #region Drag Frame
        Vector2 touchBegin;
        float startX;
        int _selectDir = 0;
        void MouseCheck(BmLerpBase node, Rect _rect, int _id, Rect _max, int _dir)
        {
            Event aEvent;
            aEvent = Event.current;
            switch (aEvent.type)
            {
                case EventType.MouseDown:
                    if (_rect.Contains(aEvent.mousePosition))
                    {
                        selectId = _id;
                        touchBegin = aEvent.mousePosition;
                        startX = _rect.xMin;
                        _selectDir = _dir;
                    }
                    break;
                case EventType.MouseUp:
                    selectId = -1;
                    _selectDir = 0;
                    break;
                case EventType.MouseDrag:
                    if (!_max.Contains(aEvent.mousePosition) || _selectDir != _dir)
                    {
                        return;
                    }
                    if (_id==selectId)
                    {
                        var t = aEvent.mousePosition - touchBegin;
                        float a = (startX + t.x) - _max.xMin;
                        if (_dir == Dir_Left)
                        {
                            
                            node.minInGroup = a / _max.width;
                            node.minInGroup = Mathf.Clamp(node.minInGroup, 0, 1);

                        }
                        else if(_dir == Dir_Right)
                        {
                            node.maxInGroup = (startX + t.x) / _max.width;
                            node.maxInGroup = Mathf.Clamp(node.maxInGroup, 0, 1);

                        }


                        Repaint();
                    }
                    
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Drag To Add
        void GetDrag()
        {
            tempObj = DragAreaGetObject.GetOjbect("Add Object In Group By Drag On Here");

            if (tempObj != null)
            {
                Debug.Log(tempObj.name);

                GameObject t = tempObj as GameObject;
                BmLerpBase [] arr = t.GetComponents<BmLerpBase>();
                if(arr != null)
                {
                    var group = target as BmLerpGroup;
                    foreach(var script in arr)
                    {
                        if (!group.groupNode.Contains(script))
                        {
                            group.groupNode.Add(script);
                        }
                    }
                }
                else
                {
                    Debug.LogError("无效物体请检查!");
                }
            }

            GUILayout.Space(10);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
        #endregion;

        void Preview()
        {
            isPreview = true;

            var group = target as BmLerpGroup;
            for (int i = 0; i < group.groupNode.Count; i++)
            {
                BmLerpAnimator aniNode = group.groupNode[i] as BmLerpAnimator;
                if(aniNode)
                {
                    aniNode.BakeAnimation();
                }
            }
        }

        void UnPreview()
        {
            isPreview = false;
        }


        private void OnEnable()
        {
            var group = target as BmLerpGroup;
            group.CheckNode();

            m_PreviousTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += inspectorUpdate;
        }


        private double delta;
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
                var group = target as BmLerpGroup;
                for (int i = 0; i < group.groupNode.Count; i++)
                {
                    BmLerpAnimator aniNode = group.groupNode[i] as BmLerpAnimator;
                    if (aniNode)
                    {
                        aniNode.animator.Update((float)delta);
                    }
                }
                
            }
        }
    }
}

