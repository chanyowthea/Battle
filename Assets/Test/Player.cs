using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class Player : NPC
{
    public static Player Instance
    {
        get
        {
            return _Instance;
        }
    }

    public static Player _Instance;

    PlayerStateMachine mMachine = new PlayerStateMachine();

    void Awake()
    {
        _Instance = this;
    }
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

        runSpeed = 160;
    }

    void Update()
    {
        mMachine.Tick();

        this.gameObject.GetComponentInChildren<TextMesh>().text = gInfo.healthCur.ToString();
    }

    public new bool CheckFight()
    {
        // 战斗状态切换
        if (Input.GetKeyDown(KeyCode.J))
        {
            return true;
        }
        return false;
    }

    protected override void OnFightingAct()
    {
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
}
