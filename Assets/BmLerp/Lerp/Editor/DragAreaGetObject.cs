using UnityEngine;
using System.Collections;
using UnityEditor;

public class DragAreaGetObject : Editor
{

    public static UnityEngine.Object GetOjbect(string meg = null)
    {
        Event aEvent;
        aEvent = Event.current;

        GUI.contentColor = Color.white;
        UnityEngine.Object temp = null;

        var dragArea = GUILayoutUtility.GetRect(0f, 35f, GUILayout.ExpandWidth(true));

        GUIContent title = new GUIContent(meg);
        if (string.IsNullOrEmpty(meg))
        {
            title = new GUIContent("Drag Object here from Project view to get the object");
        }

        GUI.Box(dragArea, title);
        switch (aEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dragArea.Contains(aEvent.mousePosition))
                {
                    break;
                }

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (aEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    for (int i = 0; i < DragAndDrop.objectReferences.Length; ++i)
                    {
                        temp = DragAndDrop.objectReferences[i];

                        if (temp == null)
                        {
                            break;
                        }
                    }
                }

                Event.current.Use();
                break;
            default:
                break;
        }

        return temp;
    }
}