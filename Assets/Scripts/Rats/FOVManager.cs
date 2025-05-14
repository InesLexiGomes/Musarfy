using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class FOVManager : MonoBehaviour
{
    [SerializeField] private GameObject head;
    private Volume vol;

    private UnityEngine.Rendering.Universal.Vignette vignette;
    private void Start()
    {
        vol = FindAnyObjectByType<Volume>();
        vol.profile.TryGet<UnityEngine.Rendering.Universal.Vignette>(out vignette);
    }

    private void Update()
    {
        Vector2 vignettePosition = (Vector2)Camera.main.WorldToScreenPoint(head.transform.position);
        vignettePosition.x /= Screen.width;
        vignettePosition.y /= Screen.height;

        vignette.center.value = vignettePosition;
    }
}
