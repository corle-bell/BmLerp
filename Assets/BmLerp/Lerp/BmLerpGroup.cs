using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpGroup : BmLerpBase
    {
        [HideInInspector]
        public List<BmLerpBase> groupNode = new List<BmLerpBase>();

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
                item.Init();
            }
        }
        

        protected override void _Lerp(float _per)
        {
            foreach (var item in groupNode)
            {
                item.Lerp(_per, true);
            }
        }

        public void CheckNode()
        {
            for (int i=0; i<groupNode.Count; i++)
            {
                if(groupNode[i]==null)
                {
                    groupNode.RemoveAt(i);
                }
            }
        }
    }
}

