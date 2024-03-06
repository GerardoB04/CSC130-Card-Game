using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private int Value;
    [SerializeField] private bool IsFaceDown = false;
    [SerializeField] private bool IsDead = false;
    [SerializeField] private Sprite FaceDownSprite;
    [SerializeField] private Sprite FaceUpSprite;

    private SpriteRenderer Renderer;

    void Start() {
        Renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (IsFaceDown) Renderer.sprite = FaceDownSprite;
        else Renderer.sprite = FaceUpSprite;
    }

    public void TurnOver() {
        if (!IsFaceDown) IsFaceDown = true;
    }

    public void TurnUp() {
        IsFaceDown = false;
    }
}