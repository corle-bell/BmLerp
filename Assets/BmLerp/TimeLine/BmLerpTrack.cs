//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-18 14:22:21
//
//----Desc:         Create By BM
//
//**************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Bm.Lerp.TimeLine
{
    [TrackBindingType(typeof(BmLerpBase))]
    [TrackClipType(typeof(BmLerpPlayableAsset))]
    public class BmLerpTrack : PlayableTrack
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var playerAsset = clip.asset as BmLerpPlayableAsset;
                if (playerAsset != null)
                {
                    playerAsset.clip = clip;
                }
            }

            return ScriptPlayable<BmLerpBehaviour>.Create(graph, inputCount);
        }
    }
}
