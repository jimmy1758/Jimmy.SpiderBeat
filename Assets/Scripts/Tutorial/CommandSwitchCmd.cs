using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSwitchCmd : TutorialCommand
{
	public CommandStates cmdStats;

	public override void Excute()
	{
        base.Excute();
        CommandManager.instance.SwitchStats(cmdStats);
	}
}
