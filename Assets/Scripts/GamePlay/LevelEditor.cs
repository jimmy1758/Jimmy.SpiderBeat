using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;

public class LevelEditor : MonoSingleton<LevelEditor> {


   [System.Serializable]
	public struct AllBeatsOfLevel
	{
		[Tooltip("The actual timing when the unit arrives its target")] public List<float> beatsTimingList;
		[Tooltip("The actual timing when the unit spwans")] public List<float> spawnTimingList;
		[Tooltip("Which target the unit will arrive")]public List<int> targetsNumList;
		[Tooltip("How fast the unit will move")]public List<float> beatsSpeed;
		private List<string> beatList;
		//计算每个unit生成的时间
		public void GetSpawnTiming()
		{
			for (int i = 0; i < beatsSpeed.Count; i++)
			{

				//print(GameManager.instance.targetObj[targetsNumList[i]].transform.position);

				float t = (GameManager.instance.targetsObj[targetsNumList[i]].transform.position - GameManager.instance.startPos.position).magnitude / beatsSpeed[i];
				//spawnTimingList[i] = beatsTimingList[i] - t;
				spawnTimingList.Add(beatsTimingList[i] - t);
			}
		}

		//从Xml文件解析关卡
		public void ParseXML(string musicName, string difficulty)
		{
            Type t = Type.GetType(musicName + difficulty + "Data");
            System.Object obj = Activator.CreateInstance(t);

            MethodInfo method = t.GetMethod("GetBeatsData");
            method.Invoke(obj, null);


            beatsTimingList = new List<float>();
			spawnTimingList = new List<float>();
			targetsNumList = new List<int>();
			beatsSpeed = new List<float>();
			beatList = new List<string>();

			XmlDocument xmlDoc = new XmlDocument();
			print("SongBeatsXML / " + musicName + "_" + difficulty);
			string data = Resources.Load("SongBeatsXML/" + musicName + "_" + difficulty).ToString();
			xmlDoc.LoadXml(data);

			XmlNodeList xmlNodeList = xmlDoc.SelectSingleNode("beats").ChildNodes;
			foreach(XmlNode xmlnode in xmlNodeList)
			{
				XmlElement xmlElement = (XmlElement)xmlnode;
				beatList.Add(xmlElement.ChildNodes.Item(0).InnerText + "," + xmlElement.ChildNodes.Item(1).InnerText + "," + xmlElement.ChildNodes.Item(2).InnerText);
			}
			for(int i=0; i<beatList.Count; i++)
			{
				//以char ‘,’ 分割每个beat信息中的beatTiming，targetNum, beatSpeed
				string[] beatsDetail = beatList[i].Split(',');
				beatsTimingList.Add(float.Parse(beatsDetail[0]));
				targetsNumList.Add(int.Parse(beatsDetail[1]));
				beatsSpeed.Add(float.Parse(beatsDetail[2]));
			}
			GetSpawnTiming();
		}
	}
	public AllBeatsOfLevel beats;


	// Use this for initialization
	void Start ()
    {
		
	}

	// Update is called once per frame
	void Update ()
    {
		
	}
}
