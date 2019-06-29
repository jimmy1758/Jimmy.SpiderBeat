using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CommandUIFade : TutorialCommand
{
    public bool selfActive;
    public float duration;
	public GameObject[] UIElements;

	public override void Excute()
	{
        base.Excute();
        for (int i = 0; i < UIElements.Length; i++)
        {
            Image[] Images = UIElements[i].GetComponentsInChildren<Image>();
            for (int j = 0; j < Images.Length; j++)
            {
                Images[j].DOFade(selfActive ? 1 : 0, duration);
            }
            Text[] txts = UIElements[i].GetComponentsInChildren<Text>();
            for (int k = 0; k < txts.Length; k++)
            {
                txts[k].DOFade(selfActive ? 1 : 0, duration);
            }
        }
    }
}
