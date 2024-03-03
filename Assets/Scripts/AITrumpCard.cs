using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrumpCard", menuName = "TrumpCard/AI", order = 2)]
public class AITrumpCard : ScriptableObject {
    public string Name;
    public string Description;

    public Sprite CardIcon;
}