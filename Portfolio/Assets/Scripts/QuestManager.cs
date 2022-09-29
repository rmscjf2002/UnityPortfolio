using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{


    // quest 관련 변수
    public int questId;
    public int questActionIdx;
    public Dictionary<int, QuestData> questList; // key : questid, value : QuestData인 Dictionary

    // Player
    [SerializeField]
    Player player;

    // GameManager
    [SerializeField]
    GameManager manager;

    // Sounds
    [SerializeField]
    AudioSource nextQuestSound;


    void Awake()
    {
      
        questList = new Dictionary<int, QuestData>();
        GenerateQuest();
    }



    // # 퀘스트 생성함수
    void GenerateQuest()
    {
        questList.Add(0,  new QuestData(" - ", new int[] { 1000 }, false, 0, new QuestReward(1000, 0)));
        questList.Add(10, new QuestData("퀘스트NPC와 대화하기.", new int[] { 1000 }, false, 0 , new QuestReward ( 1000, 0 )));
        questList.Add(20, new QuestData("터틀몬스터 10마리 처치하기.", new int[] { 1000 }, false, 10 , new QuestReward( 5000 , 300 )));
        questList.Add(30, new QuestData("보스드래곤 처치하기.", new int[] { 1000 }, false, 1, new QuestReward(10000, 1000)));
        questList.Add(40, new QuestData("모든 퀘스트 완료.", new int[] { 1000 }, false, 0, new QuestReward(0, 0)));

    }

    // # 인덱스 반환함수
    public int GetQuestTalkIdx(int id)
    {
       // questActionIdx = 0;
        return questId;
    }

    // # 퀘스트 확인

 
    public string CheckQuest()
    {
        if (questList[questId].questCnt <= player.questCnt)
        {
            player.money += questList[questId].questReward.money;
            player.exp += questList[questId].questReward.exp;
            //player.questFinish = false;
            NextQuest();
        }
        return questList[questId].questName;
    }

    // # 퀘스트완료 및 다음퀘스트 

    void NextQuest()
    {
        nextQuestSound.Play();
        player.questCnt = 0;
        questId = 0;
        player.questId += 10;
        if (player.questId > 30)
            player.questId = 40;
       // questActionIdx = 0;
    }


}
