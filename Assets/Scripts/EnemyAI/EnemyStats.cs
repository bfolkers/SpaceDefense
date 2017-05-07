using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header("General Attributes")]
    public float moveSpeed = 1f;                // Enemy movement speed
    public float lookRange = 40f;               // Enemy look range
    public float lookSphereCastRadius = 1f;     // Enemy FOV sphere cast radius

    [Header("Attack Attributes")]
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 10;
}
