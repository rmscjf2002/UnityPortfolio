using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


// Enemy class를 상속받은 Boss관련 class
public class Boss : Enemy
{

    // player를 바라보는 변수
    Vector3 lookVec;

    // 데미지를 입을 때 색상변경을 위한 Mesh
    SkinnedMeshRenderer[] bossMeshs;

    // 근접공격 할 수 있는지 확인
    bool isMelee;

    // Rock을 던질때는 isLook = false
    bool isLook;

    // Boss Collider
    BoxCollider boxCollider;

    // RockArea
    [SerializeField]
    GameObject rockArea;
    float diff;

    // Attack
    [SerializeField]
    BossMeleeAttack basicBossAtk;
    [SerializeField]
    BossMeleeAttack tailBossAtk;

    // Sounds
    [SerializeField]
    AudioSource gameSound;
    [SerializeField]
    AudioSource bossBgm;
    [SerializeField]
    AudioSource bossRock;


    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        bossMeshs = GetComponentsInChildren<SkinnedMeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        bossSettings();
        StartCoroutine("Scream");
    }


    void Update()
    {

        if (isDead)
            return;
        

        else
        {
            // diff가 66이상일때만 Rock을 던진다.
           diff = Mathf.Abs(player.trans.position.z - transform.position.z);
           if (isLook)
           {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                lookVec = new Vector3(h, 0, v) * 5f;
                transform.LookAt(target.position + lookVec);
           }
            if (nav.enabled && isChase)
            {
                ChaseStart();
                nav.isStopped = !isChase;
            }

        }

        if (!isChase)
            anim.SetFloat("move", 0f);

 

    }

    // target Chase

    protected override void ChaseStart()
    {
        if (!isDead)
        {
            nav.speed = 12f;
            nav.SetDestination(target.position);
            anim.SetFloat("move", 1f);
        }
    }

 
    // Melee Trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isMelee = true;
            isChase = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isMelee = false;
            isChase = true;
        }
    }

    // Boss가 울부짖는모션을 하고 Chase 시작
    IEnumerator Scream()
    {
        if (!isDead)
        {
            anim.SetTrigger("doScream");
            yield return new WaitForSeconds(2f);
            isChase = true;
            nav.isStopped = false;

            StartCoroutine("Think");
        }
    }

    
    // 공격패턴 생각
    public IEnumerator Think()
    {
        isLook = true;
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 6);

        switch(ranAction)
        {
            case 0:
            case 1:
            case 2:
                if (!isMelee)
                {
                    StartCoroutine(Think());
                    break;
                }
              
                StartCoroutine("BasicAttack");
                break;
            case 3:
                if(isMelee || diff < 65)
                {
                    StartCoroutine(Think());
                    break;
                }
                StartCoroutine("RockShot");
                break;
            case 4:
            case 5:
                if (!isMelee)
                {
                    StartCoroutine(Think());
                    break;
                }
                StartCoroutine("TailAttack");
                break;

        }
    }
    

    // Attacks

    IEnumerator BasicAttack()
    {
        anim.SetTrigger("doBasicAttack");
        yield return new WaitForSeconds(0.5f);
        basicBossAtk.StartCoroutine("Attack");
    }

   
    IEnumerator RockShot()
    {
        isLook = false;
        anim.SetTrigger("doRockShot");
        GameObject Rock = objectManager.MakeObj("BossRock");

        Rock.transform.position = rockArea.transform.position;
        RockShot rockShot = Rock.GetComponent<RockShot>();
        rockShot.isRock = true;
        rockShot.player = player;
        rockShot.objectManager = objectManager;
        bossRock.Play();
        yield return new WaitForSeconds(0.1f);
        rockShot.isRock = false;
        yield return new WaitForSeconds(1.5f);
       
        StartCoroutine(Think());
    }

    IEnumerator TailAttack()
    {
        anim.SetTrigger("doTailAttack");
        yield return new WaitForSeconds(0.5f);
        tailBossAtk.StartCoroutine("Attack");
    }

    // OnDamaged
    protected override IEnumerator OnDamage(Vector3 reactVec)
    {

        foreach (SkinnedMeshRenderer mesh in bossMeshs)
        {
            isDamaged = true;
            mesh.material.color = Color.red;
        }
        anim.SetTrigger("doHit");

        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (SkinnedMeshRenderer mesh in bossMeshs)
                mesh.material.color = Color.white;
            isDamaged = false;
        }

        else
        {
            // Item Drop

            for (int i = 0; i < 5; i++)
            {
                int ran = Random.Range(0, 10);

                if (ran < 2) // 20%
                {
                    continue;
                }
                else if (ran < 5) // 30%
                {
                    GameObject itemCoin = objectManager.MakeObj("Coin");
                    itemCoin.transform.position = trans.position + Vector3.up;
                }
                else if (ran < 8) // 30%
                {
                    GameObject itemRedPotion = objectManager.MakeObj("RedPotion");
                    itemRedPotion.transform.position = trans.position + Vector3.up;
                }
                else if (ran < 9) // 10%
                {
                    GameObject itemBluePotion = objectManager.MakeObj("BluePotion");
                    itemBluePotion.transform.position = trans.position + Vector3.up;
                }
                else if (ran < 10) // 10%
                {
                    GameObject itemGreenPotion = objectManager.MakeObj("GreenPotion");
                    itemGreenPotion.transform.position = trans.position + Vector3.up;
                }
            }
            foreach (SkinnedMeshRenderer mesh in bossMeshs)
                mesh.material.color = Color.gray;

            isDead = true;
            isChase = false;
            nav.enabled = false;

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            Player player = target.GetComponent<Player>();
            player.exp += exp;
       
            boxCollider.enabled = false;

            StartCoroutine(OnDie());

        }
    }

    // OnDie
    protected override IEnumerator OnDie()
    {
        if (player.questId == 30)
            player.questCnt++;
        anim.SetTrigger("doDie");
        StopAllCoroutines();
        this.gameObject.layer = 12;
        dieSound.Play();
        yield return new WaitForSeconds(0.5f);
       // this.gameObject.SetActive(false);

    }

    // Boss의 기본셋팅
    void bossSettings()
    {
        nav.enabled = true; 
        curHealth = maxHealth;
        this.gameObject.layer = 8;
        isDead = false;
        gameSound.Stop();
        bossBgm.Play();
        this.transform.position = new Vector3(xPos, yPos, zPos);
        isLook = true;
        this.transform.position = new Vector3(xPos, yPos, zPos);
        isChase = false;
        isDamaged = false;
        nav.isStopped = true;
        boxCollider.enabled = true;


        foreach (SkinnedMeshRenderer mesh in bossMeshs)
            mesh.material.color = Color.white;
    }
}
