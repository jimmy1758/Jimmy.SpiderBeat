using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : UIScreen {

    private Toggle[] toggles;
    private Transform bgTrans;

    [SerializeField]
	private Text songName;
	[SerializeField]
	private Image songCover;
	private Transform menuPanel;
	private Transform shopPanel;
    private Transform noticePanel;
    private Transform shopContent;
    private GameObject normalLock;
    private GameObject crazyLock;
    private Button btn_Buy;
    private Button btn_Left;
    private Button btn_Right;
    private Button btn_Easy;
    private Button btn_Normal;
    private Button btn_Crazy;
    private Text txt_detailEasy;
    private Text txt_detailNormal;
    private Text txt_detailCrazy;
    private Text txt_CoinCost;
    private Text txt_CoinCount;
    private Text txt_Description;
    private Image Icon_ObjToBuy;
    private GameObject commodityPrefab;
	
	private int musicIndex = 0;
    private int countToBuy = 1;
    private int currentId;

    private void Update()
    {
        txt_CoinCount.text = PlayerModel.GetCoinsData().ToString();
    }

    protected override void Init()
    {
        base.Init();
        InitComponent();
        InitData();
        Tweener tweener = bgTrans.DOLocalMoveY((bgTrans as RectTransform).rect.height - 120.0f, 10.0f);
        tweener.SetLoops(-1, LoopType.Restart);
        toggles = GetComponentsInChildren<Toggle>();
        toggles[3].onValueChanged.AddListener((bool value) => OnMenuToggleChanged(value));
        toggles[4].onValueChanged.AddListener((bool value) => OnShopToggleChanged(value));
        toggles[5].onValueChanged.AddListener((bool value) => OnBlogToggleChanged(value));
        btn_Left.onClick.AddListener(()=> OnCountSelectionButton(false));
        btn_Right.onClick.AddListener(() => OnCountSelectionButton(true));
        btn_Buy.onClick.AddListener(OnClickBuyButton);
        btn_Easy.onClick.AddListener(() => OnLevelSelectionButton("Easy"));
        btn_Normal.onClick.AddListener(() => OnLevelSelectionButton("Normal"));
        btn_Crazy.onClick.AddListener(() => OnLevelSelectionButton("Crazy"));
        UpdateMusicInfo();
    }

    private void InitComponent()
    {
        bgTrans = transform.Find("BG");
        menuPanel = transform.Find("Panel_Menu");
        shopPanel = transform.Find("Panel_Shop");
        noticePanel = transform.Find("Panel_Notice");
        shopContent = shopPanel.Find("ScrollView/Viewport/Content");
        normalLock = menuPanel.Find("Panel_LevelSelection/Panel_Requirement/Img_NormalBG/Img_Lock").gameObject;
        crazyLock = menuPanel.Find("Panel_LevelSelection/Panel_Requirement/Img_CrazyBG/Img_Lock").gameObject;
        Transform costPanel = shopPanel.Find("Panel_SaleObject/CoinCost");
        btn_Buy = costPanel.Find("Btn_Buy").GetComponent<Button>();
        btn_Left = costPanel.Find("Btn_Left").GetComponent<Button>();
        btn_Right = costPanel.Find("Btn_Right").GetComponent<Button>();
        Transform levelButtons = transform.Find("Panel_Menu/Panel_LevelSelection/Panel_Level");
        btn_Easy = levelButtons.Find("Btn_Easy").GetComponent<Button>();
        btn_Normal = levelButtons.Find("Btn_Normal").GetComponent<Button>();
        btn_Crazy = levelButtons.Find("Btn_Crazy").GetComponent<Button>();
        txt_detailEasy = menuPanel.Find("Panel_LevelSelection/Panel_Requirement/Img_EasyBG/Text_easyScore").GetComponent<Text>();
        txt_detailNormal = menuPanel.Find("Panel_LevelSelection/Panel_Requirement/Img_NormalBG/Text_normalScore").GetComponent<Text>();
        txt_detailCrazy = menuPanel.Find("Panel_LevelSelection/Panel_Requirement/Img_CrazyBG/Text_crazyScore").GetComponent<Text>();
        txt_CoinCost = btn_Buy.GetComponentInChildren<Text>();
        txt_CoinCount = shopPanel.Find("Wallet/Inset/Text_Amount").GetComponent<Text>();
        txt_Description = costPanel.parent.Find("CommodityDescription/Text_Description").GetComponent<Text>();
        Icon_ObjToBuy = shopPanel.Find("Panel_SaleObject/ObjToBuy/Img_Icon").GetComponent<Image>();
        commodityPrefab = shopContent.Find("CommodityItem").gameObject;
    }

    private void InitData()
    {
        txt_CoinCount.text = PlayerModel.GetCoinsData().ToString();

        UpdateCommidtyToBuyInfo(1001);
        List<int> allCommodities = CommodityModel.Instance.GetAllIDs();
        for (int i = 0; i < allCommodities.Count; i++)
        {
            GameObject cGO = Instantiate(commodityPrefab, shopContent);
            cGO.SetActive(true);
            cGO.name = allCommodities[i].ToString();
            string cName = CommodityModel.Instance.GetCommodityName(allCommodities[i]);
            int cPrice = CommodityModel.Instance.GetCommodityCost(allCommodities[i]);
            string cDescription = CommodityModel.Instance.GetDescription(allCommodities[i]);
            cGO.transform.Find("Text_Name").GetComponent<Text>().text = cName;
            cGO.transform.Find("Image_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + cName);
            cGO.transform.Find("Price/CoinCost").GetComponent<Text>().text = cPrice.ToString();
            cGO.GetComponent<Toggle>().onValueChanged.AddListener((bool value)=> {
                AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
                if (value)
                {
                    countToBuy = 1;
                    UpdateCommidtyToBuyInfo(int.Parse(cGO.name));
                    cGO.transform.localScale = Vector3.one * 1.05f;
                }
                else
                {
                    cGO.transform.localScale = Vector3.one;
                }

            });
        }
    }

    private void OnEnable()
    {
        UpdateMusicInfo();
    }

    private void UpdateCommidtyToBuyInfo(int cID)
    {
        currentId = cID;
        Icon_ObjToBuy.sprite = Resources.Load<Sprite>("Sprites/" + CommodityModel.Instance.GetCommodityName(cID));
        txt_CoinCost.text = CommodityModel.Instance.GetCommodityCost(cID).ToString();
        txt_Description.text = CommodityModel.Instance.GetDescription(cID).ToString();
    }

    private void OnMenuToggleChanged(bool isOn)
    {
        if (isOn)
        {
            AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
            shopPanel.transform.DOLocalMoveX(1080f, 0.5f).SetEase(Ease.InSine);
            shopPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            menuPanel.transform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.InSine);
            menuPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        }
    }

    private void OnShopToggleChanged(bool isOn)
    {
        if (isOn)
        {
            AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
            menuPanel.transform.DOLocalMoveX(1080f, 0.5f).SetEase(Ease.InSine);
            menuPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            shopPanel.transform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.InSine);
            shopPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        }
    }

    private void OnBlogToggleChanged(bool isOn)
    {
        if (isOn)
        {
            AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
            Application.OpenURL("www.baidu.com");
        }
        noticePanel.gameObject.SetActive(isOn);
    }

	public void OnLevelSelectionButton(string difficulty)
	{
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        UIManager.instance.Push(typeof(HUD));
        PlayerModel.difficulty = difficulty;
		GameManager.instance.GameStart();
	}

    private int currentMusicID = 1001;
	public void OnMusicSelectionButton(bool right)
	{
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        currentMusicID = MusicModel.Instance.GetNextMusicID(ref musicIndex, right);
		UpdateMusicInfo();
	}

    private void OnCountSelectionButton(bool right)
    {
        AudioManager.instance.PlaySFX(SFXAudio.SFX_BtnClick);
        if (right)
            countToBuy++;
        else
            countToBuy--;
        countToBuy = Mathf.Max(countToBuy, 1);
        txt_CoinCost.text = (countToBuy * CommodityModel.Instance.GetCommodityCost(currentId)).ToString();
    }

    private void OnClickBuyButton()
    {
        if (PlayerModel.SaveCoinsData(-int.Parse(txt_CoinCost.text)))
        {
            switch (currentId)
            {
                case 1001:
                    PlayerModel.SaveMaxHpData(5 * countToBuy);
                    break;
                case 1002:
                    PlayerModel.SaveInitialComboData(1 * countToBuy);
                    break;
                case 1003:
                    PlayerModel.SaveCoinsData(countToBuy * 100);
                    break;
            }
        }
        else
        {
            Debug.Log("金币不足");
            AudioManager.instance.PlaySFX(SFXAudio.SFX_CoinNotEnough);
        }
    }

	private void UpdateMusicInfo()
	{
        PlayerModel.currentSong = MusicModel.Instance.GetMusicName(currentMusicID);
        string musicName = PlayerModel.currentSong;
		songName.text = musicName;
		songCover.sprite = Resources.Load<Sprite>(MusicModel.Instance.GetCoverPath(currentMusicID));
        //根据当前选择的歌曲判断当前的难度是否锁定
        bool normal = PlayerModel.GetUnlockStatsOfSongs(musicName, "Normal");
        bool crazy = PlayerModel.GetUnlockStatsOfSongs(musicName, "Crazy");

        normalLock.SetActive(!normal);
        crazyLock.SetActive(!crazy);
        txt_detailEasy.text = "High Score: \n" + PlayerModel.GetHighScoreData(musicName, "Easy").ToString();
        txt_detailNormal.GetComponentInChildren<Text>().text = normal ? "High Score: \n" + PlayerModel.GetHighScoreData(musicName, "Normal").ToString() : "Get 1000 points in EASY for NORMAL level!";
        txt_detailCrazy.GetComponentInChildren<Text>().text = crazy ? "High Score: \n" + PlayerModel.GetHighScoreData(musicName, "Crazy").ToString() : "Get 1500 points in NORMAL for CRAZY level!";

        btn_Normal.interactable = normal;
        btn_Crazy.interactable = crazy;
    }
}
