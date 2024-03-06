using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public List<GameObject> Cards;
    public List<Transform> CardTransforms;

    private Health EnemyHealth;

    void Start() {
        EnemyHealth = GetComponent<Health>();
    }

    void Update() {
        // Instert enemy AI here
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
                Instantiate(Cards[1], CardTransforms[i].transform);
                return;
            }
        }
    }
}