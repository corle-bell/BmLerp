using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bm.Lerp
{
    public class BmLerpCanvasGroup : BmLerpBase
    {
       
        public CanvasGroup graphic;
        protected override void _Lerp(float _per)
        {
            graphic.alpha = _per;
        }

        private void Reset()
        {
            graphic = GetComponent<CanvasGroup>();
        }
    }
}
