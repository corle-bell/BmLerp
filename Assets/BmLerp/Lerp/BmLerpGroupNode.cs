using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    [System.Serializable]
    public class BmLerpGroupNode
    {
        [EnumName("区间左值")]
        public float minInGroup = 0;
        [EnumName("区间右值")]
        public float maxInGroup = 1.0f;
        [EnumName("截断曲线")]
        public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
        [EnumName("Lerp对象")]
        public BmLerpBase lerp;
    }

}
