using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDelay : TutorialCommand
{
    public float delayTime;
	public override void Excute()
	{
        base.Excute();
        StartCoroutine(StartDelay());
	}

    private IEnumerator StartDelay()
    {
        CommandManager.instance.SwitchStats(CommandStates.diabled);
        yield return new WaitForSeconds(delayTime);
        CommandManager.instance.SwitchStats(CommandStates.interactable);
    }
}
