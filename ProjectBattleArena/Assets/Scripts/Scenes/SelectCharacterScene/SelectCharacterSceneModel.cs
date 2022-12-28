using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterSceneModel
{
    public GameObject SelectCharacter { get; set; }

    public List<CharacterSlot> Slots { get; set; } = new List<CharacterSlot>();
}