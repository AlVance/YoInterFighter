using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterPickSO : ScriptableObject
{
    public string characterName;
    public string characterSiglas;
    public Sprite characterSprite;
    public Sprite siluetaSprite;
}
