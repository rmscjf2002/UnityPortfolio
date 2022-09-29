using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // Key : questId + npcId , Value : string배열인 Dictionary
    Dictionary<int, string[]> talkData;

    // GameManager
    [SerializeField]
    GameManager manager;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateTalk();
    }

    //  대화생성
    void GenerateTalk()
    {
        // Talk Data


        talkData.Add(1000, new string[] { "안녕?", "반가워!"});

        // Quest Talk
        talkData.Add(10 + 1000, new string[] { "안녕?", "처음보는 얼굴인데?", "기념으로 내가 선물을 줄게!" , "얼마 안되지만 물약 한개정돈 살 수 있을거야" });
        //talkData.Add(11 + 1000, new string[] { "얼마 안되지만 물약 한개정돈 살 수 있을거야" });
       // talkData.Add(12 + 1000, new string[] { "선물을 받고 싶으면 다시 말을 걸어줘!" });
        talkData.Add(20 + 1000, new string[] { "요즘 터틀몬스터들이 기승을 부리고 있어", "혹시 너가 처치해 줄 수 있을까?", "고마워 !", "모두 처치하고 나에게 와서 얘기해줘" });
        talkData.Add(21 + 1000, new string[] { "고마워 이건 작은 선물이야!" });
       // talkData.Add(22 + 1000, new string[] { "아쉽네..", "마음이 바뀌면 다시 말해줘!" });
        talkData.Add(30 + 1000, new string[] { "드래곤이 출몰해 마을을 망치고있어", "혹시 너가 처치해 줄 수 있을까?", "드래곤을 꼭 무찔러줘!" });
        talkData.Add(31 + 1000, new string[] { "덕분에 마을이 안전해졌어!" });
        //  talkData.Add(32 + 1000, new string[] { "혹시 마음이 바뀌면 다시 말을 걸어줘:" });
        talkData.Add(40 + 1000, new string[] { "마을에 평화를 가져다줘서 고마워!" });


    }

    // 대화 반환
    public string GetTalk(int id, int idx)
    {
     
        if (idx == talkData[id].Length)
            return null;
        else
            return talkData[id][idx];

    }

 


}
