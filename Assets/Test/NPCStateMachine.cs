using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENPCState
{ 
    Idle,
    Run,
    Fight,
    Die, 
}

public class BaseNPCState
{
    protected NPC mPlayer;
    protected NPCStateMachine mMachine;
    public BaseNPCState(NPCStateMachine machine, NPC player)
    {
        mPlayer = player;
        mMachine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
}

public class NPCStateIdle:BaseNPCState
{
    Vector2 dir;

    public NPCStateIdle(NPCStateMachine machine, NPC player) : base(machine,player) { }

    public override void Enter()
    {
        base.Enter();
        mPlayer.EnterIdle();
    }

    public override void Tick()
    {
        base.Tick();

        if (mPlayer.CheckFight())
        {
            mMachine.Switch(ENPCState.Fight);
            return;
        }

        var p = Player.Instance.transform.position;
        //dir = p - mPlayer.transform.position;
        //mPlayer.SetMoveDir(dir.normalized);
        if (Vector2.Distance(p, mPlayer.transform.position) < mPlayer.VisualRadius)
        { 
            mMachine.Switch(ENPCState.Run);
        }
    }
}
public class NPCStateRun : BaseNPCState
{
    Vector2 dir;
    float mTime = 0.6f * 0.5f;
    float mElapsedTime = 0;

    public NPCStateRun(NPCStateMachine machine, NPC player) : base(machine, player) { }

    public override void Enter()
    {
        base.Enter();
        mPlayer.EnterRun();
        mElapsedTime = 0;
        mTime = mPlayer.GetAnimTime();
    }

    public override void Tick()
    {
        base.Tick();

        if (mPlayer.CheckFight())
        {
            mMachine.Switch(ENPCState.Fight);
            return;
        }

        //if (Player.Instance.HP <= 0)
        //{
        //    mMachine.Switch(ENPCState.Idle);
        //    return;
        //}

        var npc_pos = mPlayer.transform.position;
        var target_pos = Player.Instance.transform.position;


        int gap = 12;
        //if (Vector3.Distance(npc_pos, target_pos) > gap * 4)
        {
            //var sign = Mathf.Sign(npc_pos.x - target_pos.x);
            //target_pos = new Vector3(target_pos.x + gap * 3 * sign, target_pos.y, 0);
            dir = target_pos - npc_pos;

            Debug.DrawLine(npc_pos, target_pos);

            mPlayer.SetMoveDir(dir.normalized);

            mPlayer.TickMove();
        }

        mElapsedTime += Time.deltaTime;
        if (mElapsedTime >= mTime)
        {
            mPlayer.EnterRun();
            mElapsedTime = 0;
        }
    }
}
public class NPCStateFight : BaseNPCState
{
    public NPCStateFight(NPCStateMachine machine, NPC player) : base(machine, player) { }
    float mTime = 0.6f * 0.5f;
    float mElapsedTime = 0;

    public override void Enter()
    {
        base.Enter();
        mPlayer.SetAttackFront();
        mElapsedTime = 0;
    }

    public override void Tick()
    {
        base.Tick();
        mElapsedTime += Time.deltaTime;
        if (mElapsedTime >= mTime)
        {
            mMachine.Switch(ENPCState.Idle);
        }
    }
}

public class NPCStateMachine
{
    Dictionary<ENPCState, BaseNPCState> mStates = new Dictionary<ENPCState, BaseNPCState>();
    NPC mPlayer; 
    BaseNPCState mCurState;

    public void Init(NPC player)
    {
        mPlayer = player;

        mStates.Add(ENPCState.Idle, new NPCStateIdle(this,mPlayer));
        mStates.Add(ENPCState.Run, new NPCStateRun(this, mPlayer));
        mStates.Add(ENPCState.Fight, new NPCStateFight(this, mPlayer));
        mCurState = mStates[ENPCState.Idle];
    }

    public void Switch(ENPCState state)
    {
        mCurState.Exit();
        mCurState = mStates[state];
        mCurState.Enter();
    }

    public void Tick()
    {
        mCurState.Tick();
    }
}
