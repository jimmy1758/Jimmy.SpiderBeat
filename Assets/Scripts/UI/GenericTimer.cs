using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenericTimer : MonoBehaviour {

	public float timer = 3.0f;

	private float currentTime;
	private Text timerText;

	public UnityEvent OnTimeOut = new UnityEvent();

	private void Start()
	{
		timerText = GetComponent<Text>();
	}

	public void StartTimer()
	{
		this.gameObject.SetActive(true);
		StartCoroutine(TimerFunction());
	}

	private IEnumerator TimerFunction()
	{
		currentTime = timer;
		while (currentTime > 0)
		{
			yield return null;
			currentTime -= Time.deltaTime;
			timerText.text = ((int)currentTime).ToString();
		}
		OnTimeOut.Invoke();
	}
}
