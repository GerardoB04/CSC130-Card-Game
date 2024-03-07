using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public List<GameObject> Cards;
    public List<Transform> CardTransforms;
    public UnityEvent PlayerDrawCard;
    [SerializeField] private GameObject UISettings;

    private Health PlayerHealth;
    private bool IsTurn = true;

    void Start() {
        PlayerHealth = GetComponent<Health>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!UISettings.activeInHierarchy) UISettings.SetActive(true);
            else UISettings.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q)) { 
            if (IsTurn) {
                PlayerDrawCard.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (IsTurn) {
                // Pass turn code
            }
        }
    }

    public void FirstCardDraw(GameObject card) { 
        card.GetComponent<Card>().TurnOver();
        Cards.Add(card);
        Instantiate(Cards[0], CardTransforms[0].transform);
    }

    public void DrawCard(GameObject card) {
        card.GetComponent<Card>().TurnUp();
        Cards.Add(card);

        for (int i = 1; i < CardTransforms.Count; i++) {
            if (CardTransforms[i].childCount == 0) {
                Instantiate(Cards[i], CardTransforms[i].transform);
                return;
            }
        }
    }
}