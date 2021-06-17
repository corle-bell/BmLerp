﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bm.Lerp
{
    public class BmLerpGraphicsColor : BmLerpBase
    {
        public Color start;
        public Color end;

        public Graphic graphic;
        protected override void _Lerp(float _per)
        {
            graphic.color = Color.Lerp(start, end, _per);
        }

        private void Reset()
        {
            graphic = GetComponent<Graphic>();
        }
    }
}
