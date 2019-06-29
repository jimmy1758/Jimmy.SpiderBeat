using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUnit : BaseUnit {

	protected override void DestroyGameObj()
	{
		base.DestroyGameObj();
		CommandManager.instance.SwitchStats(CommandStates.interactable);
		GameManager.instance.tutorialText.text = "Try again!";
	}

	protected override IEnumerator DeathRoutin()
	{
		anim.SetBool("isDead", true);
		yield return new WaitForSeconds(0.2f);
		PoolManager.instance.Despawn(gameObject);
		CancelInvoke();
	}
}
