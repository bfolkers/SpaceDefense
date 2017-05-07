using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act (StateController controller)
    {
        Patrol (controller);
    }

    private void Patrol (StateController controller)
    {
        controller.navMeshAgent.destination = controller.playerBase.position;   // Set destination to player's base
        controller.navMeshAgent.isStopped = false;
    }
}
