using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAdapter : MonoBehaviour
{
    public Vector2 screenSize = new Vector2(1080, 1920);
    // Start is called before the first frame update
    void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

        scaler.referenceResolution = screenSize;

        float w_p = scaler.referenceResolution.x;
        float h_p = scaler.referenceResolution.y;

        float _my_p = h_p / w_p;

        float _t = (float)Screen.height / (float)Screen.width;

        if (_t < _my_p)
        {
            scaler.matchWidthOrHeight = 1.0f;
        }
        else
        {
            scaler.matchWidthOrHeight = 0;
        }
    }
}
