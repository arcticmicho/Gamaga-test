using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyCharacterData : CharacterData
{
    int ProjectileDamage { get; }

    float ProjectileSpeed { get; }

    float FireRate { get; }

    float FireDistance { get; }

    ProjectileEntity ProjectilePrefab { get; }

    
}
