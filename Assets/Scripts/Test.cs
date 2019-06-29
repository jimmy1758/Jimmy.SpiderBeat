using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	int y = 3;
	private int x;
	public int[] nums;
	Camera cam;
	//在inspector窗口只能拖具有相应组件的物体进去。
	public GameObject camObj;
	public GameObject newObj;
	private Color mycolor;
	//awake函数更早于Start执行
	private void Awake()
	{
		Test22();
	}

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	// Use this for initialization
	void Start ()
	{
		//用Getcomponent获取脚本，根据脚本获取相应变量
		//mycolor = newObj.GetComponent<CameraTest>().yourColor;
		//print(mycolor);

		if (y == 3)
		{

		}else if(y == 4)
		{

		}
		else
		{

		}

		while (y < 3)
		{

		}

		for (int i=0; i<8; i++)
		{

		}

		switch (y)
		{
			case 3:
				y = 4;
				break;
			case 4:
				y = 5;
				break;
		}

		foreach (int i in nums)
		{
			if(i == 0)
			{
				//print("1");
			}
		}

		Invoke("Test33", 3);
		//Coroutine单词意思为协程
		StartCoroutine(RepeatJimmy());

	}
	
	// Update is called once per frame
	//update每大约0.02s执行一次，
	void Update () {
		
	}

	//fixedUpdate精确0.02s执行一次，跟物理系统有关的处理放在fixedupdate中
	private void FixedUpdate()
	{

	}

	private void OnMouseDown()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}

	void Test22()
	{
		int i = 0;
		print(i);
		//代码的原则： DRY: dont repeat yourself
		//WET: write everything twice
	}

	void Test11()
	{
		y += 1;
		print(y);
	}

	void Test33()
	{
		//print("Jimmy");
	}

	//协程的声明方法
	IEnumerator RepeatJimmy()
	{
		print("2222");

		while (true)
		{
			yield return new WaitForFixedUpdate();
			//print("1");
		}		
	}

}