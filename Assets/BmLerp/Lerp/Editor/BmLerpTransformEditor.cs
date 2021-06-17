using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{

    [CustomEditor(typeof(BmLerpTransform))]
    public class BmLerpTransformEditor : Editor
    {
        bool isPreview;

        [Range(0, 1.0f)]
        float previewP;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            var data = target as BmLerpTransform;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("记录到开始点"))
            {
                RercordData(false, data);
            }
            if (GUILayout.Button("记录到结尾点"))
            {
                RercordData(true, data);
            }
            if (GUILayout.Button("转换为本地坐标"))
            {
                ConvertLocalData();
            }
            GUILayout.EndHorizontal();

            isPreview = EditorGUILayout.Toggle("是否预览:",isPreview);
            if(isPreview)
            {
                previewP = EditorGUILayout.Slider(previewP, 0, 1);

            }
            if (isPreview)
            {
                data.Lerp(previewP);
            }
        }


        void RercordData(bool end, BmLerpTransform _data)
        {
            Vector3[] data = !end ? _data.beginData : _data.endData;

            switch (_data.type)
            {
                case BmLerpTransformType.Position:
                    RecordOne(ref data, _data.transform.position);
                    break;
                case BmLerpTransformType.PositionLocal:
                    Debug.Log(_data.transform.localPosition);
                    RecordOne(ref data, _data.transform.localPosition);
                    break;
                case BmLerpTransformType.Rotation:
                    RecordOne(ref data, _data.transform.eulerAngles);
                    break;
                case BmLerpTransformType.RotationLocal:
                    RecordOne(ref data, _data.transform.localEulerAngles);
                    break;
                case BmLerpTransformType.Scale:
                    RecordOne(ref data, _data.transform.localScale);
                    break;
                case BmLerpTransformType.TransAll:
                    RecordAll(ref data, _data.transform, false);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    RecordAll(ref data, _data.transform, false);
                    break;
            }
            if (end)
            {
                _data.endData = data;
                return;
            }
            _data.beginData = data;
        }

        void RecordAll(ref Vector3[] data, Transform _trans, bool isLocal)
        {
            if (data == null || data.Length < 3)
            {
                data = new Vector3[3];
            }

            data[0] = isLocal ? _trans.localPosition : _trans.position;
            data[1] = isLocal ? _trans.localEulerAngles : _trans.eulerAngles;
            data[2] = _trans.localScale;
        }

        void RecordOne(ref Vector3[] data, Vector3 _in)
        {
            if (data == null || data.Length < 1)
            {
                data = new Vector3[1];
            }
            data[0] = _in;
        }

        void ConvertLocalData()
        {
            _ConvertLocalData(true, target as BmLerpTransform);
            _ConvertLocalData(false, target as BmLerpTransform);
        }

        void _ConvertLocalData(bool end, BmLerpTransform _data)
        {
            Vector3[] data = !end ? _data.beginData : _data.endData;
            var parent = _data.transform.parent;
            if (parent == null)
            {
                return;
            }
            switch (_data.type)
            {
                case BmLerpTransformType.Position:
                    data[0] = parent.InverseTransformPoint(data[0]);
                    break;
                case BmLerpTransformType.PositionLocal:
                    break;
                case BmLerpTransformType.Rotation:
                    {
                        Quaternion q = _data.transform.WorldToLocalRotationInParent(Quaternion.Euler(data[0]));
                        data[0] = q.eulerAngles;
                    }
                    break;
                case BmLerpTransformType.RotationLocal:
                    break;
                case BmLerpTransformType.Scale:
                    
                    break;
                case BmLerpTransformType.TransAll:
                    {
                        data[0] = parent.InverseTransformPoint(data[0]);

                        Quaternion q = _data.transform.WorldToLocalRotationInParent(Quaternion.Euler(data[1]));
                        data[1] = q.eulerAngles;
                    }
                    break;
                case BmLerpTransformType.TransAllLocal:
                    break;
            }
        }

    }
}
