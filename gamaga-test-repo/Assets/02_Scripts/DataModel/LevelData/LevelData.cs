using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LevelData
{
    List<EnemyCharacterDataImpl> Enemies { get; }

    LevelEntity LevelEntity { get; }

	
}
