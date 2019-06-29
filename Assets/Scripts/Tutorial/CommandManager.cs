using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CommandStates
{
	interactable,
	diabled
}
public class CommandManager : MonoSingleton<CommandManager> {

	private TutorialCommand[] commandList;
	public int currentIndex = 0;
	public CommandStates cmdStates = CommandStates.interactable;

	protected override void Awake()
	{
        base.Awake();
		commandList = GetComponentsInChildren<TutorialCommand>();
	}

	public void SwitchStats(CommandStates newState)
	{
		cmdStates = newState;
	}

	void MoveNext()
	{
		if(commandList == null || commandList.Length <= 0)
		{
			return;
		}
		currentIndex++;

        if (currentIndex >= commandList.Length)
        {
            StopTutorial();
        }
    }

	void ExcuteNext()
	{
		if (commandList == null || commandList.Length <= 0)
		{
			return;
		}
		if(currentIndex < 0 || currentIndex >= commandList.Length)
		{
			return;
		}
        //第一个指令自动运行

        if (currentIndex == 0)
        {
                commandList[0].Excute();
                MoveNext();
        }

        if(commandList[currentIndex].cmdType==CommandType.Automatically)
        {
            commandList[currentIndex].Excute();
            MoveNext();
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                commandList[currentIndex].Excute();
                MoveNext();
            }
        }

	}

    
	private void Update()
	{
		switch (cmdStates)
		{
			case CommandStates.interactable:
				ExcuteNext();
				break;
			case CommandStates.diabled:
				break;
		}
		
	}

	/// <summary>
	/// 用来倒退指令
	/// </summary>
	/// <param name="i">倒退的步数</param>
	public void RetralCmd(int i)
	{
		currentIndex -= i;
        Invoke("Tmp", 1.5f);
	}

    private void Tmp()
    {
        cmdStates = CommandStates.interactable;
    }


	void StopTutorial()
	{
        TutorialManager._instance.endNotice.SetActive(true);
    }
}
