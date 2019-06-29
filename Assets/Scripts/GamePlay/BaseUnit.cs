using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{

	public float speed = 5000;
	internal Vector3 dir;

	protected Animator anim;

	public Action OnEnter;
	public Action OnExit;
	public Action OnDetroy;

	protected virtual void Start()
	{
		anim = GetComponent<Animator>();
		OnExit += delegate
		{
			Invoke("DestroyGameObj", 1.0f);
		};
	}

	protected void Update ()
    {
		if (!GameManager.instance.gamePaused)
		{
			transform.Translate(dir * Time.deltaTime * speed);
		}	
	}


	protected virtual void DestroyGameObj()
	{
		PoolManager.instance.Despawn(gameObject);
		CancelInvoke();
	}


	public void PlayDeathAnim()
	{
		StartCoroutine("DeathRoutin");
	}

	protected virtual IEnumerator DeathRoutin()
	{
		anim.SetBool("isDead", true);
		yield return new WaitForSeconds(0.2f);
		DestroyGameObj();
	}
}
