using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character Data Model.
/// Here we can put the all the data related to a generic Character. 
/// On this way it doesn't matter what kind of implementation we use, the game should always use this interfaces to manage the Data of the Character.
/// </summary>
public interface CharacterData
{
    int InitialHP { get; }
    float MovementSpeed { get; }
    float JumpForce { get; }
    CharacterEntity Entity {get;}

}
