using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 퀘스트 보상 클래스
public class QuestReward
{
    public int money; // 돈
    public int exp; // 경험치


    // 생성자
    public QuestReward (int _money, int _exp)
    {
        money = _money;
        exp = _exp;
    }

}
