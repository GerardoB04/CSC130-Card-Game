using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider EaseHealthSlider;
    [SerializeField] private float MaxHealth = 6;
    [SerializeField] private float Health;

    private float LerpSpeed = 0.05f;

    void Start() {
        Health = MaxHealth;
    }

    void Update() {
        if (HealthSlider.value != Health) HealthSlider.value = Health;

        if (HealthSlider.value != EaseHealthSlider.value) EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, Health, LerpSpeed);

        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage();
    }

    void TakeDamage() {
        Health -= 1;
    }
}