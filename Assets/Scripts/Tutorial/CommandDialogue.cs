using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDialogue : TutorialCommand
{
	public string content;
	public Text dialogueText;

	public override void Excute()
	{
        base.Excute();
        dialogueText.text = content;
	}
}
