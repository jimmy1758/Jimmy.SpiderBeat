using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
	public MainMenu mainMenu;
	public HUD hUD;
	public GameEnd gameEndScreen;

	private Dictionary<Type, UIScreen> typeToScreen = new Dictionary<Type, UIScreen>();
	private Stack<UIScreen> screenStack = new Stack<UIScreen>();


	protected override void Awake()
	{
		base.Awake();
		UIScreen[] screens = GetComponentsInChildren<UIScreen>(true);
		foreach (UIScreen screen in screens)
		{
			screen.gameObject.SetActive(false);

			typeToScreen.Add(screen.GetType(), screen);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		Push(typeof(MainMenu));

	}

	public T Push<T>() where T : UIScreen
	{
		UIScreen newScreen = Push(typeof(T));
		return (T)newScreen;
	}

	public UIScreen Push(Type screenType)
	{
		if (screenStack.Count > 0)
		{
			foreach (UIScreen screen in screenStack)
			{
				if (screen.gameObject.activeSelf)
				{
					//screen.gameObject.SetActive(false);
					screen.OnScreenQuit();
				}
			}
		}

		UIScreen newScreen = typeToScreen[screenType];
		//newScreen.gameObject.SetActive(true);

		newScreen.OnScreenEnter();
		screenStack.Push(newScreen);
		return newScreen;
	}

	public void Pop()
	{
		UIScreen topScreen = screenStack.Pop();
		//topScreen.gameObject.SetActive(false);
		topScreen.OnScreenQuit();

		//screenStack.Peek().gameObject.SetActive(true);
		screenStack.Peek().OnScreenEnter();
	}
}

