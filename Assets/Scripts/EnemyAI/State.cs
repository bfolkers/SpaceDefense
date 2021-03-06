﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Color gizmoColor = Color.grey;

    public void UpdateState (StateController controller)
    {
        DoActions (controller);
    }

    // Interate through all actions
    private void DoActions (StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act (controller);
        }
    }
}
