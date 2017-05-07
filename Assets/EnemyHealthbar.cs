using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    Quaternion rotation;
    void Awake ()
    {
        //  Get the initial rotation of the healthbar.
        rotation = transform.rotation;
    }

    void LateUpdate()
    {
        //  Set the rotation of the healthbar to it's original rotation to prevent rotating with parent object.
        transform.rotation = rotation;
    }
}
