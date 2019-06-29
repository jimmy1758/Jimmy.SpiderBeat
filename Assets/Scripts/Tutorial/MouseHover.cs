using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool mouseHover = false;

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseHover = false;
	}
}
