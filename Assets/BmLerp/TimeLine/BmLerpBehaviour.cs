//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-18 14:25:48
//
//----Desc:         Create By BM
//
//**************************************************

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Bm.Lerp.TimeLine
{
    // A behaviour that is attached to a playable
    public class BmLerpBehaviour : PlayableBehaviour
    {
        public BmLerpBase context;
        public float start;
        public float end;

        public TimelineClip clip;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            context = playerData as BmLerpBase;

            if(clip!=null)
            {
                var timelineTime = playable.GetGraph().GetRootPlayable(0).GetTime();
                float p = Mathf.InverseLerp((float)clip.start, (float)clip.end, (float)timelineTime);

                context.Lerp(Mathf.Lerp(start, end, p));
            }
        }

    }
}
