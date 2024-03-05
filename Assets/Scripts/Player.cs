using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public byte Lives { get; private set; }
    public List<Card> Cards { get; private set; }
    public byte Bet { get; private set; }

    private Health PlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = GetComponent<Health>();
        Lives = 6;
        Cards = new List<Card>();
        Bet = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
