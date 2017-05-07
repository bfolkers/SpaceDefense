using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

	[Header("Attributes")]
	public Transform playerBase;		          //  Player's Energy Core
	public Transform currentTarget;		        //  Enemy's current target
 	public Transform eyes;                    //  Position of enemy's view
  public float speed = 10f;			            //  Enemy movement speed
  public float lookRange = 2f;              //  Distance enemy can see
  public float lookSphereCastRadius = 2f;   //  Radius of enemy's field-of-view
	EnemyHealth enemyHealth;
	NavMeshAgent agent;
	Animator anim;
	TowerHealth baseHealth;

  float timer;

  bool isSlowed;
  float timerDuration;
	void Awake ()
	{
		//Setup References
		playerBase = GameObject.FindGameObjectWithTag ("PlayerBase").transform;
		baseHealth = playerBase.GetComponent<TowerHealth> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

    agent.speed = speed;
    InvokeRepeating("Look", 0f, 0.5f);
	}

	void Update ()
	{
      if (enemyHealth.currentHealth <= 0f)
          return;
          
      if (isSlowed)
      {
          if (timer >= timerDuration)
          {
              isSlowed = false;
              agent.speed = speed;
          } else
          {
              timer += Time.deltaTime;
          }
      }


	    if (currentTarget == null) 							// If no target,
	    {
          Look ();                            // Look for nearby enemies or...
          if (currentTarget == null)
              MoveToBase ();                  // Go to player's base.
              return;
	    }

		if (baseHealth.getCurrentHealth () <= 0f)
			agent.enabled = false;
	}

  public void Look ()
  {
        RaycastHit hit;

        if (currentTarget != null)
        {
            Debug.DrawRay (transform.position, currentTarget.transform.forward.normalized * lookRange, Color.cyan);
            // Use SphereCast to determine if a player is within field-of-view
			if (Physics.SphereCast (transform.position, lookSphereCastRadius, transform.forward, out hit, lookRange)
			             && hit.collider.CompareTag ("Player")) {
				currentTarget = hit.transform;
			} else {
				currentTarget = playerBase;
			}
        }
  }

  public void Slow (float slowRate, float slowDuration)
  {
      isSlowed = true;
      timer = 0;
      timerDuration = slowDuration;

      agent.speed = speed * slowRate;
  }

  void MoveToBase ()
  {
        currentTarget = playerBase;
        anim.SetBool ("IsRunning", true);
        transform.LookAt(currentTarget);
        agent.SetDestination (currentTarget.position);
        agent.isStopped = false;
  }
	public Transform getCurrentTarget()
	{
		return currentTarget;
	}

  void OnDrawGizmos()
  {
      Gizmos.color = Color.yellow;
      if (eyes != null)
      {
          Gizmos.DrawWireSphere(eyes.transform.position, lookSphereCastRadius);
          Gizmos.DrawRay(eyes.transform.position, eyes.transform.forward.normalized * lookRange);
      }
  }
}