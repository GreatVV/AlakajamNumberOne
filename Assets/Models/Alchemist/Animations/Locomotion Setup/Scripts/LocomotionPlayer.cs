 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
  


public class LocomotionPlayer : MonoBehaviour {

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private Locomotion locomotion = null;

	// Use this for initialization
	void Start () 
	{
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
	}
    
	void Update () 
	{
        if (animator && Camera.main)
		{
            JoystickToEvents.Do(transform,Camera.main.transform, ref speed, ref direction);
            locomotion.Do(speed * 6, direction * 180);
		}		
	}
}
