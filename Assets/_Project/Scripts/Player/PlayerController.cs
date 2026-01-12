using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// アニメーション８話 4:30
/// </summary>
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    float moveSpeed = 5;

    [SerializeField] GameSceneDirector sceneDirector;
    [SerializeField] Slider sliderHP;
    [SerializeField] Slider sliderXP;
    public CharacterStats Stats;
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;

    List<int> levelRequirements;
    EnemySpawnerController enemySpawner;
    public Vector2 Forward;
    Text textLevel;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ///animator = GetComponent<Animator>();
    }
    void Update()
    {
        movePlayer();
        moveCamera();
        updateTimer();
        moveSliderHP();
    }

    public void Init(GameSceneDirector sceneDirector, EnemySpawnerController enemySpawner,
        CharacterStats characterStats, Text textLevel, Slider sliderHP, Slider sliderXP)
    {
        levelRequirements = new List<int>();
        this.sceneDirector = sceneDirector;
        this.enemySpawner = enemySpawner;
        this.Stats = characterStats;
        this.textLevel = textLevel;
        this.sliderHP = sliderHP;
        this.sliderXP = sliderXP;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Forward = Vector2.right;

        levelRequirements.Add(0);
        for(int i = 1; i < 1000; i++)
        {
            int prevxp = levelRequirements[i - 1];
            int addxp = 16;
            levelRequirements.Add(prevxp + addxp);
            if(i == 1)
            {
                addxp = 5;
            }
            else if(20 >= i)
            {
                addxp = 10;
            }
            else if(40 >= i)
            {
                addxp = 13;
            }
            levelRequirements.Add(prevxp + addxp);
        }
        Stats.MaxXP = levelRequirements[1];

        setTextLv();
        setSliderHP();
        setSliderXP();
        moveSliderHP();
    }

    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        string trigger = "";

        if(Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector2.up;
            trigger = "isUp";
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            dir -= Vector2.up;
            trigger = "isDown";
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector2.right;
            trigger = "isRight";
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            dir -= Vector2.right;
            trigger = "isLeft";
        }
        if(Vector2.zero == dir)return;
        rigidbody2d.position += dir.normalized * Stats.MoveSpeed * Time.deltaTime;
        ///animator.SetTrigger(trigger);
        if(rigidbody2d.position.x < sceneDirector.WorldStart.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x =sceneDirector.WorldStart.x;
            rigidbody2d.position = pos;
        }
        if(rigidbody2d.position.y < sceneDirector.WorldStart.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y =sceneDirector.WorldStart.y;
            rigidbody2d.position = pos;
        }
        if(rigidbody2d.position.x > sceneDirector.WorldEnd.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x =sceneDirector.WorldEnd.x;
            rigidbody2d.position = pos;
        }
        if(rigidbody2d.position.y > sceneDirector.WorldEnd.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y =sceneDirector.WorldEnd.y;
            rigidbody2d.position = pos;
        }
    }

    void moveCamera()
    {
        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;
        if(pos.x < sceneDirector.TileMapStart.x)
        {
            pos.x = sceneDirector.TileMapStart.x;
        }
        if(pos.y < sceneDirector.TileMapStart.y)
        {
            pos.y = sceneDirector.TileMapStart.y;
        }
        if(pos.x > sceneDirector.TileMapEnd.x)
        {
            pos.x = sceneDirector.TileMapEnd.x;
        }
        if(pos.y > sceneDirector.TileMapEnd.y)
        {
            pos.y = sceneDirector.TileMapEnd.y;
        }
        Camera.main.transform.position = pos;

    }

    void moveSliderHP()
    {
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main,transform.position);
        pos.y -= 50;
        sliderHP.transform.position = pos;
    }
    
    public void Damage(float attack)
    {
        if(!enabled) return;
        float damage = Mathf.Max(0,attack - Stats.Defense);
        Stats.HP -= damage;

        //ダメージ表示
        sceneDirector.DispDamage(gameObject, damage);

        if(0 > Stats.HP)
        {
            
        }
        if(0 > Stats.HP) Stats.HP = 0;
        setSliderHP();
    }
    void setSliderHP()
    {
        sliderHP.maxValue = Stats.MaxHP;
        sliderHP.value = Stats.HP;
    }
    void setSliderXP()
    {
        sliderHP.maxValue = Stats.MaxXP;
        sliderHP.value = Stats.XP;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        attackEnemy(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        attackEnemy(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    //プレイヤーへ攻撃
    void attackEnemy(Collision2D collision)
    {
        if(! collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        if(0 < attackCoolDownTimer) return;
        
        enemy.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }
    void updateTimer()
    {
        if(0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }

    void setTextLv()
    {
        textLevel.text = "LV" + Stats.Lv;
    }
}