using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpSkinmeshRenderBlendShape), true)]
    public class BmLerpSkinmeshRenderBlendShapeEdtor : Editor
    {
        bool isPreview;

        private SerializedProperty tempProperty;
        private UnityEngine.Object tempObj = null;
        private int selectId = -1;

        private const int Dir_Left = 1;
        private const int Dir_Right = 2;
        private float percent;

        private bool isOrder;
        public override void OnInspectorGUI()
        {


            var group = target as BmLerpSkinmeshRenderBlendShape;

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



            if (isPreview)
            {
                percent = EditorGUILayout.Slider(percent, 0, 1);

                group.Lerp(percent);
            }

            base.OnInspectorGUI();


            GUI.backgroundColor = Color.green;
            GetDrag();

            GUI.backgroundColor = Color.white;
            Rect progressRect = GUILayoutUtility.GetRect(50, 20 * (group.groupNode.Count + 1));
            EditorGUI.DrawRect(progressRect, new Color(0.15f, 0.15f, 0.15f));


            float buttonLen = 40;
            progressRect.width -= buttonLen * 2;

            Rect groupRect = new Rect(progressRect);
            groupRect.height = 20;
            EditorGUI.ProgressBar(groupRect, group.percent, "Group");


            for (int i = 0; i < group.groupNode.Count; i++)
            {
                GUI.backgroundColor = Color.green;
                var node = group.groupNode[i];

                var pp = group.skinnedMesh.GetBlendShapeWeight(node.Id) * 0.01f;

                //绘制节点进度条
                Rect t1 = new Rect(progressRect);
                t1.y += 20 * (i + 1);
                t1.height = 20;
                t1.width = (node.maxInGroup - node.minInGroup) * progressRect.width;
                t1.x = progressRect.x + progressRect.width * node.minInGroup;

                EditorGUI.ProgressBar(t1, pp, node.Name.ToString());

                Rect line = new Rect(t1);
                line.width = progressRect.width;
                line.x = progressRect.x;
                line.height = 20;
                GUI.backgroundColor = i % 2 == 0 ? Color.white : Color.cyan;
                GUI.Box(line, "");

                //绘制节点进度条左值滑动帧
                Rect t2 = new Rect(t1);
                t2.width = 5;

                MouseCheck(node, t2, i, progressRect, Dir_Left);
                EditorGUI.DrawRect(t2, Color.green);

                //绘制节点进度条右值滑动帧
                t2.x = t1.xMin + t1.width;
                t2.width = 5;
                MouseCheck(node, t2, i, progressRect, Dir_Right);
                EditorGUI.DrawRect(t2, Color.red);

                //绘制节点功能键
                GUI.backgroundColor = Color.red;
                GUI.contentColor = Color.white;
                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2 + 5;

                if (GUI.Button(t1, "Edit"))
                {
                    BmLerpBlendShapeNodeWindow.Open(node);
                }

                t1.width = buttonLen;
                t1.x = progressRect.width + t1.width / 2 + 5 + buttonLen;
                if (GUI.Button(t1, "Del"))
                {
                    group.groupNode.Remove(node);
                }
            }

            //指针            
            progressRect.x += progressRect.width * group.percent;
            progressRect.width = 1;
            EditorGUI.DrawRect(progressRect, Color.white);

            //快速排列工具
            GUI.backgroundColor = Color.cyan;

            group.start_time = EditorGUILayout.Slider("起始位置", group.start_time, 0, 1);
            group.space_time = EditorGUILayout.Slider("间隔参数", group.space_time, -1, 1);
            group.node_len_time = EditorGUILayout.Slider("单个节点长度", group.node_len_time, -1, 1);
            isOrder = EditorGUILayout.Toggle("快速排列", isOrder);

            if (isOrder)
            {
                OrderNodes();
            }
        }

        bool isContain(BmLerpBlendShapeNode _lerp)
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            foreach (var item in group.groupNode)
            {
                if (item.Id == _lerp.Id)
                {
                    return true;
                }
            }
            return false;
        }

        void OrderNodes()
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            for (int i = 0; i < group.groupNode.Count; i++)
            {
                BmLerpBlendShapeNode aniNode = group.groupNode[i];
                aniNode.minInGroup = Mathf.Clamp01(group.start_time + group.space_time * i);
                aniNode.maxInGroup = Mathf.Clamp01(aniNode.minInGroup + group.node_len_time);                
            }
            Repaint();
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }

        #region Drag Frame
        Vector2 touchBegin;
        float startX;
        int _selectDir = 0;
        void MouseCheck(BmLerpBlendShapeNode node, Rect _rect, int _id, Rect _max, int _dir)
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
                    if (_id == selectId)
                    {
                        var t = aEvent.mousePosition - touchBegin;
                        float a = (startX + t.x) - _max.xMin;
                        if (_dir == Dir_Left)
                        {

                            node.minInGroup = a / _max.width;
                            node.minInGroup = Mathf.Clamp(node.minInGroup, 0, 1);

                        }
                        else if (_dir == Dir_Right)
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
            if(GUILayout.Button("添加"))
            {
                var group = target as BmLerpSkinmeshRenderBlendShape;                                
                BmLerpBlendShapeNodeWindow.Open(group.AddNode());
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
        }

        void UnPreview()
        {
            isPreview = false;
        }


        private void OnEnable()
        {
            var group = target as BmLerpSkinmeshRenderBlendShape;
            group.CheckNode();
            EditorUtility.SetDirty(group);
        }


       
    }
}

