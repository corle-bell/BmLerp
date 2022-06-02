using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    [System.Serializable]
    public struct BmLerpMaterialUV
    {
        public Vector2 tilling;
        public Vector2 offset;
    }

    [System.Serializable]
    public class BmLerpMaterialNode
    {
        public string name;
        public MaterialParamsLerpType type;
        public Color[] colorParams = new Color[] { Color.white, Color.white };
        public float[] floatParams = new float[] { 0, 1 };
        public Vector4[] vecParams = new Vector4[] { Vector4.zero, Vector4.one };
        public BmLerpMaterialUV[] uvParams = new BmLerpMaterialUV[2];
    }

    public enum MaterialParamsLerpType
    {
        Float,
        Color,
        Vector4,
        UV,
    }

    public class BmLerpMaterial : BmLerpBase
    {
        public Renderer renderer;
        public string material_paramsName;
        public BmLerpMaterialNode[] lerpData;
        public override void Init()
        {
            base.Init();
        }

        protected override void _Lerp(float _per)
        {
            base._Lerp(_per);
            foreach (var i in lerpData)
            {
                _LerpParams(_per, i);
            }            
        }

        private void _LerpParams(float _p, BmLerpMaterialNode data)
        {
#if UNITY_EDITOR
            var mat = Application.isPlaying ? renderer.material : renderer.sharedMaterial;
#else
            var mat = renderer.material;
#endif

            switch (data.type)
            {
                case MaterialParamsLerpType.Float:
                    mat.SetFloat(data.name, Mathf.Lerp(data.floatParams[0], data.floatParams[1], _p));
                    break;
                case MaterialParamsLerpType.Color:
                    mat.SetColor(data.name, Color.Lerp(data.colorParams[0], data.colorParams[1], _p));
                    break;
                case MaterialParamsLerpType.Vector4:
                    mat.SetVector(data.name, Vector4.Lerp(data.vecParams[0], data.vecParams[1], _p));
                    break;
                case MaterialParamsLerpType.UV:
                    mat.SetTextureScale("_MainTex", Vector2.Lerp(data.uvParams[0].tilling, data.uvParams[1].tilling, _p));
                    mat.SetTextureOffset("_MainTex", Vector2.Lerp(data.uvParams[0].offset, data.uvParams[1].offset, _p));                    
                    break;
            }
        }

        private void Reset()
        {
            renderer = GetComponent<Renderer>();
        }
    }
}

