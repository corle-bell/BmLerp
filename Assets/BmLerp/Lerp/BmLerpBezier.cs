using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpBezier : BmLerpBase
    {
        public Vector3 start;
        public Vector3 control;
        public Vector3 end;
        public Transform target;

        protected override void _Lerp(float _per)
        {
            Vector3 _start = transform.TransformPoint(start);
            Vector3 _control = transform.TransformPoint(control);
            Vector3 _end = transform.TransformPoint(end);
            target.position = CalcPoint(_start, _control, _end, _per);
        }

        public Vector3 CalcPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
        }

        private void Reset()
        {
            target = transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.TransformPoint(start), 0.08f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.TransformPoint(control), 0.08f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.TransformPoint(end), 0.08f);
        }
    }
}
