using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TalkEffect : MonoBehaviour
{

    int CharPerSeconds; // 시간당 단어시간을 위한 변수
    public bool isAnim; 

    // 대화가끝나면 나오는 커서오브젝트
    public GameObject endCursor;

    string tmpMsg;

    // Text
    Text msgText;

    // 인덱스
    int idx;
    float interval; // 휴식시간.

    // Sound
    AudioSource audioSource;

    void Awake()
    {
        CharPerSeconds = 10;
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // text에 Message 할당
    public void SetMsg(string msg)
    {
        if(isAnim)
        {
            StopAllCoroutines();
            msgText.text = tmpMsg;
            EffectEnd();
        }
        else
        {
            tmpMsg = msg;
            EffectStart();
        }
    }

    // Talk이펙트 실행
    void EffectStart()
    {
        endCursor.SetActive(false);
        isAnim = true;
        msgText.text = "";
        idx = 0;
        interval = 1f / CharPerSeconds;
        StartCoroutine(Effecting());

    }

    // 이펙트실행중
    IEnumerator Effecting()
    {
        yield return new WaitForSeconds(interval);
        if(msgText.text == tmpMsg)
        {
            EffectEnd();
            yield break;
        }
        msgText.text += tmpMsg[idx];

        if (tmpMsg[idx] != '.' || tmpMsg[idx] != ' ')
            audioSource.Play();
        idx++;

        StartCoroutine(Effecting());
    }

    // 이펙트 종료
    void EffectEnd()
    {
        endCursor.SetActive(true);
        isAnim = false;
    }
}
