/*************************************************
----Author:       Cyy 

----CreateDate:   2022-04-02 11:20:21

----Desc:         Create By BM
**************************************************/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpAnimationClip : BmLerpBase
    {
        public AnimationClip clip;
        protected override void _Lerp(float _per)
        {
            clip.SampleAnimation(gameObject, Mathf.Lerp(0, clip.length, _per));            
        }
    }
}

