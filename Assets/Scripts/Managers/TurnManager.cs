using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { START, PLAYERTURN, ENEMYTURN, RESET, WON, LOST }

public class TurnManager : MonoBehaviour {
    [Header("Decks")]
    public Deck FullDeck;
    public List<GameObject> FullDeckCopy;

    [Header("Card Transforms")]
    public List<Transform> PlayerTransforms;
    public List<Transform> enemyTransforms;

    private bool PlayerTurnPass = false;
    private bool EnemyTurnPass = false;

    private GameObject PlayerObject;
    private GameObject EnemyObject;

    public GameStates State;

    void Start() {
        State = GameStates.START;
        StartGame();
    }

    void StartGame() {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        EnemyObject = GameObject.FindGameObjectWithTag("Enemy");
        var test = FullDeck.CardDeck;

        FullDeckCopy = test;

        PlayerObject.GetComponent<Player>().FirstCardDraw(GetRandomAndRemove(FullDeckCopy));
        PlayerObject.GetComponent<Player>().DrawCard(GetRandomAndRemove(FullDeckCopy));

        EnemyObject.GetComponent<Enemy>().FirstCardDraw(GetRandomAndRemove(FullDeckCopy));
        EnemyObject.GetComponent<Enemy>().DrawCard(GetRandomAndRemove(FullDeckCopy));
    }

    public GameObject GetRandomAndRemove(List<GameObject> deck) {
        int RandomNumber = Random.Range(0, deck.Count);
        var RandomCard = deck[RandomNumber];

        deck.RemoveAt(RandomNumber);

        return RandomCard;
    }

    public void PlayerDraw() {
        PlayerObject.GetComponent<Player>().DrawCard(GetRandomAndRemove(FullDeckCopy));
    }

    public void Punishment() {
        // Punish entity furthest from 21
    }
}