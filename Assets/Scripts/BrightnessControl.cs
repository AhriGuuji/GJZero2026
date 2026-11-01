using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessControl : MonoBehaviour
{
    [SerializeField] private Volume volume; // needs a "Color Adjustments" override

    public void SetBrightness(float value) // slider from -2 to 2
    {
        if (volume.profile.TryGet(out ColorAdjustments adj))
            adj.postExposure.value = value;
    }
}