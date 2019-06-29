using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoSingleton<PoolManager>
{
	//存储所有需要重复生成的prefab的文件夹名称
	public static string folderPath = "PoolObjs";

	//dictionary就是key指向value的list。
	//为每个物体创建一个stack储存所有clone,以物体名称为key指向对应stack
	private Dictionary<string, Stack<GameObject>> nameToObjects = new Dictionary<string, Stack<GameObject>>();
	protected override void Awake()
	{
		base.Awake();
		//从resource文件夹下载所有poolobj的prefab去重复生产clone
		GameObject[] resources = Resources.LoadAll<GameObject>(folderPath);
		//每个unit的prefab创建stack存储在dictionary
		foreach (GameObject objPrefab in resources)
		{
			Stack<GameObject> objStack = new Stack<GameObject>();
			objStack.Push(objPrefab);//将一个元素加到stack顶部
			nameToObjects.Add(objPrefab.name, objStack);//将物体prefab name与对应的stack作为一组元素存储在dictionary
		}
	}

	//Initialization
	public void Init()
	{
		BaseUnit[] objs = FindObjectsOfType<BaseUnit>();
		foreach (var obj in objs)
		{
			Destroy(obj.gameObject);
		}
		nameToObjects = new Dictionary<string, Stack<GameObject>>();
		GameObject[] resources = Resources.LoadAll<GameObject>(folderPath);
		foreach (GameObject objPrefab in resources)
		{
			Stack<GameObject> objStack = new Stack<GameObject>();
			objStack.Push(objPrefab);
			nameToObjects.Add(objPrefab.name, objStack);
		}
	}

	public GameObject Spawn(string name, Vector3 position)
	{
		GameObject obj = Spawn(name);
		obj.transform.localPosition = position;
		return obj;
	}


	public GameObject Spawn(string name)
	{
		Stack<GameObject> objStack = nameToObjects[name];
		//stack只有一个母体，形成一个clone
		if (objStack.Count == 1)
		{
			GameObject objClone = Instantiate(objStack.Peek());
			objClone.name = name;
			return objClone;
		}
		//stack不止一个物体，用pop不再产生stack
		GameObject topObject = objStack.Pop();
		topObject.SetActive(true);
		return topObject;
	}
	//让物体消失
	public void Despawn(GameObject obj)
	{
		obj.SetActive(false);
		Stack<GameObject> objStack = nameToObjects[obj.name];
		objStack.Push(obj);
	}

}



