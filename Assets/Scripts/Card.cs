using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private int Value;
    [SerializeField] private bool IsFaceDown = false;
    [SerializeField] private bool IsDead = false;
    [SerializeField] private Sprite FaceDownSprite;
    [SerializeField] private Sprite FaceUpSprite;

    private SpriteRenderer Renderer;
    private Animator Anim;

    void Start() {
        Renderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
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

    public void StartBurning() {
        Anim.SetTrigger("Discard");
    }

    public void Burn() {
        Destroy(gameObject);
    }
}