using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpAnimator : BmLerpBase
    {
        public Animator animator;
        public string state;
        public float currentAnimLen;
        public float bakeAnimLen;

        private void Awake()
        {
            
        }

        void Start()
        {
            BakeAnimation();
        }

        protected override void _Lerp(float _per)
        {
            animator.Play(state, 0, _per);
        }

        public void BakeAnimation()
        {
            animator.speed = 0;
        }

        private void Reset()
        {
            animator = GetComponent<Animator>();
        }
    }
}
