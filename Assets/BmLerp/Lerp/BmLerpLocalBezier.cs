using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpLocalBezier : BmLerpBezier
    {

        protected override void _Lerp(float _per)
        {
            target.localPosition = CalcPoint(start, control, end, _per);
        }
    }
}
