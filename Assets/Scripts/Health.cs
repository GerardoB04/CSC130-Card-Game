using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    public UnityEvent PlayerDeath;
    public UnityEvent EnemyDeath;
    [SerializeField] private float StartingHealth;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public void Awake() {
        MaxHealth = StartingHealth;
        CurrentHealth = StartingHealth;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) TakeDamage(1);
    }

    public void TakeDamage(float damage) {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        if (CurrentHealth > 0) {
            AudioManager.Instance.PlaySFX("Injured");
        } else {
            AudioManager.Instance.PlaySFX("Death");
            if (gameObject.CompareTag("Player")) {
                Debug.Log("Player has died.");
                PlayerDeath.Invoke();
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Enemy")) {
                Debug.Log("Enemy has died.");
                EnemyDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}