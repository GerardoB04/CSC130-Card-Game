using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum GameStates { START, PLAYERTURN, ENEMYTURN, PUNISHMENT, RESET, WON, LOST }

public class TurnManager : MonoBehaviour {
    [Header("Decks")]
    public Deck FullDeck;
    public List<GameObject> FullDeckCopy;

    [Header("Cards")]
    public List<Transform> PlayerTransforms;
    public List<Transform> enemyTransforms;
    public UnityEvent UpdateCards;

    public bool PlayerTurnPass = false;
    public bool EnemyTurnPass = false;

    private GameObject PlayerObject;
    private GameObject EnemyObject;

    public GameStates State;

    void Start() {
        State = GameStates.START;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        EnemyObject = GameObject.FindGameObjectWithTag("Enemy");

        FullDeckCopy = FullDeck.CardDeck.ToList();

        PlayerObject.GetComponent<Player>().FirstCardDraw(GetRandomAndRemove(FullDeckCopy));
        PlayerObject.GetComponent<Player>().DrawCard(GetRandomAndRemove(FullDeckCopy));

        EnemyObject.GetComponent<Enemy>().FirstCardDraw(GetRandomAndRemove(FullDeckCopy));
        EnemyObject.GetComponent<Enemy>().DrawCard(GetRandomAndRemove(FullDeckCopy));

        yield return new WaitForSeconds(2f);

        State = GameStates.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator ResetRound() {
        foreach (var CardObject in GameObject.FindGameObjectsWithTag("Card")) CardObject.GetComponent<Card>().StartBurning();

        PlayerObject.GetComponent<Player>().ClearDeck();
        EnemyObject.GetComponent<Enemy>().ClearDeck();
        FullDeckCopy = FullDeck.CardDeck.ToList();

        PlayerTurnPass = false;
        EnemyTurnPass = false;

        PlayerObject.GetComponent<Player>().FirstCardDraw(GetRandomAndRemove(FullDeckCopy));
        PlayerObject.GetComponent<Player>().DrawCard(GetRandomAndRemove(FullDeckCopy));

        EnemyObject.GetComponent<Enemy>().FirstCardDraw(GetRandomAndRemove(FullDeckCopy));
        EnemyObject.GetComponent<Enemy>().DrawCard(GetRandomAndRemove(FullDeckCopy));

        yield return new WaitForSeconds(2f);

        State = GameStates.PLAYERTURN;
        PlayerTurn();
    }

    public void PlayerTurn() {
        PlayerObject.GetComponent<Player>().SetTurn(true);
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

    public void PlayerPass() {
        EnemyObject.GetComponent<Enemy>().SetTurn(true);
        PlayerTurnPass = true;
        if (PlayerTurnPass && EnemyTurnPass) {
            State = GameStates.PUNISHMENT;
            Punishment();
        } else {
            State = GameStates.ENEMYTURN;

        }
    }

    public void EnemyDraw() {
        EnemyObject.GetComponent<Enemy>().DrawCard(GetRandomAndRemove(FullDeckCopy));
    }

    public void EnemyPass() {
        PlayerObject.GetComponent<Player>().SetTurn(true);
        EnemyTurnPass = true;
        if (PlayerTurnPass && EnemyTurnPass) {
            State = GameStates.PUNISHMENT;
            Punishment();
        } else { 
            State = GameStates.PLAYERTURN;
        }
    }

    public void Punishment() {
        Debug.Log("Procced Punishment");
        PlayerObject.GetComponent<Player>().SetTurn(false);
        PlayerObject.GetComponent<Player>().SetTurn(false);

        var PlayerCardVals = PlayerObject.GetComponent<Player>().GetCardValues();
        var EnemyCardVals = EnemyObject.GetComponent<Enemy>().GetCardValues();

        // If both players have same card values, punish both if over 21 if not no punishments
        if (PlayerCardVals == EnemyCardVals) {
            if (PlayerCardVals > 21) {
                PlayerObject.GetComponent<Health>().TakeDamage(1);
                EnemyObject.GetComponent<Health>().TakeDamage(1);
                Debug.Log("Both punished");
            }
            Debug.Log("Neither punished");
            State = GameStates.RESET;
            StartCoroutine(ResetRound());
            return;
        }

        // If both are over 21, punish the highest value
        if (PlayerCardVals > 21 && EnemyCardVals > 21) { 
            if (PlayerCardVals > EnemyCardVals) PlayerObject.GetComponent<Health>().TakeDamage(1);
            else EnemyObject.GetComponent<Health>().TakeDamage(1);
            Debug.Log("Both over 21");
            State = GameStates.RESET;
            StartCoroutine(ResetRound());
            return;
        }

        // If both are under 21, punish the lowest value
        if (PlayerCardVals < 21 && EnemyCardVals < 21) {
            if (PlayerCardVals > EnemyCardVals) EnemyObject.GetComponent<Health>().TakeDamage(1);
            else PlayerObject.GetComponent<Health>().TakeDamage(1);
            Debug.Log("Neither over 21");
            State = GameStates.RESET;
            StartCoroutine(ResetRound());
            return;
        }

        // If player is over 21 and enemy isn't, punish player
        if (PlayerCardVals > 21 && EnemyCardVals <= 21) {
            Debug.Log("Player punished");
            PlayerObject.GetComponent<Health>().TakeDamage(1);
            State = GameStates.RESET;
            StartCoroutine(ResetRound());
            return;
        }

        // Vice versa
        if (EnemyCardVals > 21 && PlayerCardVals <= 21) EnemyObject.GetComponent<Health>().TakeDamage(1);
        State = GameStates.RESET;
        StartCoroutine(ResetRound());
    }
}