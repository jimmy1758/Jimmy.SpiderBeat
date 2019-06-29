using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommandGOFade : TutorialCommand
{
    public bool selfActive;
    public float duration;
    public bool includeChild;
    public List<GameObject> gos;

    public override void Excute()
    {
        base.Excute();
        for (int i = 0; i < gos.Count ; i++)
        {
            SpriteRenderer[] sprites = gos[i].GetComponentsInChildren<SpriteRenderer>();
            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j].DOFade(selfActive ? 1 : 0, duration);
                if (!includeChild)
                    break;
            }
        }
    }
}

