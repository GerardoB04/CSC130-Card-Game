using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider EaseHealthSlider;
    [SerializeField] private Health HealthComponent;
    [SerializeField] private TMP_Text CardValueText;
    [SerializeField] private float MaxHealth = 6;
    [SerializeField] private float LerpSpeed = 0.05f;
    [SerializeField] private bool IsPlayer = true;

    private GameObject PlayerObject;
    private GameObject EnemyObject;

    private void Start() {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        EnemyObject = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update() {
        if (EaseHealthSlider.maxValue != HealthComponent.MaxHealth) EaseHealthSlider.maxValue = HealthComponent.MaxHealth;
        if (HealthSlider.maxValue != HealthComponent.MaxHealth) HealthSlider.maxValue = Mathf.Lerp(HealthSlider.maxValue, HealthComponent.MaxHealth, LerpSpeed);
        if (HealthSlider.value != HealthComponent.CurrentHealth) HealthSlider.value = HealthComponent.CurrentHealth;

        if (HealthSlider.value != EaseHealthSlider.value) {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, HealthComponent.CurrentHealth, LerpSpeed);
        }

        if (IsPlayer) {
            CardValueText.text = PlayerObject.GetComponent<Player>().GetCardValues() + "/21";
        } else CardValueText.text = "?+" + EnemyObject.GetComponent<Enemy>().GetCardValues() + "/21";
    }
}