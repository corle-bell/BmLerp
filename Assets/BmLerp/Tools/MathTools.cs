using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MathTools
{
    [Obsolete]
    public static bool isIntersectWithRect(Rect _rect, Vector2 _a, Vector2 _b, out Vector2 _ret)
    {
        Vector3 ret;
        _ret = Vector2.zero;
        bool top = TryGetIntersectPoint(new Vector3(_rect.xMin, _rect.yMin, 0), new Vector3(_rect.xMax, _rect.yMin, 0),
            _a, _b, out ret);
        if (top)
        {
            _ret = ret;
            return top;
        }

        bool left = TryGetIntersectPoint(new Vector3(_rect.xMin, _rect.yMin, 0), new Vector3(_rect.xMin, _rect.yMax, 0),
            _a, _b, out ret);
        if (left)
        {
            _ret = ret;
            return left;
        }

        bool right = TryGetIntersectPoint(new Vector3(_rect.xMax, _rect.yMin, 0), new Vector3(_rect.xMax, _rect.yMax, 0),
            _a, _b, out ret);
        if (right)
        {
            _ret = ret;
            return right;
        }

        bool bottom = TryGetIntersectPoint(new Vector3(_rect.xMin, _rect.yMax, 0), new Vector3(_rect.xMax, _rect.yMax, 0),
            _a, _b, out ret);
        if (bottom)
        {
            _ret = ret;
            return bottom;
        }

        return false;
    }


    [Obsolete]
    public static int GetIntersection(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 contractPoint)
    {
        contractPoint = new Vector3(0, 0);

        if (Mathf.Abs(b.z - a.z) + Mathf.Abs(b.x - a.x) + Mathf.Abs(d.z - c.z)
                + Mathf.Abs(d.x - c.x) == 0)
        {
            if ((c.x - a.x) + (c.z - a.z) == 0)
            {
                //Debug.Log("ABCD是同一个点！");
            }
            else
            {
                //Debug.Log("AB是一个点，CD是一个点，且AC不同！");
            }
            return 0;
        }

        if (Mathf.Abs(b.z - a.z) + Mathf.Abs(b.x - a.x) == 0)
        {
            if ((a.x - d.x) * (c.z - d.z) - (a.z - d.z) * (c.x - d.x) == 0)
            {
                //Debug.Log("A、B是一个点，且在CD线段上！");
            }
            else
            {
                //Debug.Log("A、B是一个点，且不在CD线段上！");
            }
            return 0;
        }
        if (Mathf.Abs(d.z - c.z) + Mathf.Abs(d.x - c.x) == 0)
        {
            if ((d.x - b.x) * (a.z - b.z) - (d.z - b.z) * (a.x - b.x) == 0)
            {
                //Debug.Log("C、D是一个点，且在AB线段上！");
            }
            else
            {
                //Debug.Log("C、D是一个点，且不在AB线段上！");
            }
            return 0;
        }

        if ((b.z - a.z) * (c.x - d.x) - (b.x - a.x) * (c.z - d.z) == 0)
        {
            //Debug.Log("线段平行，无交点！");
            return 0;
        }

        contractPoint.x = ((b.x - a.x) * (c.x - d.x) * (c.z - a.z) -
                c.x * (b.x - a.x) * (c.z - d.z) + a.x * (b.z - a.z) * (c.x - d.x)) /
                ((b.z - a.z) * (c.x - d.x) - (b.x - a.x) * (c.z - d.z));
        contractPoint.z = ((b.z - a.z) * (c.z - d.z) * (c.x - a.x) - c.z
                * (b.z - a.z) * (c.x - d.x) + a.z * (b.x - a.x) * (c.z - d.z))
                / ((b.x - a.x) * (c.z - d.z) - (b.z - a.z) * (c.x - d.x));

        if ((contractPoint.x - a.x) * (contractPoint.x - b.x) <= 0
                && (contractPoint.x - c.x) * (contractPoint.x - d.x) <= 0
                && (contractPoint.z - a.z) * (contractPoint.z - b.z) <= 0
                && (contractPoint.z - c.z) * (contractPoint.z - d.z) <= 0)
        {

            //Debug.Log("线段相交于点(" + contractPoint.x + "," + contractPoint.z + ")！");
            return 1; // '相交  
        }
        else
        {
            //Debug.Log("线段相交于虚交点(" + contractPoint.x + "," + contractPoint.z + ")！");
            return -1; // '相交但不在线段上  
        }
    }

    [Obsolete]
    private static bool TryGetIntersectPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 intersectPos)
    {
        intersectPos = Vector3.zero;

        Vector3 ab = b - a;
        Vector3 ca = a - c;
        Vector3 cd = d - c;

        Vector3 v1 = Vector3.Cross(ca, cd);

        if (Mathf.Abs(Vector3.Dot(v1, ab)) > 1e-6)
        {
            // 不共面
            return false;
        }

        if (Vector3.Cross(ab, cd).sqrMagnitude <= 1e-6)
        {
            // 平行
            return false;
        }

        Vector3 ad = d - a;
        Vector3 cb = b - c;
        // 快速排斥
        if (Mathf.Min(a.x, b.x) > Mathf.Max(c.x, d.x) || Mathf.Max(a.x, b.x) < Mathf.Min(c.x, d.x)
           || Mathf.Min(a.y, b.y) > Mathf.Max(c.y, d.y) || Mathf.Max(a.y, b.y) < Mathf.Min(c.y, d.y)
           || Mathf.Min(a.z, b.z) > Mathf.Max(c.z, d.z) || Mathf.Max(a.z, b.z) < Mathf.Min(c.z, d.z)
        )
            return false;

        // 跨立试验
        if (Vector3.Dot(Vector3.Cross(-ca, ab), Vector3.Cross(ab, ad)) > 0
            && Vector3.Dot(Vector3.Cross(ca, cd), Vector3.Cross(cd, cb)) > 0)
        {
            Vector3 v2 = Vector3.Cross(cd, ab);
            float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
            intersectPos = a + ab * ratio;
            return true;
        }

        return false;
    }

    public static float Lerp(float _input_min, float _input_max, float _output_min, float _output_max, float _v)
    {
        return Mathf.Lerp(_output_min, _output_max, Mathf.InverseLerp(_input_min, _input_max, _v));
    }

    public static Vector3 GetBezierPoint(Vector3 start, Vector3 mid, Vector3 end, float _t)
    {
        Vector3 t0 = Vector3.Lerp(start, mid, _t);
        Vector3 t1 = Vector3.Lerp(mid, end, _t);
        Vector3 p = Vector3.Lerp(t0, t1, _t);
        return p;

        //return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
    }

    public static Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 _bp0 = Vector3.Lerp(p0, p1, t);
        Vector3 _bp1 = Vector3.Lerp(p1, p2, t);
        Vector3 _bp2 = Vector3.Lerp(p2, p3, t);
        return GetBezierPoint(_bp0, _bp1, _bp2, t);

        //return (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
    }

    public static Vector3 GetSplinePoint(Vector3[] pts, float t, Transform _parent = null)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
        float u = t * (float)numSections - (float)currPt;

        Vector3 p0 = pts[currPt];
        Vector3 p1 = pts[currPt + 1];
        Vector3 p2 = pts[currPt + 2];
        Vector3 p3 = pts[currPt + 3];

        if (_parent != null)
        {
            p0 = _parent.TransformPoint(pts[currPt]);
            p1 = _parent.TransformPoint(pts[currPt + 1]);
            p2 = _parent.TransformPoint(pts[currPt + 2]);
            p3 = _parent.TransformPoint(pts[currPt + 3]);
        }

        return 0.5f * (
            (-p0 + 3f * p1 - 3f * p2 + p3) * (u * u * u)
            + (2f * p0 - 5f * p1 + 4f * p2 - p3) * (u * u)
            + (-p0 + p2) * u
            + 2f * p1
            );
    }
    public static Vector3 GetSplinePoint(List<Vector3> pts, float t, Transform _parent = null)
    {
        int numSections = pts.Count - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
        float u = t * (float)numSections - (float)currPt;

        Vector3 p0 = pts[currPt];
        Vector3 p1 = pts[currPt + 1];
        Vector3 p2 = pts[currPt + 2];
        Vector3 p3 = pts[currPt + 3];

        if (_parent != null)
        {
            p0 = _parent.TransformPoint(pts[currPt]);
            p1 = _parent.TransformPoint(pts[currPt + 1]);
            p2 = _parent.TransformPoint(pts[currPt + 2]);
            p3 = _parent.TransformPoint(pts[currPt + 3]);
        }

        return 0.5f * (
            (-p0 + 3f * p1 - 3f * p2 + p3) * (u * u * u)
            + (2f * p0 - 5f * p1 + 4f * p2 - p3) * (u * u)
            + (-p0 + p2) * u
            + 2f * p1
            );
    }

    public static Vector3 Reverse(Vector3 _v)
    {
        return _v - Vector3.one;
    }

    public static Vector3 Abs(Vector3 _v)
    {
        return new Vector3(Mathf.Abs(_v.x), Mathf.Abs(_v.y), Mathf.Abs(_v.z));
    }

    public static bool Between(float v, float _min, float _max)
    {
        return v >= _min && v <= _max;
    }

    public static bool Between(int v, int _min, int _max)
    {
        return v >= _min && v <= _max;
    }

    public static bool Equals(float _a, float _b)
    {
        return Mathf.Abs(_a - _b) <= Mathf.Epsilon;
    }

    public static float PerlinNoiseRandom(float _a, float _b, float _x, float _y)
    {
        float len = Mathf.Abs(_a - _b);
        return Mathf.PerlinNoise(_x, _y) * len + Mathf.Min(_a, _b);
    }

    #region 生成高斯分布数
    // mean：均值，variance：方差
    // min和max用于去掉不需要的偏差值
    public static float NextGaussian(float mean, float variance, float min, float max)
    {
        float x;
        do
        {
            x = NextGaussian(mean, variance);
        } while (x < min || x > max);
        return x;
    }

    public static float NextGaussian(float mean, float standard_deviation)
    {
        return mean + NextGaussian() * standard_deviation;
    }

    public static float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * UnityEngine.Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * UnityEngine.Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);
        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
        return v1 * s;
    }
    #endregion
}
