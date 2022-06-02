using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpSkinmeshRenderBlendShape : BmLerpBase
    {
        public SkinnedMeshRenderer skinnedMesh;

        [HideInInspector]
        public List<BmLerpBlendShapeNode> groupNode = new List<BmLerpBlendShapeNode>();

        [HideInInspector]
        public float start_time = 0;

        [HideInInspector]
        public float space_time = 0.1f;

        [HideInInspector]
        public float node_len_time = 0.1f;

        public override void Init()
        {
            base.Init();

            CheckNode();
            percent = 0;
          
        }

        public void CheckNode()
        {
            for (int i = 0; i < groupNode.Count; i++)
            {
                if (groupNode[i] == null)
                {
                    groupNode.RemoveAt(i);
                }
            }
        }

        public BmLerpBlendShapeNode AddNode()
        {
            var t = new BmLerpBlendShapeNode();
            groupNode.Add(t);
            return t;
        }

        protected override void _Lerp(float _per)
        {
            for(int i=0; i< groupNode.Count; i++)
            {
                skinnedMesh.SetBlendShapeWeight(groupNode[i].Id, Lerp2ShapeWeight(groupNode[i], _per));
            }
        }

        private float Lerp2ShapeWeight(BmLerpBlendShapeNode node, float _per)
        {
            return MathTools.Lerp(0, 1, 0, 100, 
                node.curve.Evaluate(
                    MathTools.Lerp(node.minInGroup, node.maxInGroup, 0, 1.0f, _per)
               ));            
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
