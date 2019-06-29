using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour {

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    public virtual void OnScreenEnter()
	{
		this.gameObject.SetActive(true);
	}

	public virtual void OnScreenQuit()
	{
		this.gameObject.SetActive(false);
	}

}
