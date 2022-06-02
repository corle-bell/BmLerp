using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Bm.Lerp
{
    public class BmLerpImageFill : BmLerpBase
    {
        public Image img;
        protected override void _Lerp(float _per)
        {
            img.fillAmount = _per;
        }

        private void Reset()
        {
            img = GetComponent<Image>();
            img.type = Image.Type.Filled;
        }
    }
}
