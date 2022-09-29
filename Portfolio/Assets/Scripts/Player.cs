using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{



    // GameManager
    public GameManager manager;


    // Rigidbody
    Rigidbody rigid;

    // Transform
    public Transform trans;

    // Physics
    bool isBorder;

    // Status
    public int level; // 레벨
    public int str; // 힘
    public int ammo; // 방어력
    public int health; // 기본체력 100
    public int totalHealth; // 총 체력
    public int exp; // 경험치
    public int statPoint; // 스탯포인트
    public bool sDown; // status 키 s를 눌렀는지 판별
    public int money; // 돈
    

    // Move 
    float speed = 10f; // move Speed
    Vector3 moveVec;
    float hAxis;
    float vAxis;

    // Jump
    bool jDown; // 점프키를 눌렀는지 판별
    bool isJump; // 점프중이면 점프를 못하게 하기 위한 bool값
    public float jumpPower;

    // Attack
    public GameObject sword; // 무기 오브젝트
    bool f1Down; // 마우스 좌클릭 눌렀는지 확인
    public bool isAtkReady; // 공격준비가 되었는지 확인
    public float maxDelay; // 초과시 원래공격으로 초기화 
    public float waitTime; // 무기공격시간 curTime이 waitTime이 되어야 공격가능
    public float curTime; // 현재시간
    public int atkNum; // 0~3까지 연속공격
    bool cDown; // 방패들기 
    bool isDeffend; // 방패든 상태면 데미지 0


    // onDamage
    bool isDamage; // 데미지 받는중인지 확인
    MeshRenderer[] meshs; // 데미지 받을 때 mesh의 색상을 변경
    public bool isDead; // 죽음확인


    // Inventory
    [SerializeField]
    Inventory inventory; // 인벤토리 

    // Shop
    public bool isShop; // Shop을 이용중인지 확인하는 bool변수

    // Quest
    public int questId;
    public int questCnt;
    public int npcId;
    public bool isTalk;
//    public bool questFinish;


    // Animation

    Animator anim;

    // Sounds
    [SerializeField]
    AudioSource jumpSound;
    [SerializeField]
    AudioSource attackSound;
    [SerializeField]
    AudioSource dieSound;


    // Settings
    public bool isSetting;

    // Respawn
    [SerializeField]
    ReturnZone returnZone;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        trans = GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }

    void Update()
    {

        if (!isDead && !manager.isQuest) // 죽었거나, 퀘스트중일 때는 행동불가
        {
            curTime += Time.deltaTime;
            if (curTime > maxDelay)
                atkNum = 0;
            GetInput();
            if (!isSetting)
            {
                Move();
                Turn();
                Jump();
                Attack();


                if (cDown) // c키를 누르면 방패를 들고 디펜스한다.
                {
                    anim.SetBool("isDeffend", true);
                    isDeffend = true;
                }

                else if (!cDown)
                {
                    anim.SetBool("isDeffend", false);
                    isDeffend = false;
                }
            }
        }


    }

    // Input

    void GetInput()
    {
        if (!isShop) // 상점을 이용중이지 않을 때만 인풋을 받는다.
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
            jDown = Input.GetButtonDown("Jump");
            cDown = Input.GetKey(KeyCode.C);
            if (!sDown && !inventory.inventoryActivated)
                f1Down = Input.GetButtonDown("Fire1");
        }

    }

    // Move

    void Move()
    {
        if (!isDeffend) // 방패를 들고있을때는 움직이지못하도록 
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            anim.SetBool("isWalk", moveVec != Vector3.zero);
            if (!isBorder)
                transform.position += moveVec * speed * Time.deltaTime;
        }

    }

    // Turn

    void Turn() // 움직이는방향으로 바라보게하는 함수
    {
        transform.LookAt(transform.position + moveVec);
    }

    // Jump

    void Jump()
    {
        if (isJump || !jDown || cDown)
            return;

        jumpSound.Play();
        isJump = true;
        anim.SetTrigger("doJump");
        StartCoroutine("Jumping");
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

    }

    IEnumerator Jumping()
    {

        anim.SetFloat("jumpAnim", 0.1f);
        yield return new WaitForSeconds(0.3f);
        anim.SetFloat("jumpAnim", 0.5f);
        yield return new WaitForSeconds(1.9f);
        anim.SetFloat("jumpAnim", 1f);

    }





    // Physics Problem

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        // 벽에 레이를 쏴서 벽이 있으면 border = true
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }


    // Attack

    void Attack()
    {
        if (isDead)
            return;
  
        isAtkReady = waitTime < curTime;

        if (isAtkReady && f1Down)
        {
            attackSound.Play();


            sword.GetComponent<Weapon>().use();
            anim.SetTrigger("doAttack");
            anim.SetFloat("atkOrder", atkNum++);

            if (atkNum >= 4)
               atkNum = 0;
            curTime = 0;

        }

 

    }

  
    // OnCollision

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;     
        }

    }

    // OnTrigger 함수
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyAtk")
        {
            if (!isDamage)
            {
                EnemyAttack enemyAtk = other.GetComponent<EnemyAttack>();
                if(!isDeffend)
                    health -= enemyAtk.damage - ammo;
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec));
            }
        }

        else if(other.tag == "RockAtk")
        {
            if (!isDamage)
            {
                RockShot rockShot = other.GetComponent<RockShot>();
                if(!isDeffend)
                    health -= rockShot.damage - ammo;
                other.gameObject.SetActive(false);
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec));
            }
        }

        else if(other.tag == "BossAtk")
        {
            if(!isDamage)
            {
                BossMeleeAttack bossAtk = other.GetComponent<BossMeleeAttack>();
                if(!isDeffend)
                    health -= bossAtk.damage - ammo;
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Item")
        {
            manager.CheckItem(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Item")
        {
            manager.OutItem();
        }
    }

    // OnDamage

    IEnumerator OnDamage(Vector3 reactVec)
    {
        if (isDead)
            yield break;

        isDamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }
        if(health > 0)
            anim.SetTrigger("doHit");

        rigid.AddForce(transform.forward * -100, ForceMode.Impulse);

        if (health <= 0 && !isDead) // health가 0 이하면 사망
            OnDie();
        
        yield return new WaitForSeconds(1f);
        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
    }
   
    // OnDie

    void OnDie()
    {
        isDead = true;
        dieSound.Play();
        anim.SetTrigger("doDie");
        this.tag = "PlayerDie";
        this.gameObject.layer = 13;
        manager.GameOver();

    }

    // Respawn

    public void Respawn()
    {
        isDead = false;
        anim.Play("Idle");
        this.gameObject.layer = 11;
        this.tag = "Player";
        returnZone.SetActiveFunc();
    }

}
