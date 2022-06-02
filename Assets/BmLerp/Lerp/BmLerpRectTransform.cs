using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public enum BmLerpTransformType
    {
        Position,
        PositionLocal,        
        Rotation,
        RotationLocal,
        Scale,
        TransAll,
        TransAllLocal,
        AnchoredPosition,
    }

    public class BmLerpRectTransform : BmLerpBase
    {
        public BmLerpTransformType type;
        //[HideInInspector]
        public Vector2 [] moveData;
        //[HideInInspector]
        public Vector3 [] scaleData;
        //[HideInInspector]
        public Quaternion[] rotationData;


        protected override void _Lerp(float _per)
        {
            if(isValueCurve)
            {
                _LerpCurve(_per);
            }
            else
            {
                _LerpNoCurve(_per);
            }
        }

        protected void _LerpCurve(float _per)
        {
            var rectTransform = transform as RectTransform;
            switch (this.type)
            {
                case BmLerpTransformType.AnchoredPosition:
                    rectTransform.anchoredPosition = Vector2.LerpUnclamped(moveData[0], moveData[1], _per);
                    break;
                case BmLerpTransformType.Rotation:
                    transform.rotation = Quaternion.LerpUnclamped(rotationData[0], rotationData[1], _per);
                    break;
                case BmLerpTransformType.RotationLocal:
                    transform.localRotation = Quaternion.LerpUnclamped(rotationData[0], rotationData[1], _per);
                    break;
                case BmLerpTransformType.Scale:
                    transform.localScale = Vector3.LerpUnclamped(scaleData[0], scaleData[1], _per);
                    break;
                case BmLerpTransformType.TransAll:
                    rectTransform.anchoredPosition = Vector2.LerpUnclamped(moveData[0], moveData[1], _per);
                    transform.rotation = Quaternion.LerpUnclamped(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.LerpUnclamped(scaleData[0], scaleData[1], _per);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    rectTransform.anchoredPosition = Vector2.LerpUnclamped(moveData[0], moveData[1], _per);
                    transform.localRotation = Quaternion.LerpUnclamped(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.LerpUnclamped(scaleData[0], scaleData[1], _per);
                    break;
            }
        }


        protected void _LerpNoCurve(float _per)
        {
            var rectTransform = transform as RectTransform;
            switch (this.type)
            {
                case BmLerpTransformType.AnchoredPosition:
                    rectTransform.anchoredPosition = Vector2.Lerp(moveData[0], moveData[1], _per);
                    break;
                case BmLerpTransformType.Rotation:
                    transform.rotation = Quaternion.Lerp(rotationData[0], rotationData[1], _per);
                    break;
                case BmLerpTransformType.RotationLocal:
                    transform.localRotation = Quaternion.Lerp(rotationData[0], rotationData[1], _per);
                    break;
                case BmLerpTransformType.Scale:
                    transform.localScale = Vector3.Lerp(scaleData[0], scaleData[1], _per);
                    break;
                case BmLerpTransformType.TransAll:
                    rectTransform.anchoredPosition = Vector2.Lerp(moveData[0], moveData[1], _per);
                    transform.rotation = Quaternion.Lerp(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.Lerp(scaleData[0], scaleData[1], _per);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    rectTransform.anchoredPosition = Vector2.Lerp(moveData[0], moveData[1], _per);
                    transform.localRotation = Quaternion.Lerp(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.Lerp(scaleData[0], scaleData[1], _per);
                    break;
            }
        }
    }
}
