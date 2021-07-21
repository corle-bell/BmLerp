using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bm.Lerp
{
    public class BmLerpSpriteColor : BmLerpBase
    {
        public Color start=Color.white;
        public Color end=Color.white;

        public SpriteRenderer graphic;
        protected override void _Lerp(float _per)
        {
            graphic.color = Color.Lerp(start, end, _per);
        }

        private void Reset()
        {
            graphic = GetComponent<SpriteRenderer>();
        }
    }
}
