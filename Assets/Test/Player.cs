using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GeneralInfo
{
    public int strength;
    public int healthMax;
    public int healthCur;
}

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get
        {
            return _Instance;
        }
    }

    public static Player _Instance;

    //public WarSceneController.WhichSide side;
    private Vector3 generalPosBack = new Vector3(600, 0, 0);
    private Vector3 generalPosFront = new Vector3(120, 0, 0);
    private Vector3 deadBodyOffset = new Vector3(0, 50, 0);
    private Vector3 weaponOffset = new Vector3(0, 50, 0);

    private GeneralInfo gInfo;
    private exSpriteAnimation head;
    private exSpriteAnimation body;
    private exSpriteAnimation weapon;
    private exSpriteAnimation horse;
    bool isStopped = false;

    public int HP
    {
        set
        {
            gInfo.healthCur = value;
        }
        get
        {
            return gInfo.healthCur;
        }
    }

    PlayerStateMachine mMachine = new PlayerStateMachine();

    int[,] generalBody = new int[2, 2]{
        {7, 5},		//丁奉
		{2, 12}		//于禁
	};

    public int gIdx = 1;
    // Use this for initialization

    void Awake()
    {
        _Instance = this;
    }

    //public bool CheckFight()
    //{
    //    var npcs = NPCManager.Instance.GetNpcs();
    //    for (int i = 0; i < npcs.Count; i++)
    //    {
    //        var item = npcs[i];
    //        var pos = item.transform.position;
    //        if (CheckInRange(new Vector3(pos.x, pos.y)))
    //            return true;
    //    }
    //    return false;
    //}

    void Start()
    {
        gInfo = new GeneralInfo();
        gInfo.strength = 90;
        gInfo.healthCur = 100;
        gInfo.healthMax = 100;

        //gInfo = Informations.Instance.GetGeneralInfo(gIdx);
        GeneralInit(0, UnityEngine.Random.Range(1, 6),
            UnityEngine.Random.Range(1, 11),
            UnityEngine.Random.Range(1, 11),
            UnityEngine.Random.Range(1, 18));

        mMachine.Init(this);
    }

    // horse_id [001,005]
    // head_id [01,10]
    // body_id [01,10]
    // weapon_id [01,17]
    void GeneralInit(int gIdx = 0, int horse_id = 1, int head_id = 1, int body_id = 1, int weapon_id = 1)
    {
        string str = "";
        GameObject go;
        str = "Generals/HorseGreen/";
        str += "Horse00" + horse_id;
        go = (GameObject)Instantiate(Resources.Load(str));
        horse = go.GetComponent<exSpriteAnimation>();
        str = "Generals/HeadGreen/";
        str += "Head" + head_id.ToString("00");
        go = (GameObject)Instantiate(Resources.Load(str));
        head = go.GetComponent<exSpriteAnimation>();
        str = "Generals/BodyGreen/";

        str += "Body" + body_id.ToString("00");
        go = (GameObject)Instantiate(Resources.Load(str));
        body = go.GetComponent<exSpriteAnimation>();
        str = "Generals/Weapon/Weapon" + weapon_id.ToString("00");
        go = (GameObject)Instantiate(Resources.Load(str));
        weapon = go.GetComponent<exSpriteAnimation>();

        head.transform.parent = transform;
        body.transform.parent = transform;
        weapon.transform.parent = transform;
        horse.transform.parent = transform;

        head.transform.localPosition = Vector3.zero;
        body.transform.localPosition = Vector3.zero;
        weapon.transform.localPosition = new Vector3(0, 0, -5);
        horse.transform.localPosition = new Vector3(0, 0, 5f);

        head.transform.localScale = Vector3.one;
        body.transform.localScale = Vector3.one;
        weapon.transform.localScale = Vector3.one;
        horse.transform.localScale = Vector3.one;

        head.transform.localRotation = Quaternion.identity;
        body.transform.localRotation = Quaternion.identity;
        weapon.transform.localRotation = Quaternion.identity;
        horse.transform.localRotation = Quaternion.identity;
    }

    float runSpeed = 80;
    Vector3 dir;

    public void SetAttackFront()
    {
        int rand = Random.Range(0, 100);
        int type = rand / 25;

        if (type == 3)
        {
        }

        string anim = "AttackFront" + (type + 1);

        head.Play(anim);
        body.Play(anim);
        horse.Play("Fight");
        //head.GetCurrentAnimation().speed = animSpeed;
        //body.GetCurrentAnimation().speed = animSpeed;
        //horse.GetCurrentAnimation().speed = animSpeed;
        SoundController.Instance.PlaySound3D("00033", transform.position);


        OnFightingAct();
    }

    public float GetAnimTime()
    {
        var origin_time = (1 / 6f * 4f);
        var speed = head.GetCurrentAnimation().speed / 1.5f;
        return origin_time / speed;
    }

    public void EnterRun()
    {
        if (!head.IsPlaying("Run"))
        {
            head.Play("Run");
            body.Play("Run");
            horse.Play("Run");
        }
        SoundController.Instance.PlaySound3D("00021", transform.position);
    }

    void Update()
    {
        mMachine.Tick();

        this.gameObject.GetComponentInChildren<TextMesh>().text = gInfo.healthCur.ToString();
    }

    public bool CheckFight()
    {
        // 战斗状态切换
        if (Input.GetKeyDown(KeyCode.J))
        {
            return true;
        }
        return false;
    }

    public void EnterIdle()
    {
        head.Play("Idle");
        body.Play("Idle");
        horse.Play("Idle");
    }

    public void SetMoveDir(Vector2 d)
    {
        dir = d;

        if (dir.x == 0)
        {
            return;
        }

        var s = this.transform.localScale;
        s.x = 0.5f * Mathf.Sign(-dir.x);
        this.transform.localScale = s;
    }

    public void TickMove()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + dir.x * runSpeed * Time.deltaTime, transform.localPosition.y + dir.y * runSpeed * Time.deltaTime, transform.localPosition.z);
    }

    private bool isMightyHit;
    void OnFightingAct()
    {
        //OnDamage(damage);

        NPC target_npc = null;
        var npcs = NPCManager.Instance.GetNpcs();
        for (int i = 0; i < npcs.Count; i++)
        {
            var item = npcs[i];
            var list = item.GetBodyRect();
            for (int j = 0; j < list.Count; j++)
            {
                var pos = list[j];
                if (CheckInRange(pos))
                {
                    target_npc = item;
                    break;
                }
            }
        }
        if (target_npc == null)
        {
            return;
        }

        int damage = 0;
        if (!isMightyHit)
        {
            damage = Random.Range(gInfo.strength / 12 - 3, gInfo.strength / 12 + 3);
        }
        else
        {
            damage = Random.Range(gInfo.strength / 12, gInfo.strength / 12 + 5);
        }
        OnDamage(damage,0,target_npc);
    }

    bool IsDirLeft()
    {
        return this.transform.localScale.x > 0;
    }
    public bool CheckInRange(Vector3 enemy_pos)
    {
        var s = this.transform.localScale;
        var sign = Mathf.Sign(-s.x);
        int gap = (int)(12 * sign);
        int gapy = Mathf.Abs(gap);
        var pos = transform.localPosition;
        if (IsDirLeft())
        {
            if ((pos.x >= enemy_pos.x && enemy_pos.x > pos.x + gap * 3)
                && (pos.y <= enemy_pos.y && enemy_pos.y < pos.y + gapy * 1))
            {
                return true;
            }
        }
        if ((pos.x <= enemy_pos.x && enemy_pos.x < pos.x + gap * 3)
            && (pos.y <= enemy_pos.y && enemy_pos.y < pos.y + gapy * 1))
        {
            return true;
        }
        return false;
    }

    bool OnDamage(int d, int type = 0, NPC target_npc = null)
    {
        if (target_npc == null)
        {
            return false;
        }

        Debug.Log(d);
        //gInfo.healthCur -= d;
        target_npc.HP -= d;
        return true;
    }

    public List<Vector2> GetBodyRect()
    {
        int gap = 12;
        var pos_new = transform.localPosition;
        var pos_list = new List<Vector2>();

        // 起点
        pos_new.x -= gap * 1;
        pos_list.Add(pos_new);

        pos_new.y += gap * 1;
        pos_list.Add(pos_new);

        pos_new.x += gap * 2;
        pos_list.Add(pos_new);

        pos_new.y -= gap * 1;
        pos_list.Add(pos_new);

        return pos_list;
    }
    void DrawBody()
    {
        int gap = 12;
        var pos = transform.localPosition;
        var pos_new = pos;
        var pos_list = new List<Vector2>();

        // 起点
        pos_new.x -= gap * 1;
        pos_list.Add(pos_new);

        pos_new.y += gap * 1;
        pos_list.Add(pos_new);

        pos_new.x += gap * 2;
        pos_list.Add(pos_new);

        pos_new.y -= gap * 1;
        pos_list.Add(pos_new);

        pos_new.x -= gap * 2;
        pos_list.Add(pos_new);

        for (int i = 0; i < pos_list.Count - 1; i++)
        {
            var p = pos_list[i];
            Debug.DrawLine(p, pos_list[i + 1], Color.red);
        }
    }

    protected void OnDrawGizmos()
    {
        var s = this.transform.localScale;
        var sign = Mathf.Sign(-s.x);
        int gap = (int)(12 * sign);
        int gapy = Mathf.Abs(gap);
        var pos = transform.localPosition;
        var pos_new = pos;
        var pos_list = new List<Vector2>();
        pos_list.Add(pos_new);

        pos_new.y += gapy * 1;
        pos_list.Add(pos_new);

        pos_new.x += gap * 3;
        pos_list.Add(pos_new);

        pos_new.y -= gapy * 1;
        pos_list.Add(pos_new);

        pos_new.x -= gap * 3;
        pos_list.Add(pos_new);

        //if ((pos.x <= enemy_pos.x && enemy_pos.x < pos.x + gap * 4)
        //    && (pos.y <= enemy_pos.y && enemy_pos.y < pos.y + gap * 4))

        for (int i = 0; i < pos_list.Count - 1; i++)
        {
            var p = pos_list[i];
            Debug.DrawLine(p, pos_list[i + 1], Color.blue);
        }

        DrawBody();
    }
}
