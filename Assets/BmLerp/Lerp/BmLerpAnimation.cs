using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        [EnumName("是否使用速度曲线")]
        public bool isSpeedCurve = false;

        [EnumName("速度曲线")]
        public AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 1, 1);

        [EnumName("时间轴缩放")]
        public float TimeScale=1;

        public int status = 0;
        protected float tick = 0;
        private bool isForward;
        private float speed = 1.0f;
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

        public void PlayWithTime(float _time, int _loop = 1)
        {
            tick = _time;
            Play(_loop);
        }

        public void Play(int _loop = 1)
        {
            loop = _loop;
            status = 1;
            isForward = true;
            InitLerp(0);
        }

        public void PlayBack()
        {
            isForward = false;
            pingPang = true;
            loop = 1;
            status = 1;
            InitLerp(0);
        }

        public void Stop()
        {
            status = 0;
        }

        public void Pause()
        {
            status = 99;
        }

        public void Resume()
        {
            status = 1;
        }


        private void InitLerp(float _per)
        {
            foreach (var item in groupNode)
            {
                item.lerp.Lerp(_per, true, false);
            }
        }

        private void Update()
        {
            Logic((ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime));
        }

        public void Logic(float deltaTime)
        {
            switch (status)
            {
                case 1:

                    tick += TimeScale * deltaTime * speed;
                    tick = tick > time ? time : tick;
                    LerpPingPang(tick / time);
                    if (tick >= time)
                    {
                        tick = 0;
                        if (loop > 0)
                        {
                            loop--;
                            status = loop == 0 ? 0 : 1;
                        }
                        isForward = !isForward;
                        CleanExec();
                    }
                    break;
            }
        }

        protected override void _Lerp(float _per)
        {
            base._Lerp(_per);
            speed = isSpeedCurve?speedCurve.Evaluate(_per):1.0f;
        }

        public void LerpPingPang(float _percent)
        {
            Lerp((isForward||!pingPang) ?_percent:(1-_percent));
        }

        public void AddEventByTime(float _time, UnityAction _event = null)
        {
            AddEventByPercent(_time / time, _event);
        }

        public void AddEventByPercent(float _progress, UnityAction _event = null)
        {
            var t = new BmLerpEvent();
            t.progress = _progress;
            if (_event != null)
            {
                t.mEvent = new UnityEvent();
                t.mEvent.AddListener(_event);
            }
            eventData.Add(t);
        }
    }
}

