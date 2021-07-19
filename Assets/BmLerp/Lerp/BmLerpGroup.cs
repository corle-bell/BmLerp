using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpGroup : BmLerpBase
    {
        [HideInInspector]
        public List<BmLerpGroupNode> groupNode = new List<BmLerpGroupNode>();

        [HideInInspector]
        public float start_time=0;

        [HideInInspector]
        public float space_time=0.1f;

        [HideInInspector]
        public float node_len_time = 0.1f;
        public override void Init()
        {
            base.Init();

            CheckNode();
            percent = 0;
            foreach (var item in groupNode)
            {
                item.lerp.Init();
            }
        }
        

        protected override void _Lerp(float _per)
        {
            foreach (var item in groupNode)
            {
                if (_per >= item.minInGroup)
                {
                    item.lerp.Lerp(item.curve.Evaluate(MathTools.Lerp(item.minInGroup, item.maxInGroup, 0, 1.0f, _per)), true);
                }
            }
        }

        public void CheckNode()
        {
            for (int i=0; i<groupNode.Count; i++)
            {
                if(groupNode[i]==null || (groupNode[i] != null && groupNode[i].lerp == null))
                {
                    groupNode.RemoveAt(i);
                }
            }
        }
    }
}

