using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpAnimation : BmLerpGroup
    {
        [EnumName("自动播放")]
        public bool autoPlay=true;

        [EnumName("动画时长")]
        public float time=1.0f;

        [EnumName("是否忽略TimeScale")]
        public bool ignoreTimeScale=false;

        [EnumName("循环次数")]
        public int loop = -1;

        [EnumName("乒乓")]
        public bool pingPang=false;

        public int status = 0;
        protected float tick = 0;
        private bool isForward;
        public override void Init()
        {
            base.Init();

            if(autoPlay)
            {
                Play(loop);
            }
        }

        public void Play(float _time, int _loop = 1)
        {
            time = _time;
            Play(_loop);
        }

        public void Play(int _loop = 1)
        {
            loop = _loop;
            status = 1;
            isForward = true;
            Lerp(0);
        }

        public void Stop()
        {
            status = 0;
        }

        private void Update()
        {
            switch (status)
            {
                case 1:
                    tick += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
                    tick = tick > time ? time : tick;
                    LerpPingPang(tick / time);
                    if (tick>=time)
                    {
                        tick = 0;
                        if(loop>0)
                        {
                            loop--;
                            status = loop==0?0:1;
                        }
                        isForward = !isForward;
                    }
                    break;
            }
        }

        public void LerpPingPang(float _percent)
        {
            Lerp((isForward||!pingPang) ?_percent:(1-_percent));
        }
    }
}

