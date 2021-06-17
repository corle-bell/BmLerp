using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatLerpConvert
{
    private float input_min, input_max;
    private float output_min, output_max;
    public FloatLerpConvert(float _input_min, float _input_max, float _output_min, float _output_max)
    {
        SetInput(_input_min, _input_max);
        SetOutput(_output_min, _output_max);
    }

    public void SetInput(float _min, float _max)
    {
        input_min = _min;
        input_max = _max;
    }

    public void SetOutput(float _min, float _max)
    {
        output_min = _min;
        output_max = _max;
    }

    public float Lerp(float _v)
    {
        return MathTools.Lerp(input_min, input_max, output_min, output_max, _v);
    }
}
