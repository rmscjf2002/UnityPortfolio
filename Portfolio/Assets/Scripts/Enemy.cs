using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    // Animator
    public Animator anim;

    // Type
    public string Type; // Boss, Normal

    // Rigidbody Collider
    public Rigidbody rigid;
    CapsuleCollider capCollider;

    // Transform
    protected Transform trans;

    // return Pos
    public float xPos;
    public float yPos;
    public float zPos;

    // Turtle 
    [SerializeField]
    GameObject turtle;

    // ObjectManager <- ItemDrop위함
    [SerializeField]
    protected ObjectManager objectManager;

    // Player
    [SerializeField]
    protected Player player;

    // AI
    public Transform target; // 쫓아갈 타겟
    public NavMeshAgent nav;
    public bool isChase;

    // Attack
    public GameObject attackArea;
    public bool isAttack;

    // Hp, OnDamage
    public int curHealth;
    public int maxHealth;
    SkinnedMeshRenderer[] meshs;
    protected bool isDamaged;
    public bool isDead;

    // exp
    public int exp;

    // Die Effect
    [SerializeField]
    GameObject dieEffect;

    // Sounds
    [SerializeField]
    protected AudioSource dieSound;



    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();

    }


    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void Update()
    {
        // Chase 
        if (nav.enabled && isChase && Type == "Normal")
        {
            ChaseStart();
            nav.isStopped = !isChase;
        }
    }


    // 플레이어가 적의구역에 들어가면 Sense애니메이션 플레이
    public void Sense()
    {
        anim.SetTrigger("doSense");
    }

    // target Chase
    protected virtual void ChaseStart()
    { 
        nav.speed = 5f;
        nav.SetDestination(target.position);
        anim.SetFloat("move", 1f);

    }

    // 기존위치로 return
    public void returnPos()
    {
        nav.speed = 8f;
        nav.SetDestination(new Vector3(xPos, yPos, zPos));
        if (trans.position == new Vector3(xPos, yPos, zPos))
            anim.SetFloat("move", 0f);

    }

    // Chase가 true일때 velocity와 angularVelocity를 Freeze시키는 함수
    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }


    // Player의 공격범위에 닿으면 데미지를 입는다.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee" && !isDead && !isDamaged)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if ((curHealth - (weapon.damage + (player.str * 2))) >= 0)
                curHealth -= weapon.damage + player.str * 2;
            else
                curHealth = 0;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
    }

    // 피격 함수
    protected virtual IEnumerator OnDamage(Vector3 reactVec)
    {
      
        foreach (SkinnedMeshRenderer mesh in meshs)
        {
            isDamaged = true;
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (SkinnedMeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
            isDamaged = false;
        }

        else
        {
            // Item Drop
            int ran = Random.Range(0, 10);
            if (ran < 5) // 50%
            {
                
            }
            else if (ran < 7) // 20%
            {
                GameObject itemCoin = objectManager.MakeObj("Coin");
                itemCoin.transform.position = trans.position + Vector3.up;
            }
            else if (ran < 8) // 10%
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
            foreach (SkinnedMeshRenderer mesh in meshs)
                mesh.material.color = Color.gray;
            gameObject.layer = 12;
            isDead = true;
            isChase = false;
            isDamaged = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            Player player = target.GetComponent<Player>();
            player.exp += exp;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            capCollider.enabled = false;

            StartCoroutine(OnDie());

        }
    }


    // OnDie
    protected virtual IEnumerator OnDie()
    {
        if (player.questId == 20)
            player.questCnt++;
        dieSound.Play();

        yield return new WaitForSeconds(0.5f);
        turtle.SetActive(false);
        dieEffect.SetActive(true);
        StartCoroutine(RespawnMonster());
    }

    // 기존위치에 몬스터 부활.
    IEnumerator RespawnMonster()
    {
        yield return new WaitForSeconds(1f);
        foreach (SkinnedMeshRenderer mesh in meshs)
            mesh.material.color = Color.white;
        trans.position = new Vector3(xPos, yPos, zPos);
        dieEffect.SetActive(false);
        isDamaged = false;
        isDead = false;
        isChase = true;
        isAttack = false;
        //capCollider.enabled =  true;
        curHealth = 100;
        capCollider.enabled = true;
        nav.enabled = true;
        gameObject.layer = 8;
        yield return new WaitForSeconds(0.5f);
        turtle.SetActive(true);

    }

}
