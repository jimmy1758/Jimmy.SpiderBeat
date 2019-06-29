using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType
{
	Manually,
	Automatically
}

public class TutorialCommand : MonoBehaviour {

    public int cmd_INDEX;

	public virtual void Excute()
    {
        cmd_INDEX = CommandManager.instance.currentIndex;
    }

	public CommandType cmdType;
}
