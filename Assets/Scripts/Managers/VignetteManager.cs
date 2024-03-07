using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VignetteManager : MonoBehaviour {
    [SerializeField] private ScriptableRendererFeature Vignette;
    [SerializeField] private Material VignetteMaterial;

    [Header("Time Stats")]
    [SerializeField] private float FadeInTime;
    [SerializeField] private float FadeOutTime;
    [SerializeField] private float LerpSpeed = 0.05f;

    private int Intensity = Shader.PropertyToID("_VignetteIntensity");
    private int Power = Shader.PropertyToID("_VignetteRadiusPower");

    private const float IntensityStartAmmount = 0.675f;
    private const float PowerStartAmmount = 4.2f;

    void Start() {
        Vignette.SetActive(false);
    }

    public void StartFadeIn() {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut() {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn() {
        Vignette.SetActive(true);
        VignetteMaterial.SetFloat(Intensity, 0);
        VignetteMaterial.SetFloat(Power, 0);

        float ElapsedTime = 0;
        while (ElapsedTime < FadeInTime) {
            ElapsedTime += Time.deltaTime;

            float LerpedIntensity = Mathf.Lerp(0f, IntensityStartAmmount, (ElapsedTime / FadeOutTime));
            float LerpedPower = Mathf.Lerp(0f, PowerStartAmmount, (ElapsedTime / FadeOutTime));

            VignetteMaterial.SetFloat(Intensity, LerpedIntensity);
            VignetteMaterial.SetFloat(Power, LerpedPower);

            yield return null;
        }
    }

    private IEnumerator FadeOut() {
        Vignette.SetActive(true);

        float ElapsedTime = 0;
        while (ElapsedTime < FadeOutTime) { 
            ElapsedTime += Time.deltaTime;

            float LerpedIntensity = Mathf.Lerp(IntensityStartAmmount, 0f, (ElapsedTime / FadeOutTime));
            float LerpedPower = Mathf.Lerp(PowerStartAmmount, 0f, (ElapsedTime / FadeOutTime));

            VignetteMaterial.SetFloat(Intensity, LerpedIntensity);
            VignetteMaterial.SetFloat(Power, LerpedPower);

            yield return null;
        }

        Vignette.SetActive(false);
    }
}