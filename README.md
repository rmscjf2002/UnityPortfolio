# UnityPortfolio

## 개발기간
2022/09/08 ~ 2022/09/24 (17일)
<br/>

## 개발계기
> - 2021년에 레트로의 유니티 게임 프로그래밍이란 책을보고 클론코딩을 하고,<br/> 
Youtube로 클론코딩을 했었는데, 2022년이 되어서 한번 내가 만들고싶은 프로그램을 만들어보자해서, <br/>
Youtube로 Unity를 다시 한번 복습한 후 만들게 되었다.
<br/>

## 개발환경
- Unity 버전 2019.4.20f1
<br/>

## 개발과정

<br/>

### 0908 
> - 맵에 Material을 적용
> - 블렌드트리로 공격 애니메이션
> - 최대4번공격 가능하게 Attack 구현 (로직을 못짜겠어서, UI를 가져와서 게이지를 가져와 캐치하면 공격하는식으로 구현)
> - 점프 구현
<br/>

#### 0911
> - Turtle몬스터를 생성하고 Spawn구역에서만 활동하고, Spawn구역의 NavMesh활용을 위해 Static설정
> - OnTriggerStay, OnTriggerExit을 사용해, 플레이어를 따라오도록 구현.
> - Spawn구역에 플레이어가 있으면 NavMesh가 작동하고, 밖으로 나가면 몬스터는 제자리로 돌아간다.
> - Idle, Attack1, Attack2, Sense 애니메이션 설정
> - 2가지 어택패턴 중 하나를 Random.Range로 구해서 공격
> - 물리문제 수정<br/>
     - 레이를 쏴서 벽을 통과하지못하도록 코드작성<br/>
     - 플레이어가 콜라이더와 부딪히면 자동으로 y축으로 회전하는문제 해결
<br/>

#### 0912
> - 몬스터의 Attack 로직수정 , OnTriggerEnter로 플레이어가 들어오면 공격을 하도록 설정했는데,<br/>
    그러면 한번만 공격하고, 공격을 멈춰서 OnTriggerStay로 변경
> - 플레이어가 Enemy를 공격했을 때 Enemy의 피격구현 (스킨드메쉬렌더러의 색상변경)
<br/>

#### 0914
> - 적이 사망하고 원래 태어나던 자리로 부활하도록 구현
<br/>

#### 0915
> - 플레이어 로직 구현
> - 플레이어 죽음 (현재 HP가 0이하로 떨어지면 사망하고 사망 UI를 띄운다.)
> - 플레이어 리스폰 (사망UI에서 부활하는 UI버튼을 클릭하면 캐릭터가 부활한다.)
> - 플레이어 레벨과 경험치 (CSV파일로 만들어서 GameManager에서 읽어서 레벨업구현)
> - 플레이어 스텟포인트 (레벨업하면 스텟포인트를 3 지급한다.)
<br/>

#### 0916
> - 스텟창 UI 구현 (Text로 플레이어의 현재스텟 할당)
> - 몬스터 사냥하면 랜덤확률로 아이템이 드랍하게 만듦 (오브젝트 풀)
> - 인벤토리 구현 (드래그앤 드랍이 가능하고, 아이템을 획득하면 인벤토리에 등록된다.)
> - 아이템 사용 (인벤토리에서 우클릭을 누르면 사용)
<br/>

#### 0917
> - 퀵슬롯 구현 (퀵슬롯에 아이템을 등록하고 각 버튼을 누르면 사용된다.)
> - 상점 구현 (상점에서 아이템 구매가능, 돈이부족하면 돈이부족하다는 텍스트가 나온다.)
> - 보스맵을 새로운 씬을 만들어서 생성
> - 보스와 전투구현 (보스는 3가지 패턴을 생각하면서 각기다른 공격을 한다.)
<br/>

#### 0920
> - 인벤토리UI에 플레이어의 Money표기
> - 캐릭터 피격 애니메이션 추가
> - 캐릭터 방어 애니메이션 추가, 캐릭터 방어가능 (c키로 방어하고, 쉴드 중 피격당하면 데미지는 -0)
> - 인벤토리 드래그앤드랍이 제대로 작동하지않음 -> RectPosition을 잘못설정한 문제 해결
<br/>

#### 0921
> - BossZone을 파티클시스템과 트리거를 통해 구현 <br/>
    BossZone에 들어가면 경고 UI가 나오고 F를 누르면 보스존에 입장가능.
> - MenuCamera와 GameCamera를 구분 (MenuCamera는 시작화면, GameCamera는 게임시작 시 활성화)
> - UI도 MenuPanel과 GamePanel로 구분짓고 시작화면 GamePanelUI를 활성화
<br/>

#### 0922
> - 퀘스트 구현(UI 작성 및 퀘스트 로직 작성)
<br/>

#### 0923
> - 퀘스트 구현 리팩토링(퀘스트를 받고 다음 퀘스트로 안넘어가지는 버그가 있어서 수정)
> - HP,EXP UI 생성
> - 보스맵으로 이동하면 오브젝트가 파괴된다 -> 파괴되면 안되는 것들 싱글톤으로 작성.
<br/>

#### 0924
> - 보스로직수정 (보스존에 재입장할 시 보스가 초기화가 되어있지않았음 -> OnEnable로 초기값 설정해줌)
> - 보스맵Scene을 삭제하고 SetActive로 구현하도록 변경<br/>
  -> Scene전환 시 GC가 동작한다고 하여, 굳이 Scene이 필요없을 거 같아 활성화/비활성화로 구현
> - QuestUI, BossHpUI등 필요 UI 생성
> - 보스존에서 마을로 돌아오는 ReturnZone 생성
> - 사운드 적용
> - 캐릭터가 죽었을 때 Enemy가 Player를 계속 물리효과로 끌고가는 현상 수정 <br/>
-> Layer를 PlayerDie로 변경하고 LayerCollisionMatrix활용
> - Player의 공격데미지를 Str과 비례하게 설정
> - Player의 피격데미지를 Ammo에 비례하게 설정

<br/>

## 영상링크
https://youtu.be/3PsVFbm03W8
## PDF
https://drive.google.com/file/d/1gBjAKHpYxyFXiYzx_YqUdEndbpj3SNLd/view?usp=sharing
