using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
	public bool touched;
	protected bool firstClick = true;
    protected GameObject touchedObj;
    protected SpriteRenderer sr;

    public Color normalColor = Color.white;
    public Color missingColor = Color.red;
    public Color clickColor = Color.green;
    public Color noticeColor = Color.yellow;

    private void Start()
    {
        sr = transform.Find("Circle").GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
        sr.DOColor(noticeColor, 0.2f);
		if(collision.gameObject.GetComponent<BaseUnit>() != null)
		{
			touched = true;
			touchedObj = collision.gameObject;
            EventManager.instance.Dispatch(EventConst.EVENT_ENTER, null);
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<BaseUnit>() != null)
		{
			if (touched)
			{
				touchedObj.GetComponent<BaseUnit>().OnExit();
			}
			ResetTarget();
			//如果当一个Unit的退出trigger是因为miss，而不是玩家点击，则判定为miss，执行miss时的函数
			if (collision.gameObject.activeSelf)
			{
                sr.DOColor(missingColor, 0.5f).onComplete = () => sr.DOColor(normalColor, 0.5f);
                sr.transform.DOShakePosition(0.5f);
                EventManager.instance.Dispatch(EventConst.EVENT_MISS, 5);
                AudioManager.instance.PlaySFX(SFXAudio.SFX_Miss);
			}
		}
	}

	protected virtual void OnMouseDown()
	{
		if(touched)
		{
            //正确节拍的反馈
            sr.DOColor(clickColor, 0.5f).onComplete = () => sr.DOColor(normalColor, 0.5f);
            sr.transform.DOShakeScale(1f, 5f, 10, 0);
            //点中Unit开始计算combo计数
            //GameManager.instance.ComboNumIncrease();

			// Vector3.Distance 计算两点之间距离
			float distance = Vector3.Distance(touchedObj.transform.position, gameObject.transform.position);
            string evaluation = GetEvaluation(distance);

            //事件机制传递该次点击的评价，位置等
            EventManager.instance.Dispatch(EventConst.EVENT_CLICK, evaluation, transform.position);
            AudioManager.instance.PlaySFX(SFXAudio.SFX_ClickRight);

			//播放死亡动画
			touchedObj.GetComponent<BaseUnit>().PlayDeathAnim();
			ResetTarget();
		}
		else
		{
            GameManager.instance.ComboReset(null);
		}	
	}

	/// <summary>
	/// 当miss一个Unit时需要重置该target的状态
	/// </summary>
	protected void ResetTarget()
	{
		touched = false;
		touchedObj = null;
		firstClick = true;
	}

	/// <summary>
	/// 根据Unit与target之间的距离获得一次点击的评价。
	/// </summary>
	/// <param name="dis"></param>
	/// <returns></returns>
	private string GetEvaluation(float dis)
	{
		string evaluation;
        if (dis < GetComponent<CircleCollider2D>().radius * 2 / 3)
		{
			evaluation = "Perfect";
		}
		else if(dis < GetComponent<CircleCollider2D>().radius * 4 / 3)
		{
			evaluation = "Great";
		}
		else
		{
			evaluation = "Good";
		}
		return evaluation;
	}


}
