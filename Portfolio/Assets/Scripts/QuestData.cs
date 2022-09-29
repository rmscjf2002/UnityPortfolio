using System.Collections;
using System.Collections.Generic;

// QuestData클래스
public class QuestData
{
    public string questName; // 퀘스트이름
    public int[] npcId; // npcId
    public bool finish; // 완료했는지 확인
    public int questCnt; // 만약 몬스터를 잡는 퀘스트면 잡은 개수
    public QuestReward questReward; // 퀘스트 보상


    // 생성자
    public QuestData(string name, int[] npc, bool _finish, int cnt, QuestReward reward)
    {
        questName = name;
        npcId = npc;
        finish = _finish;
        questCnt = cnt;
        questReward = reward;
    }
}
