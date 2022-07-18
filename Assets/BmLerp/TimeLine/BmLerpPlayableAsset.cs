//*************************************************
//----Author:       Cyy 
//
//----CreateDate:   2022-07-18 14:24:26
//
//----Desc:         Create By BM
//
//**************************************************


using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Bm.Lerp.TimeLine
{
    [System.Serializable]
    public class BmLerpPlayableAsset : PlayableAsset
    {
        [Range(0, 1)]
        public float start;

        [Range(0, 1)]
        public float end;

        [Header("Clip“˝”√")]
        public TimelineClip clip;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var scriptPlayable = ScriptPlayable<BmLerpBehaviour>.Create(graph);            
            var scriptBehavior = scriptPlayable.GetBehaviour();
            scriptBehavior.start = start;
            scriptBehavior.end = end;
            scriptBehavior.clip = clip;
            return scriptPlayable;
        }

    }
}

