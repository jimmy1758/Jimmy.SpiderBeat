using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> 
{
	public static T instance;

	protected virtual void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarningFormat("{0} Singleton already exists in the scene! Destroying this version...", this.name);
			Destroy(transform.root.gameObject);
			return;
		}
		instance = (T)this;
	}

}
