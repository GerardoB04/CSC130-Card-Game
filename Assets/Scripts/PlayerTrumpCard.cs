using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTrumpCard", menuName = "TrumpCard/Player", order = 1)]
public class PlayerTrumpCard : ScriptableObject {
    public string Name;
    public string Description;

    public Sprite CardIcon;
}