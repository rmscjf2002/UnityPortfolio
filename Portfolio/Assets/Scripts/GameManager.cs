using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{



    // GameOver UI
    [SerializeField]
    GameObject overPanel;

    
    // Player
    [SerializeField]
    Player player;

    // Player Info
    int curExp;
    int totalExp;
    int level;
    int str;
    int ammo;
    int health;
    int statPoint;
    int money;

    // EnemySpawnObject
    [SerializeField]
    GameObject enemySpawn;


    // Status UI
    [SerializeField]
    GameObject statusPanel;
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text strText;
    [SerializeField]
    Text ammoText;
    [SerializeField]
    Text healthText;
    [SerializeField]
    Text statText;
    [SerializeField]
    GameObject strBtn;
    [SerializeField]
    GameObject ammoBtn;
    [SerializeField]
    GameObject healthBtn;


    // Item
    [SerializeField]
    GameObject itemGet;
    bool isGet;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    Text moneyText;

    public Item[] invenName;
    //public int[] invenIdx;
    public int[] invenCnt;

    // Panel
    [SerializeField]
    GameObject gamePanel;
    [SerializeField]
    GameObject menuPanel;

    // Camera
    [SerializeField]
    GameObject menuCamera;
    [SerializeField]
    GameObject gameCamera;

    // Quest
    int questId;
    int managerQuestId;
    int questCnt;
    int npcId;

    // Boss // Boss방 입장시 boss에 할당
    public Boss boss;

    // Boss health

    [SerializeField]
    RectTransform bossHealthGroup;

    [SerializeField]
    RectTransform bossHealthBar;

    // Player health

    [SerializeField]
    RectTransform playerHealthBar;

    // Player Exp

    [SerializeField]
    RectTransform playerExpBar;

    // Talk
    [SerializeField]
    TalkManager talkManager;
    [SerializeField]
    QuestManager questManager;
    [SerializeField]
    Animator talkGroup;
    [SerializeField]
    TalkEffect talkText;
    
    int talkIdx;
    public bool isQuest;

    // Quest UI
    [SerializeField]
    Text questTitle;
    [SerializeField]
    Text questContents;


    // Level Up UI
    [SerializeField]
    GameObject levelUpGroup;
    [SerializeField]
    Text levelUpText;

    // Sounds
    [SerializeField]
    AudioSource levelUpSound;

    // Settings
    [SerializeField]
    GameObject settingsGroup;
    


    void Update()
    {



        // Settings
        SettingOn();

        // PlayerInfo 
        level = player.level;
        ammo = player.ammo;
        str = player.str;
        statPoint = player.statPoint;
        health = player.health;
        money = player.money;
        questId = player.questId;
        managerQuestId = questManager.questId;
        questCnt = player.questCnt;
        npcId = player.npcId;


        ReadFile();
        curExp = player.exp;
      

        // Status 
        OpenStatus();
        levelText.text = level.ToString();
        ammoText.text = ammo.ToString();
        strText.text = str.ToString();
        healthText.text = health.ToString();
        statText.text = statPoint.ToString();
        moneyText.text = money.ToString();


        // Quest
        questTitle.text = questManager.questList[questManager.questId].questName;
        if (questManager.questList[questManager.questId].questCnt == 0)
            questContents.text = "-";

        else
            questContents.text = player.questCnt + " / " + questManager.questList[questManager.questId].questCnt.ToString();


    }

    void LateUpdate()
    {
        // Boss Hp UI
        if(boss != null)
        {
            bossHealthGroup.anchoredPosition = Vector3.down * 5;
            bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
        }
        else
            bossHealthGroup.anchoredPosition = Vector3.up * 200;

        // Player HP UI
        playerHealthBar.localScale = new Vector3((float)(health) / (player.totalHealth), 1, 1);

        // Player EXP UI
        playerExpBar.localScale = new Vector3((float)curExp / totalExp, 1, 1);

    }

    // csv파일을 읽고 레벨업하면 레벨과 스텟포인트 증가
    void ReadFile()
    {
        TextAsset textFile = Resources.Load("LevelExp") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;

            if (line.Split(',')[0] == "Level")
                continue;

            if (int.Parse(line.Split(',')[0]) == player.level)
            {
                totalExp = int.Parse(line.Split(',')[1]);
                if (int.Parse(line.Split(',')[1]) <= player.exp)
                {
                    player.exp -= int.Parse(line.Split(',')[1]);
                    player.level++;
                    StartCoroutine("LevelUpFunc");
                    player.statPoint += 3;
                    break;
                }

                break;
            }
        }
        stringReader.Close();
    }

 
    // 캐릭터 사망
    public void GameOver()
    {
        overPanel.SetActive(true);
    }

    // 부활버튼
    public void Restart()
    {
        player.Respawn();
        player.health = player.totalHealth;
        overPanel.SetActive(false);
    }

    // 스텟창 열고 닫기 (t키로 설정)
    void OpenStatus()
    {


        if (!player.sDown && Input.GetButtonDown("Status")) // button t
        {
            player.sDown = true;
            statusPanel.SetActive(true);

        }

        else if (player.sDown && Input.GetButtonDown("Status"))
        {
            player.sDown = false;
            statusPanel.SetActive(false);
        }

        if (statusPanel.activeSelf)
        {
            if (statPoint >= 1)
            {
                strBtn.SetActive(true);
                ammoBtn.SetActive(true);
                healthBtn.SetActive(true);
            }
            else
            {
                strBtn.SetActive(false);
                ammoBtn.SetActive(false);
                healthBtn.SetActive(false);
            }
        }
    }

    public void CloseStatus() // 스탯창 버튼으로 닫을 때
    {
        player.sDown = false;
        statusPanel.SetActive(false);
    }


    // 스텟 찍기

    public void StrUp()
    {
        player.str++;
        player.statPoint--;
    }

    public void AmmoUp()
    {
        player.ammo++;
        player.statPoint--;
    }

    public void HealthUp()
    {
        player.health += 3;
        player.totalHealth += 3;
        player.statPoint--;
    }

    // Item Get

    public void CheckItem(GameObject item)
    {
        isGet = true;
        itemGet.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Z) && isGet)
        {
            inventory.AcquireItem(item.GetComponent<Item_PickUp>().item);
            item.SetActive(false);
            OutItem();
        }
    }
    public void OutItem()
    {
        isGet = false;
        itemGet.SetActive(false);
    }

    // Item Use
    public void PortionUse(int num)
    {
       
        if (player.health + num > player.totalHealth)
            player.health = player.totalHealth;
        else
            player.health += num;
        
    }

    public void PointPortionUse(int num)
    {
        player.statPoint += num;
    }

    public void CoinUse(int num)
    {
        player.money += num;
    }


    // GameSave   
    public void GameSave()
    {
        PlayerPrefs.SetInt("Level", player.level);
        PlayerPrefs.SetInt("Ammo", player.ammo);
        PlayerPrefs.SetInt("Str", player.str);
        PlayerPrefs.SetInt("StatPoint", player.statPoint);
        PlayerPrefs.SetInt("Health", player.health);
        PlayerPrefs.SetInt("Money", player.money);
        PlayerPrefs.SetInt("QuestId", player.questId);
        PlayerPrefs.SetInt("ManagerQuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestCnt", player.questCnt);
        PlayerPrefs.SetInt("Exp", player.exp);
    }

    //  Game Start Screen
    public void GameStart()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        menuCamera.SetActive(false);
        gameCamera.SetActive(true);
        player.gameObject.SetActive(true);
        enemySpawn.SetActive(true);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("Level")) // 저장데이터가 없으면 불러오기가 안된다.
            return;

        player.level = PlayerPrefs.GetInt("Level");
        player.ammo = PlayerPrefs.GetInt("Ammo");
        player.str = PlayerPrefs.GetInt("Str");
        player.statPoint = PlayerPrefs.GetInt("StatPoint");
        player.health = PlayerPrefs.GetInt("Health");
        player.money = PlayerPrefs.GetInt("Money");
        player.questId = PlayerPrefs.GetInt("QuestId");
        questManager.questId = PlayerPrefs.GetInt("ManagerQuestId");
        player.questCnt = PlayerPrefs.GetInt("QuestCnt");
        player.exp = PlayerPrefs.GetInt("Exp");


        GameStart();

    }

    // GameQuit

    public void GameQuit()
    {
        Application.Quit();
    }


    // Quest

    public void Action()
    {
        if (questManager.questList[questId].questCnt <= player.questCnt && questManager.questList[questId].questCnt != 0)
        {
           // player.questFinish = true;
            Talk(npcId + 1);
        }
        else
            Talk(npcId);
        talkGroup.SetBool("isShow", isQuest);
    }


    public void ExitQuest()
    {
        talkGroup.SetBool("isShow", false);
    }

    // Talk 
    public void Talk(int _npcId)
    {
        int questTalkIdx = 0;
        string tmp = "";
        if(talkText.isAnim)
        {
            talkText.SetMsg("");
            return;
        }

        else
        {
            questManager.questId = questId;
            questTalkIdx = questManager.GetQuestTalkIdx(_npcId);
            tmp = talkManager.GetTalk(_npcId + questTalkIdx, talkIdx);
        }


        // End Talk
        if (tmp == null)
        {
            isQuest = false;
            talkIdx = 0;
            questManager.CheckQuest();
            ExitQuest();
            return;
        }

        talkText.SetMsg(tmp);
        isQuest = true;
        talkIdx++;
    }

 

    // LvUp

    IEnumerator LevelUpFunc()
    {
        yield return new WaitForSeconds(0.5f);
        levelUpSound.Play();
        levelUpGroup.SetActive(true);
        levelUpText.text = "레벨업 했습니다" + "\n" + "Lv: " + level.ToString();
        yield return new WaitForSeconds(1.5f);
        levelUpGroup.SetActive(false);
    }


    // Settings

    public void SettingOnBtn()
    {
        if(!player.isSetting)
        {
            player.isSetting = true;
            settingsGroup.SetActive(true);
        }
    }

    public void SettingQuitBtn()
    {
        if (player.isSetting)
        {
            player.isSetting = false;
            settingsGroup.SetActive(false);
        }
    }
    public void SettingOn()
    {
        if (!player.isSetting && Input.GetKeyDown(KeyCode.Escape))
        {
            player.isSetting = true;
            settingsGroup.SetActive(true);
        }
        else if(player.isSetting && Input.GetKeyDown(KeyCode.Escape))
        {
            player.isSetting = false;
            settingsGroup.SetActive(false);
        }
    }



}
