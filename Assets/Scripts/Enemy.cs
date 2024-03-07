using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {
    public List<GameObject> Cards;
    public List<Transform> CardTransforms;
    public UnityEvent EnemyDrawCard;
    public UnityEvent EnemyPass;
    [SerializeField] private int CardValues;

    private Health EnemyHealth;
    public bool IsTurn = false;

    void Start() {
        EnemyHealth = GetComponent<Health>();
    }

    void Update() {
        UpdateCardValues();

        if (IsTurn) {
            if (CardValues < 21) EnemyDrawCard.Invoke();
            else {
                Debug.Log("Enemy pass");
                EnemyPass.Invoke();
                IsTurn = false;
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

    public void ClearDeck() {
        Cards.Clear();
    }

    public void SetTurn(bool turn) {
        if (turn) IsTurn = true;
        else IsTurn = false;
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
        return CardVal - Cards[0].GetComponent<Card>().Value;
    }
}