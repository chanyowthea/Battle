//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using UnityEngine;


//public class GeneralInfo
//{
//    public int active = 1;
//    public int king = -1;
//    public int city = -1;
//    public int prisonerIdx = -1;
//    public int loyalty = 100;
//    public int[] magic = new int[4];
//    public int job;
//    public int equipment;
//    public int strength;
//    public int intellect;
//    public int experience;
//    public int level;
//    public int healthMax;
//    public int healthCur;
//    public int manaMax;
//    public int manaCur;
//    public int soldierMax;
//    public int soldierCur;
//    public int knightMax;
//    public int knightCur;
//    public int arms;
//    public int armsCur;
//    public int formation;
//    public int formationCur;
//    public int escape;
//}


//public class Player : MonoBehaviour
//{
//    //public WarSceneController.WhichSide side;
//    private Vector3 generalPosBack = new Vector3(600, 0, 0);
//    private Vector3 generalPosFront = new Vector3(120, 0, 0);
//    private Vector3 deadBodyOffset = new Vector3(0, 50, 0);
//    private Vector3 weaponOffset = new Vector3(0, 50, 0);

//    private GeneralInfo gInfo;
//    private exSpriteAnimation head;
//    private exSpriteAnimation body;
//    private exSpriteAnimation weapon;
//    private exSpriteAnimation horse;
//    bool isStopped = false;

//    PlayerStateMachine mMachine = new PlayerStateMachine();

//    int[,] generalBody = new int[255, 2]{
//        {7, 5},		//丁奉
//		{2, 12},		//于禁
//		{2, 15},		//兀突骨
//		{5, 8},		//公孙瓒
//		{7, 5},		//卞喜
//		{7, 15},		//太史慈
//		{7, 6},		//孔岫
//		{0, 11},		//孔融
//		{3, 12},		//文钦
//		{4, 0},		//文聘
//		{6, 3},		//文鸯
//		{7, 11},		//文丑
//		{4, 6},		//牛金
//		{9, 5},		//王允
//		{0, 14},		//王双
//		{9, 16},		//司马炎
//		{3, 3},		//司马昭
//		{4, 6},		//司马师
//		{9, 16},		//司马懿
//		{0, 15},		//甘宁
//		{7, 4},		//田丰
//		{6, 6},		//伊籍
//		{1, 15},		//全琮
//		{7, 11},		//忙牙长
//		{9, 11},		//朱桓
//		{1, 16},		//朵思大王
//		{7, 12},		//吴兰
//		{1, 12},		//吴懿
//		{0, 9},		//吕布
//		{1, 16},		//吕蒙
//		{9, 5},		//宋宪
//		{6, 6},		//李典
//		{1, 0},		//李恢
//		{6, 14},		//李儒
//		{0, 4},		//李严
//		{5, 6},		//李傕
//		{0, 2},		//步鹭
//		{3, 16},		//沙摩诃
//		{9, 3},		//车胄
//		{0, 6},		//邢道荣
//		{7, 4},		//典韦
//		{4, 5},		//周仓
//		{1, 4},		//周泰
//		{4, 16},		//周瑜
//		{6, 0},		//孟达
//		{4, 0},		//孟优
//		{7, 12},		//孟获
//		{5, 14},		//法正
//		{3, 16},		//沮授
//		{8, 4},		//金环三结
//		{1, 11},		//阿会喃
//		{4, 12},		//姜维
//		{7, 12},		//纪灵
//		{5, 6},		//胡车儿
//		{8, 4},		//凌统
//		{4, 5},		//凌操
//		{7, 15},		//夏侯惇
//		{1, 14},		//夏侯渊
//		{6, 5},		//夏侯霸
//		{4, 3},		//孙坚
//		{3, 14},		//孙策
//		{6, 14},		//孙权
//		{0, 0},		//孙翊
//		{1, 2},		//徐晃
//		{3, 16},		//徐庶
//		{1, 7},		//徐盛
//		{7, 6},		//徐质
//		{1, 1},		//祝融夫人
//		{9, 16},		//荀攸
//		{9, 16},		//荀彧
//		{4, 1},		//袁尚
//		{2, 7},		//袁绍
//		{2, 1},		//袁术
//		{6, 5},		//袁熙
//		{0, 3},		//袁谭
//		{6, 15},		//郝昭
//		{4, 4},		//马良
//		{3, 0},		//马岱
//		{4, 12},		//马超
//		{0, 4},		//马腾
//		{9, 0},		//马谡
//		{9, 12},		//高顺
//		{3, 5},		//高览
//		{4, 15},		//张任
//		{3, 12},		//张松
//		{8, 16},		//张虎
//		{7, 12},		//张昭
//		{9, 8},		//张苞
//		{6, 15},		//张郃
//		{7, 10},		//张飞
//		{2, 7},		//张鲁
//		{5, 11},		//张辽
//		{6, 14},		//张纮
//		{4, 14},		//曹仁
//		{8, 5},		//曹芳
//		{3, 14},		//曹爽
//		{9, 7},		//曹植
//		{9, 8},		//曹彰
//		{2, 3},		//曹操
//		{7, 11},		//曹睿
//		{4, 12},		//许褚
//		{9, 5},		//逢纪
//		{9, 16},		//郭嘉
//		{6, 12},		//郭图
//		{0, 7},		//郭汜
//		{7, 15},		//陈宫
//		{8, 7},		//陈琳
//		{6, 8},		//陈群
//		{1, 11},		//陆抗
//		{2, 16},		//陆逊
//		{0, 16},		//陶谦
//		{2, 1},		//程昱
//		{7, 0},		//程普
//		{4, 14},		//华雄
//		{6, 7},		//华歆
//		{6, 7},		//黄忠
//		{8, 7},		//黄祖
//		{5, 2},		//黄盖
//		{1, 15},		//黄权
//		{7, 0},		//杨修
//		{7, 4},		//董允
//		{7, 15},		//董卓
//		{5, 14},		//董荼那
//		{2, 2},		//贾充
//		{9, 16},		//贾诩
//		{3, 6},		//廖化
//		{7, 5},		//满宠
//		{5, 8},		//赵统
//		{5, 5},		//赵云
//		{5, 0},		//赵广
//		{9, 2},		//蒯良
//		{3, 8},		//蒯越
//		{3, 2},		//刘表
//		{2, 0},		//刘焉
//		{2, 2},		//刘备
//		{6, 0},		//刘晔
//		{9, 0},		//刘禅
//		{1, 0},		//刘繇
//		{9, 6},		//樊稠
//		{4, 5},		//乐进
//		{7, 5},		//潘璋
//		{6, 7},		//蒋济
//		{5, 1},		//蒋琬
//		{6, 14},		//蔡邕
//		{9, 6},		//蔡瑁
//		{9, 16},		//诸葛亮
//		{8, 0},		//诸葛恪
//		{8, 8},		//诸葛瑾
//		{9, 16},		//鲁肃
//		{1, 1},		//邓艾
//		{4, 12},		//邓忠
//		{2, 15},		//邓芝
//		{0, 11},		//卢植
//		{2, 1},		//阎圃
//		{3, 12},		//钟会
//		{3, 3},		//韩当
//		{2, 11},		//韩馥
//		{4, 14},		//颜良
//		{7, 15},		//魏延
//		{1, 14},		//魏续
//		{1, 12},		//庞统
//		{7, 11},		//庞德
//		{2, 0},		//谯周
//		{5, 5},		//关平
//		{4, 13},		//关羽
//		{6, 12},		//关索
//		{0, 11},		//关兴
//		{2, 8},		//严白虎
//		{8, 15},		//严纲
//		{9, 6},		//严舆
//		{7, 15},		//严颜
//		{8, 1},		//公孙越
//		{5, 15},		//王朗
//		{4, 0},		//朱治
//		{7, 6},		//辛评
//		{9, 14},		//武安国
//		{4, 0},		//皇甫嵩
//		{1, 6},		//孙乾
//		{3, 4},		//祖茂
//		{7, 15},		//马玩
//		{5, 2},		//高沛
//		{8, 5},		//张勋
//		{3, 1},		//张济
//		{8, 6},		//曹洪
//		{0, 3},		//梁兴
//		{9, 3},		//陈武
//		{4, 7},		//陈登
//		{8, 16},		//陈横
//		{0, 3},		//乔玄
//		{5, 3},		//乔瑁
//		{1, 0},		//关凤
//		{9, 11},		//杨怀
//		{0, 0},		//虞翻
//		{4, 11},		//邹靖
//		{3, 16},		//雷铜
//		{4, 6},		//雷薄
//		{9, 12},		//刘璋
//		{2, 0},		//潘凤
//		{9, 0},		//霍峻
//		{8, 1},		//糜竺
//		{5, 4},		//糜芳
//		{1, 3},		//韩嵩
//		{5, 11},		//简雍
//		{0, 0},		//阚泽
//		{3, 14},		//曹丕
//		{1, 0},		//貂蝉
//		{1, 0},		//孙尚香
//		{1, 11},		//何进
//		{6, 12},		//张英
//		{0, 2},		//丁原
//		{2, 3},		//张角
//		{6, 0},		//张梁
//		{1, 7},		//张宝
//		{4, 16},		//程远志
//		{0, 15},		//邓茂
//		{5, 0},		//管亥
//		{1, 0},		//赵弘
//		{4, 0},		//韩忠
//		{5, 2},		//龚都
//		{4, 11},		//何仪
//		{9, 3},		//龚景
//		{1, 11},		//曹真
//		{6, 3},		//刘封
//		{9, 16},		//董承
//		{1, 0},		//董袭
//		{9, 5},		//张闿
//		{8, 3},		//张翼
//		{0, 4},		//张嶷
//		{7, 3},		//彻里吉
//		{2, 2},		//臧霸
//		{0, 14},		//徐荣
//		{9, 16},		//夏侯恩
//		{2, 1},		//淳于琼
//		{6, 0},		//曹休
//		{0, 8},		//曹纯
//		{2, 0},		//孙韶
//		{1, 1},		//金旋
//		{9, 0},		//公孙康
//		{6, 0},		//向朗
//		{1, 0},		//吕范
//		{4, 12},		//李异
//		{2, 5},		//夏侯尚
//		{5, 16},		//于吉
//		{0, 16},		//左慈
//		{1, 6},		//孙静
//		{2, 3},		//桓范
//		{1, 11},		//费祎
//		{4, 12},		//轲比能
//		{0, 14},		//董旻
//		{9, 2},		//刘琦
//		{0, 15},		//刘琮
//		{1, 6},		//蒋钦
//		{5, 4},		//苏飞
//		{9, 8},		//谭雄
//		{4, 11},		//顾雍
		
//	};

//    public int gIdx = 1;
//    // Use this for initialization
//    void Start()
//    {
//        gInfo = new GeneralInfo();
//        gInfo.healthCur = 100;
//        gInfo.healthMax = 100;

//        //gInfo = Informations.Instance.GetGeneralInfo(gIdx);
//        GeneralInit(gIdx);

//        mMachine.Init(this);
//    }

//    public int horseIdx = 0;
//    void GeneralInit(int gIdx=0)
//    {
//        //if (gInfo.equipment > 26)
//        //{
//        //    horseIdx = gInfo.equipment - 26;
//        //}

//        string str = "";
//        GameObject go;

//        //if (side == WarSceneController.WhichSide.Left)
//            if(true)
//        {
//            str = "Generals/HorseRed/";
//        }
//        else
//        {
//            str = "Generals/HorseGreen/";
//        }
//        str += "Horse00" + (horseIdx + 1);
//        go = (GameObject)Instantiate(Resources.Load(str));
//        horse = go.GetComponent<exSpriteAnimation>();

//        //if (side == WarSceneController.WhichSide.Left)
//            if (true)
//            {
//            str = "Generals/HeadRed/";
//        }
//        else
//        {
//            str = "Generals/HeadGreen/";
//        }

//        //int[,] generalBody = this.generalBody;
//        if (generalBody[gIdx, 0] < 9)
//        {
//            str += "Head0" + (generalBody[gIdx, 0] + 1);
//        }
//        else
//        {
//            str += "Head" + (generalBody[gIdx, 0] + 1);
//        }
//        go = (GameObject)Instantiate(Resources.Load(str));
//        head = go.GetComponent<exSpriteAnimation>();

//        //if (side == WarSceneController.WhichSide.Left)
//            if (true)
//            {
//            str = "Generals/BodyRed/";
//        }
//        else
//        {
//            str = "Generals/BodyGreen/";
//        }
//        if (generalBody[gIdx, 1] < 9)
//        {
//            str += "Body0" + (generalBody[gIdx, 1] + 1);
//        }
//        else
//        {
//            str += "Body" + (generalBody[gIdx, 1] + 1);
//        }
//        go = (GameObject)Instantiate(Resources.Load(str));
//        body = go.GetComponent<exSpriteAnimation>();

//        if (generalBody[gIdx, 1] < 9)
//        {
//            str = "Generals/Weapon/Weapon0" + (generalBody[gIdx, 1] + 1);
//        }
//        else
//        {
//            str = "Generals/Weapon/Weapon" + (generalBody[gIdx, 1] + 1);
//        }
//        go = (GameObject)Instantiate(Resources.Load(str));
//        weapon = go.GetComponent<exSpriteAnimation>();

//        head.transform.parent = transform;
//        body.transform.parent = transform;
//        weapon.transform.parent = transform;
//        horse.transform.parent = transform;

//        head.transform.localPosition = Vector3.zero;
//        body.transform.localPosition = Vector3.zero;
//        weapon.transform.localPosition = new Vector3(0, 0, -5);
//        horse.transform.localPosition = new Vector3(0, 0, 5f);

//        head.transform.localScale = Vector3.one;
//        body.transform.localScale = Vector3.one;
//        weapon.transform.localScale = Vector3.one;
//        horse.transform.localScale = Vector3.one;

//        head.transform.localRotation = Quaternion.identity;
//        body.transform.localRotation = Quaternion.identity;
//        weapon.transform.localRotation = Quaternion.identity;
//        horse.transform.localRotation = Quaternion.identity;
//    }

//    float runSpeed = 80;
//    Vector3 dir;

//    public void SetAttackFront()
//    {
//        int rand = Random.Range(0, 100);
//        int type = rand / 25;

//        if (type == 3)
//        {
//        }

//        string anim = "AttackFront" + (type + 1);

//        head.Play(anim);
//        body.Play(anim);
//        horse.Play("Fight");
//        //head.GetCurrentAnimation().speed = animSpeed;
//        //body.GetCurrentAnimation().speed = animSpeed;
//        //horse.GetCurrentAnimation().speed = animSpeed;
//        SoundController.Instance.PlaySound3D("00033", transform.position);


//        OnFightingAct();
//    }

//    public float GetAnimTime()
//    {
//        var origin_time = (1 / 6f * 4f);
//        var speed = head.GetCurrentAnimation().speed / 1.5f;
//        return origin_time / speed;
//    }

//    public void EnterRun()
//    {
//        if (!head.IsPlaying("Run"))
//        {
//            head.Play("Run");
//            body.Play("Run");
//            horse.Play("Run");
//        }
//        SoundController.Instance.PlaySound3D("00021", transform.position);
//    }

//    void Update()
//    {
//        mMachine.Tick();
//    }

//    public bool CheckFight()
//    {
//        // 战斗状态切换
//        if (Input.GetKeyDown(KeyCode.J))
//        {
//            return true;
//        }
//        return false;
//    }

//    public void EnterIdle()
//    {
//        head.Play("Idle");
//        body.Play("Idle");
//        horse.Play("Idle");
//    }

//    public void SetMoveDir(Vector2 d)
//    {
//        dir = d;

//        if (dir.x == 0)
//        {
//            return;
//        }

//        var s = this.transform.localScale;
//        s.x = 0.5f * Mathf.Sign(-dir.x);
//        this.transform.localScale = s;
//    }

//    public void TickMove()
//    {
//        transform.localPosition = new Vector3(transform.localPosition.x + dir.x * runSpeed * Time.deltaTime, transform.localPosition.y + dir.y * runSpeed * Time.deltaTime, transform.localPosition.z);
//    }

//    private bool isMightyHit;
//    void OnFightingAct()
//    {
//        int damage = 0;
//        if (!isMightyHit)
//        {
//            damage = Random.Range(gInfo.strength / 12 - 3, gInfo.strength / 12 + 3);
//        }
//        else
//        {
//            damage = Random.Range(gInfo.strength / 12, gInfo.strength / 12 + 5);
//        }

//        if (CheckInRange(new Vector3(transform.position.x + 10, transform.position.y)))
//        {
//            OnDamage(damage);
//        }
//    }

//    bool CheckInRange(Vector3 enemy_pos)
//    {
//        int gap = 12;
//        var pos = transform.localPosition;
//        if ((pos.x <= enemy_pos.x && enemy_pos.x < pos.x + gap * 4)
//            && (pos.y <= enemy_pos.y && enemy_pos.y < pos.y + gap * 4))
//        { 
//            return true;
//        }
//        return false;
//    }

//    bool OnDamage(int d, int type=0)
//    {
//        Debug.LogError(d);
//        return false;
//    }
//}
