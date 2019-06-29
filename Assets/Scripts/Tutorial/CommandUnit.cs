using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUnit : TutorialCommand
{
	public string nameOfSpawnObj;
	public Transform targetPos;
	public Transform spawnPos;


    public override void Excute()
	{
		CommandManager.instance.SwitchStats(CommandStates.diabled);
		GameObject unitClone = PoolManager.instance.Spawn(nameOfSpawnObj, spawnPos.position);
		unitClone.GetComponent<BaseUnit>().dir = (targetPos.position - spawnPos.position).normalized;
		//unitClone.GetComponent<BaseUnit>().speed = 10;
	}
}
