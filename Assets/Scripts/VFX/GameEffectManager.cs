using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEffectManager : MonoSingleton<GameEffectManager>
{

	Dictionary<string, List<GameEffect>> effectPool = new Dictionary<string, List<GameEffect>>();


	//Initialization
	public void Init()
	{
		foreach (var pool in effectPool)
		{
			List<GameEffect> effects = pool.Value;
			foreach(var effect in effects)
			{
				if (effect.gameObject.activeSelf)
				{
					effect.gameObject.SetActive(false);
				}
			}
		}
	}

	/// <summary>
	/// Add static effect in somewhere of the scene
	/// </summary>
	/// <param name="effectName"></param>
	/// <param name="pos"></param>
	/// <param name="scale"></param>
	/// <param name="time"></param>
	/// <returns></returns>
	public GameEffect AddWorldEffect(string effectName, Vector3 pos, float scale, float time)
	{
		GameEffect ge = GetEffect(effectName);
		if (ge == null) return null;
		ge.transform.position = pos;
		ge.Play(scale, time);
		return ge;
	}

	/// <summary>
	/// Adds effects and sets a parent object that it will follow.
	/// </summary>
	/// <param name="effectName"> The name of this effect</param>
	/// <param name="obj"></param>
	/// <param name="pos"></param>
	/// <param name="scale"></param>
	/// <param name="time"></param>
	/// <returns></returns>
	public GameEffect AddEffect(string effectName, GameObject obj, Vector3 pos, float scale, float time)
	{
		if (obj == null)
			AddWorldEffect(effectName, pos, scale, time);

		GameEffect ge = GetEffect(effectName);
		if (ge == null)
			return null;

		ge.SetParent(obj);
		ge.Play(scale, time);
		return ge;
	}

	GameEffect GetEffect(string effectName)
	{
		GameEffect ret = null;
		//假如已经生成过同名特效
		if (effectPool.ContainsKey(effectName))
		{
			List<GameEffect> pool = effectPool[effectName];
			for (int i = 0; i < pool.Count; i++)
			{
				GameEffect eff = pool[i];


				if (eff)
				{
					if (eff.gameObject.activeSelf)
					{
						continue;
					}
				}
				ret = eff;
				break;
			}
		}

		//没有生成过同名特效
		if (ret == null)
		{
			GameEffect eff = CreateEffect(effectName);
			if (eff != null) ret = eff;
		}

		return ret;
	}



	public GameEffect CreateEffect(string effectName)
	{
		GameObject obj = Resources.Load<GameObject>("Effects/" + effectName);
		if (obj == null)
		{
			Debug.LogError("can't find file ! :" + effectName);
		}
		GameObject effectObj = Instantiate(obj);
		GameEffect ge = effectObj.AddComponent<GameEffect>();
		ge.Init(effectName);
		if (!effectPool.ContainsKey(effectName))
		{
			effectPool.Add(effectName, new List<GameEffect>());
		}
		effectPool[effectName].Add(ge);
		return ge;
	}
}
