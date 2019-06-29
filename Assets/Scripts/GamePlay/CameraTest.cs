using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举
public enum ClearFlags
{
	Skybox,
	SolidColor,
	DepthOnly
}

public enum KeyCodes {
	A,
	B,
	C,
	D,
	E,
	F
}


public class CameraTest : MonoBehaviour {
	//Static为静态变量
	public static CameraTest main;
	public static Color jimmyColor;


	//internal变量不能在inspector窗口看到，其余跟public一样，我们叫公有变量。
	//protected 变量只能在自身类中和子类中调用，不能在其他类（脚本）中调用。
	//private 变量只能在自身类中调用。任何其他类中不能获取。
	internal Color yourColor;
	public Color myColor;
	public ClearFlags clearFlags;
	public float size;
	private int myNum;
	protected int yourNum;
	internal float y;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
