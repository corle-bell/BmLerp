using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpSpline : BmLerpBase
    {
        [HideInInspector]
        public List<Vector3> points = new List<Vector3>();
        public Transform target;

        protected override void _Lerp(float _per)
        {
            target.position = MathTools.GetSplinePoint(points, _per, transform);
        }

        private void Reset()
        {
            target = transform;
            points.Add(Vector3.zero);
            points.Add(Vector3.zero);
        }

#if UNITY_EDITOR
        public Color lineColor=Color.red;
        private void OnDrawGizmos()
        {
            if (points.Count == 0) return;
            Gizmos.color = lineColor;
            for (int i = 1; i < 100; i++)
            {
                float t = i;
                var pos = MathTools.GetSplinePoint(points, (t - 1) / 100, transform);
                var pos1 = MathTools.GetSplinePoint(points, t / 100, transform);
                Gizmos.DrawLine(pos, pos1);
            }            
        }
#endif
    }

}