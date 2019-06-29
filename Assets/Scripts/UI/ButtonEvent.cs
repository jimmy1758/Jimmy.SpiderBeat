using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEffectManager.instance.AddWorldEffect("CFX_BtnClick", ScreenToWorld(eventData.pressPosition) + new Vector3(0, 0, 1f), 3, 1f);
    }

    private Vector3 ScreenToWorld(Vector3 screenPos)
    {
        Vector3 res = Camera.main.ScreenToWorldPoint(screenPos);
        return res;
    }
}
