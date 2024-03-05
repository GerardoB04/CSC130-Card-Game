using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider EaseHealthSlider;
    [SerializeField] private Health HealthComponent;
    [SerializeField] private float MaxHealth = 6;
    [SerializeField] private float LerpSpeed = 0.05f;

    void Update() {
        if (EaseHealthSlider.maxValue != HealthComponent.MaxHealth) EaseHealthSlider.maxValue = HealthComponent.MaxHealth;
        if (HealthSlider.maxValue != HealthComponent.MaxHealth) HealthSlider.maxValue = Mathf.Lerp(HealthSlider.maxValue, HealthComponent.MaxHealth, LerpSpeed);
        if (HealthSlider.value != HealthComponent.CurrentHealth) HealthSlider.value = HealthComponent.CurrentHealth;

        if (HealthSlider.value != EaseHealthSlider.value) {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, HealthComponent.CurrentHealth, LerpSpeed);
        }
    }
}