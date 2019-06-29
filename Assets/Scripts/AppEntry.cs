using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AppEntry : MonoBehaviour {

    private Transform bgTrans;
    private Button btn_tutorial;
    private Button btn_Start;
    private Button btn_No;

    private Shadow shadow1;
    private Shadow shadow2;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        Init();
        Tweener tweener = bgTrans.DOLocalMoveY((bgTrans as RectTransform).rect.height - 120.0f, 10.0f);
        tweener.SetLoops(-1, LoopType.Restart);

        Tweener shadow1Tween = shadow1.transform.DOShakePosition(5, 10);
        shadow1Tween.SetLoops(-1, LoopType.Yoyo);
        Tweener shadow2Tween = shadow2.transform.DOShakePosition(5, 10);
        shadow2Tween.SetLoops(-1, LoopType.Yoyo);
    }

    private void Init()
    {
        bgTrans = transform.Find("BG");
        btn_tutorial = transform.Find("Panel_ButtonList/Button_Tutorial").GetComponent<Button>();
        btn_Start = transform.Find("Panel_ButtonList/Button_Start").GetComponent<Button>();
        btn_No = transform.Find("Panel_ButtonList/Button_No").GetComponent<Button>();
        shadow1 = transform.Find("GameName/Text_Spider").GetComponent<Shadow>();
        shadow2 = transform.Find("GameName/Text_Beat").GetComponent<Shadow>();

        //AddListener
        btn_tutorial.onClick.AddListener(OnTutorialButton);
        btn_Start.onClick.AddListener(OnStartButton);
        btn_No.onClick.AddListener(OnStartButton);

        //FirstOpenApp?
        btn_tutorial.gameObject.SetActive(PlayerModel.GetFirstOpenApp());
        btn_No.gameObject.SetActive(PlayerModel.GetFirstOpenApp());
        btn_Start.gameObject.SetActive(!PlayerModel.GetFirstOpenApp());
    }

    private void OnTutorialButton()
    {
        PlayerModel.SaveFirstOpenApp();
        SceneManager.LoadSceneAsync("TutorialScene");
    }

    private void OnStartButton()
    {
        PlayerModel.SaveFirstOpenApp();
        SceneManager.LoadSceneAsync("MenuScene");
    }
}
