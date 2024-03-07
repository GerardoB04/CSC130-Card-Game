using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public List<GameObject> Cards;
    public List<Transform> CardTransforms;
    public UnityEvent PlayerDrawCard;
    public UnityEvent PlayerPass;
    public UnityEvent EnableVignette;
    public UnityEvent DisableVignette;
    [SerializeField] private GameObject UISettings;
    [SerializeField] private int CardValues;

    private Health PlayerHealth;
    private bool IsTurn = false;

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
                PlayerPass.Invoke();
                DisableVignette.Invoke();
                IsTurn = false;
            }
        }
        UpdateCardValues();
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

    public void ClearDeck() { 
        Cards.Clear();
    }

    public void SetTurn(bool turn) {
        if (turn) { 
            EnableVignette.Invoke();
            IsTurn = true;
        } else IsTurn = false;
    }

    public void UpdateCardValues() {
        int CardVal = 0;
        foreach (var card in Cards) CardVal += card.GetComponent<Card>().Value;
        CardValues = CardVal;
    }

    public int GetCardValues() {
        int CardVal = 0;
        if (Cards.Count <= 0) return CardVal;
        foreach (var card in Cards) CardVal += card.GetComponent<Card>().Value;
        return CardVal;
    }
}