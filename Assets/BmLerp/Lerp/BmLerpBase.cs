﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Bm.Lerp
{

    [System.Serializable]
    public struct BmLerpEvent
    {
        [EnumName("事件插入时间(S):")]
        public float progress;
        public UnityEvent mEvent;
    }
    public class BmLerpBase : MonoBehaviour
    {
        [EnumName("进度")]
        public float percent;

        [EnumName("曲线")]
        public AnimationCurve curve;
        [EnumName("是否使用曲线")]
        public bool isCurve;

        [EnumName("打开曲线区间限制")]
        public bool isValueCurve;

        [EnumName("事件是否只执行一次")]
        public bool isOnceEvent;

        public BmLerpEvent [] eventData;

        [HideInInspector]
        public bool lockByGroup = false;


        private bool[] isExec;

        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            if(eventData!=null) isExec = new bool[eventData.Length];
            CleanExec(true);
        }

        protected void CleanExec(bool _force=false)
        {
            if(!isOnceEvent || _force)
            {
                if (eventData != null) BmTools.ArrayClean(isExec);
            }
        }

        protected void ExecEvent(float _percent)
        {
            for(int i=0; i< eventData.Length; i++)
            {
                if (_percent >= eventData[i].progress && !isExec[i])
                {
                    isExec[i] = true;
                    eventData[i].mEvent.Invoke();
                }
            }
        }

        public void Lerp(float _percent, bool _isGroup=false, bool _event=true)
        {
            if (_isGroup && lockByGroup) return;
            percent = _percent;            
            float _per = percent;
            if(isCurve)
            {
                _per = curve.Evaluate(_per);
            }
            _Lerp(_per);
            
            if(Application.isPlaying && _event)
            {
                ExecEvent(percent);
            }
        }

        protected virtual void _Lerp(float _per)
        {

        }
    }

}
