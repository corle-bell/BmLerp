//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2024-01-13 09:42:41
//
//----Desc:         Create By BM
//
//**************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Bm.Lerp;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    public class BmLerpComponentSelectWindow : EditorWindow
    {

        public static BmLerpComponentSelectWindow Open(BmLerpGroup target, BmLerpBase[] component, BmLerpGroupEditor GroupEditor)
        {
            var myWindow = EditorWindow.GetWindow(typeof(BmLerpComponentSelectWindow), false, "BmLerpComponentSelectWindow", true) as BmLerpComponentSelectWindow;
            myWindow.Init(target, component, GroupEditor);
            myWindow.Show();
            return myWindow;
        }

        protected BmLerpBase[] componentlist;
        protected Texture2D[] iconList;
        protected string[] componentDesc;
        protected int selectIndex = -1;
        public BmLerpGroup root;
        public string fieldName;
        private Material m_material;
        private BmLerpGroupEditor GroupEditor;
        public void Init(BmLerpGroup target, BmLerpBase[] component, BmLerpGroupEditor groupEditor)
        {
            GroupEditor = groupEditor;
            root = target;
            UpdateParams(component);
            
            m_material = new Material(Shader.Find("Hidden/Internal-Colored"));
            m_material.hideFlags = HideFlags.HideAndDontSave;
        }
    

        private void OnGUI()
        {
            float label_width = position.width*0.7f;
            float height = 30f;
            float spacex = 5f;
            float spacey = 5f;
            float childPadingLeft = position.width*0.2f;
            Rect rect = new Rect(0, 0, this.position.width, height);
            string titleString = "";
            float top = 0;
            for(int i=0; i<componentlist.Length; i++)
            {
                //绘制标题
                Rect icoRect = new Rect(rect);
                if (!titleString.Equals(componentDesc[i]))
                {
                    titleString = componentDesc[i];
                    
                    rect.y += height+spacey;
                    
                    
                    icoRect.width = rect.width;
                    icoRect.x = 0;
                    icoRect.height = height;
                    GUI.Label(icoRect, titleString, GUI.skin.customStyles[53]);

                    top = rect.y-height;
                    
                    icoRect.y += height + spacey;
                }

                //绘制折线
                Rect lineRect = new Rect(childPadingLeft*0.3f, height*0.5f, childPadingLeft*0.7f, position.height);
                GUI.BeginGroup(lineRect);
                
                GL.PushMatrix();
                m_material.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Color(new Color(1f, 1f, 1f));
                
                DrawLine(new Vector2(0, top), new Vector2(0, icoRect.y));
                DrawLine(new Vector2(0, icoRect.y), new Vector2(childPadingLeft*0.7f, icoRect.y));
                
                GL.End();
                GL.PopMatrix();
                
                
                
                GUI.EndGroup();
                
              
                
                
                //绘制图片
                icoRect.x += childPadingLeft;
                
                icoRect.x += spacex;
                icoRect.width = icoRect.height = rect.height;
                GUI.DrawTexture(icoRect, iconList[i]);
                
                //绘制按钮
                icoRect.x += icoRect.height + spacex;
                icoRect.width = label_width;
                
                if(i==selectIndex)
                {
                    GUI.Label(icoRect, componentlist[i].GetType().ToString(), GUI.skin.customStyles[42]);
                }
                else
                {
                    if (GUI.Button(icoRect, componentlist[i].GetType().ToString()))
                    {
                        selectIndex = i;
                        SetComponent(componentlist[selectIndex]);
                        this.Close();
                    }
                }
                
                rect.y += height+spacey;
            }
            
            
            if (GUI.Button(rect, "All"))
            {
                SetComponent(null);
                this.Close();
            }
        }

        private void DrawLine(Vector2 p0, Vector2 p1)
        {
            GL.Vertex(p0);
            GL.Vertex(p1);
        }

      
        protected void SetComponent(BmLerpBase _Component)
        {
            if (_Component==null)
            {
                foreach (var c in componentlist)
                {
                    GroupEditor.SafeAddNode(root, c);
                }
            }
            else
            {
                GroupEditor.SafeAddNode(root, _Component);
            }
            EditorUtility.SetDirty(root);
        }

        protected void UpdateParams(BmLerpBase[] component)
        {
            if (componentlist == null || componentlist.Length == 0)
            {
                componentlist = component;

                Texture2D defaultTex = EditorGUIUtility.FindTexture("d_cs script Icon");
                componentDesc = new string[componentlist.Length];
                iconList = new Texture2D[componentDesc.Length];
                for (int i = 0; i < componentDesc.Length; i++)
                {
                    componentDesc[i] = FormatDesc(componentlist[i], componentlist[i].name, false, false);
                    iconList[i] = defaultTex;
                }
            }
        }
        
        string FormatDesc(Component component, string _root, bool includeChildren, bool includeType=true)
        {
            var fullName = component.transform.HierarchyName().Split(new char[] { '/' });
            int index = Array.IndexOf(fullName, _root);
            string name = "";
            for (int i = index; i < fullName.Length; i++)
            {
                name += fullName[i];
                name += i < fullName.Length - 1 ? "->" : "";
            }

            if (includeType)
            {
                return includeChildren ? $"{name}/{component.GetType()}" : $"{component.GetType()}";
            }
            else
            {
                return $"{name}";
            }
        }
    }

}

