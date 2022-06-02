using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{

    public class BmLerpTransform : BmLerpBase
    {
        public BmLerpTransformType type;

        [HideInInspector]
        public Vector3 [] moveData;
        [HideInInspector]
        public Vector3 [] scaleData;
        [HideInInspector]
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
            switch (this.type)
            {
                case BmLerpTransformType.Position:
                    transform.position = Vector3.LerpUnclamped(moveData[0], moveData[1], _per);
                    break;
                case BmLerpTransformType.PositionLocal:
                    transform.localPosition = Vector3.LerpUnclamped(moveData[0], moveData[1], _per);
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
                    transform.position = Vector3.LerpUnclamped(moveData[0], moveData[1], _per);
                    transform.rotation = Quaternion.LerpUnclamped(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.LerpUnclamped(scaleData[0], scaleData[1], _per);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    transform.localPosition = Vector3.LerpUnclamped(moveData[0], moveData[1], _per);
                    transform.localRotation = Quaternion.LerpUnclamped(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.LerpUnclamped(scaleData[0], scaleData[1], _per);
                    break;
            }
        }


        protected void _LerpNoCurve(float _per)
        {
            switch (this.type)
            {
                case BmLerpTransformType.Position:
                    transform.position = Vector3.Lerp(moveData[0], moveData[1], _per);
                    break;
                case BmLerpTransformType.PositionLocal:
                    transform.localPosition = Vector3.Lerp(moveData[0], moveData[1], _per);
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
                    transform.position = Vector3.Lerp(moveData[0], moveData[1], _per);
                    transform.rotation = Quaternion.Lerp(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.Lerp(scaleData[0], scaleData[1], _per);
                    break;
                case BmLerpTransformType.TransAllLocal:
                    transform.localPosition = Vector3.Lerp(moveData[0], moveData[1], _per);
                    transform.localRotation = Quaternion.Lerp(rotationData[0], rotationData[1], _per);
                    transform.localScale = Vector3.Lerp(scaleData[0], scaleData[1], _per);
                    break;
            }
        }
    }
}
