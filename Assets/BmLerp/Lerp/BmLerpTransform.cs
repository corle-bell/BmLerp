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
    }

    public class BmLerpTransform : BmLerpBase
    {
        public BmLerpTransformType type;

        [HideInInspector]
        public Vector3 [] beginData;
        [HideInInspector]
        public Vector3 [] endData;

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
            switch (this.type)
            {
                case BmLerpTransformType.Position:
                    transform.position = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.PositionLocal:
                    transform.localPosition = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.Rotation:
                    transform.eulerAngles = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.RotationLocal:
                    transform.localEulerAngles = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.Scale:
                    transform.localScale = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.TransAll:
                    transform.position = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    transform.eulerAngles = Vector3.LerpUnclamped(beginData[1], endData[1], _per);
                    transform.localScale = Vector3.LerpUnclamped(beginData[2], endData[2], _per);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    transform.localPosition = Vector3.LerpUnclamped(beginData[0], endData[0], _per);
                    transform.localEulerAngles = Vector3.LerpUnclamped(beginData[1], endData[1], _per);
                    transform.localScale = Vector3.LerpUnclamped(beginData[2], endData[2], _per);
                    break;
            }
        }


        protected void _LerpNoCurve(float _per)
        {
            switch (this.type)
            {
                case BmLerpTransformType.Position:
                    transform.position = Vector3.Lerp(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.PositionLocal:
                    transform.localPosition = Vector3.Lerp(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.Rotation:
                    transform.eulerAngles = Vector3.Lerp(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.RotationLocal:
                    transform.localEulerAngles = Vector3.Lerp(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.Scale:
                    transform.localScale = Vector3.Lerp(beginData[0], endData[0], _per);
                    break;
                case BmLerpTransformType.TransAll:
                    transform.position = Vector3.Lerp(beginData[0], endData[0], _per);
                    transform.eulerAngles = Vector3.Lerp(beginData[1], endData[1], _per);
                    transform.localScale = Vector3.Lerp(beginData[2], endData[2], _per);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    transform.localPosition = Vector3.Lerp(beginData[0], endData[0], _per);
                    transform.localEulerAngles = Vector3.Lerp(beginData[1], endData[1], _per);
                    transform.localScale = Vector3.Lerp(beginData[2], endData[2], _per);
                    break;
            }
        }
    }
}
