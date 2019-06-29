using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCreateBeat : TutorialCommand {

    public override void Excute()
    {
        base.Excute();
        StartCoroutine(TutorialManager._instance.CreateTutorialBeat());
    }
}
