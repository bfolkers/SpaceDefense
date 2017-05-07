using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Enemy;

public class StateController : MonoBehaviour
{
    public EnemyStats enemyStats;   //  Enemy Stats
    public Transform eyes;          //  Where the enemy is looking from
    public State currentState;      //  Enemy's Current State
    public State remainState;

    [HideInInspector] public NavMeshAgent navMeshAgent;       //  Enemy navMeshAgent
    [HideInInspector] public Enemy.EnemyAttack enemyAttack;   //  Enemy attack script
    [HideInInspector] public Transform playerBase;            //  Player's base location
    [HideInInspector] public Transform chaseTarget;           //  Enemy's target to chase
    private bool aiActive;
    
    void Awake()
    {
        // playerBase = GameObject.FindGameObjectWithTag ("PlayerBase").transform;
        navMeshAgent = GetComponent<NavMeshAgent> ();
        enemyAttack = GetComponent<EnemyAttack> ();
    }

    // Initialize AI from EnemyManager with destination to the player's base
    public void SetupAI (bool aiActivationFromEnemyManager, Transform waypointToPlayerBase)
    {
        playerBase = waypointToPlayerBase;
        aiActive = aiActivationFromEnemyManager;
        if (aiActive)
        {
            navMeshAgent.enabled = true;
        } else
        {
          navMeshAgent.enabled = false;
        }
    }

    // If AI is active, update current state
    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState (this);
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            // Enemy's view radius
            Gizmos.color = currentState.gizmoColor;
            Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
        }
        if (chaseTarget != null)
            Gizmos.color = currentState.gizmoColor;
    }
}
