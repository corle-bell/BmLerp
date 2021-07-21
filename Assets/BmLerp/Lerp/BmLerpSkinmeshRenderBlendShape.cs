using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class BmLerpSkinmeshRenderBlendShape : BmLerpBase
    {
        public SkinnedMeshRenderer skinnedMesh;
        public int [] id = new int[] { 0 };


        protected override void _Lerp(float _per)
        {
            for(int i=0; i<id.Length; i++)
            {
                skinnedMesh.SetBlendShapeWeight(id[i], MathTools.Lerp(0, 1, 0, 100, _per));
            }
        }

        private void Reset()
        {
            skinnedMesh = GetComponent<SkinnedMeshRenderer>();
            if(skinnedMesh==null)
            {
                Debug.LogError("@@@@ NO Skin Mesh");
            }
        }
    }
}
