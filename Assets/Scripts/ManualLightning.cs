using UnityEngine;

/// <summary>
/// Fakes 3D light on an Unlit 2D sprite by tinting the material's base color.
/// Looks for lights inside a radius, picks the closest one and uses its
/// color, intensity and distance falloff to compute the tint.
/// Put it on the same GameObject as the SpriteRenderer.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ManualLightning : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRadius = 8f;
    [Tooltip("How often (seconds) the scene is re-scanned for lights. Lights are cached between scans.")]
    [SerializeField] private float lightRescanInterval = 1f;
    [SerializeField] private bool includeDirectionalLights = false;
    [SerializeField] private LayerMask lightCullingMask = ~0;

    [Header("Look")]
    [Tooltip("Color of the sprite when no light reaches it.")]
    [SerializeField] private Color ambientColor = new Color(0.15f, 0.15f, 0.2f, 1f);
    [Tooltip("The sprite's own tint (white keeps the original texture colors).")]
    [SerializeField] private Color baseTint = Color.white;
    [SerializeField] private float lightStrength = 1f;
    [Tooltip("Higher = light fades faster with distance.")]
    [SerializeField] private float falloffPower = 2f;
    [SerializeField] private bool respectSpotAngle = true;
    [Tooltip("Colors can go above 1 (brighter than the texture). Keep 1 for no overexposure.")]
    [SerializeField] private float maxBrightness = 1.2f;
    [Tooltip("0 = instant change, higher = smoother.")]
    [SerializeField] private float smoothing = 10f;

    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor"); // URP Unlit
    private static readonly int ColorId = Shader.PropertyToID("_Color");         // Built-in / Sprites-Default

    private SpriteRenderer sr;
    private MaterialPropertyBlock block;
    private int colorProperty;
    private Light[] cachedLights = new Light[0];
    private float nextScan;
    private Color currentColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();

        colorProperty = sr.sharedMaterial != null && sr.sharedMaterial.HasProperty(BaseColorId)
            ? BaseColorId
            : ColorId;

        currentColor = ambientColor;
    }

    private void OnEnable()
    {
        nextScan = 0f;
    }

    private void Update()
    {
        if (Time.time >= nextScan)
        {
            cachedLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
            nextScan = Time.time + lightRescanInterval;
        }

        Color target = ComputeTargetColor();

        currentColor = smoothing <= 0f
            ? target
            : Color.Lerp(currentColor, target, 1f - Mathf.Exp(-smoothing * Time.deltaTime));

        Apply(currentColor);
    }

    private Color ComputeTargetColor()
    {
        Light closest = null;
        float closestDist = float.MaxValue;
        Vector3 pos = transform.position;

        foreach (Light l in cachedLights)
        {
            if (l == null || !l.enabled || !l.gameObject.activeInHierarchy) continue;
            if ((lightCullingMask.value & (1 << l.gameObject.layer)) == 0) continue;

            if (l.type == LightType.Directional)
            {
                // Directional lights have no position; treat as the fallback if nothing else is found.
                if (includeDirectionalLights && closest == null) closest = l;
                continue;
            }

            if (l.type != LightType.Point && l.type != LightType.Spot) continue;

            float dist = Vector3.Distance(pos, l.transform.position);
            if (dist > detectionRadius || dist > l.range) continue;

            if (respectSpotAngle && l.type == LightType.Spot)
            {
                Vector3 toSprite = (pos - l.transform.position).normalized;
                if (Vector3.Angle(l.transform.forward, toSprite) > l.spotAngle * 0.5f) continue;
            }

            if (dist < closestDist)
            {
                closestDist = dist;
                closest = l;
            }
        }

        Color light = Color.black;

        if (closest != null)
        {
            float attenuation = 1f;

            if (closest.type != LightType.Directional)
            {
                float t = Mathf.Clamp01(closestDist / Mathf.Min(closest.range, detectionRadius));
                attenuation = Mathf.Pow(1f - t, falloffPower);
            }

            light = closest.color * (closest.intensity * attenuation * lightStrength);
        }

        // lit result = baseTint * (ambient + light)
        Color lit = ambientColor + light;
        Color result = new Color(
            Mathf.Min(baseTint.r * lit.r, maxBrightness),
            Mathf.Min(baseTint.g * lit.g, maxBrightness),
            Mathf.Min(baseTint.b * lit.b, maxBrightness),
            baseTint.a);

        return result;
    }

    private void Apply(Color color)
    {
        sr.GetPropertyBlock(block); // keep the sprite texture that SpriteRenderer already put in the block
        block.SetColor(colorProperty, color);
        sr.SetPropertyBlock(block);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.9f, 0.3f, 0.6f);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
