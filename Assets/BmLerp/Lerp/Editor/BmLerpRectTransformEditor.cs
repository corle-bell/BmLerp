using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bm.Lerp
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BmLerpRectTransform))]
    public class BmLerpRectTransformEditor : Editor
    {
        bool isPreview;

        [Range(0, 1.0f)]
        float previewP;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            var data = target as BmLerpRectTransform;
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
            if (GUILayout.Button("交换起始数据"))
            {
                SwapData(data);
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


        void RercordData(bool end, BmLerpRectTransform _data)
        {
            switch (_data.type)
            {
                case BmLerpTransformType.AnchoredPosition:
                    RecordOne(ref _data.moveData, (_data.transform as RectTransform).anchoredPosition, end);
                    break;
                case BmLerpTransformType.Rotation:
                    RecordOne(ref _data.rotationData, _data.transform.rotation, end);
                    break;
                case BmLerpTransformType.RotationLocal:
                    RecordOne(ref _data.rotationData, _data.transform.localRotation, end);
                    break;
                case BmLerpTransformType.Scale:
                    RecordOne(ref _data.scaleData, _data.transform.localScale, end);
                    break;
                case BmLerpTransformType.TransAll:
                    RecordAll(_data, _data.transform, false, end);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    RecordAll(_data, _data.transform, true, end);
                    break;
            }
        }

        void RecordAll(BmLerpRectTransform _data, Transform _trans, bool isLocal, bool _isEnd)
        {
            RecordOne(ref _data.moveData, (_data.transform as RectTransform).anchoredPosition, _isEnd);
            RecordOne(ref _data.rotationData, isLocal ? _data.transform.localRotation :_data.transform.rotation, _isEnd);
            RecordOne(ref _data.scaleData, _data.transform.localScale, _isEnd);
        }


        void RecordOne<T>(ref T[] data, T _in, bool _isEnd)
        {
            if (data == null || data.Length!=2)
            {
                data = new T[2];
            }
            data[_isEnd ? 1 : 0] = _in;
        }

        void SwapData(BmLerpRectTransform _data)
        {
            BmTools.Swap(ref _data.moveData[0], ref _data.moveData[1]);
            BmTools.Swap(ref _data.scaleData[0], ref _data.scaleData[1]);
            BmTools.Swap(ref _data.rotationData[0], ref _data.rotationData[1]);
        }

        void ConvertLocalData()
        {
            _ConvertLocalData(true, target as BmLerpRectTransform);
            _ConvertLocalData(false, target as BmLerpRectTransform);
        }

        void _ConvertLocalData(bool end, BmLerpRectTransform _data)
        {
            var parent = _data.transform.parent;
            if (parent == null)
            {
                return;
            }
            int id = end ? 1 : 0;
            switch (_data.type)
            {
                case BmLerpTransformType.Rotation:
                    {
                        _data.rotationData[id] = _data.transform.WorldToLocalRotationInParent(_data.rotationData[id]);
                    }
                    break;
                case BmLerpTransformType.RotationLocal:
                    break;
                case BmLerpTransformType.Scale:
                    
                    break;
                case BmLerpTransformType.TransAll:
                    {
                        _data.moveData[id] = parent.InverseTransformPoint(_data.moveData[id]);
                        _data.rotationData[id] = _data.transform.WorldToLocalRotationInParent(_data.rotationData[id]);
                    }
                    break;
                case BmLerpTransformType.TransAllLocal:
                    break;
            }
        }

    }
}
