using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerState
{ 
    Idle,
    Run,
    Fight,
    Die, 
}

public class BasePlayerState
{
    protected Player mPlayer;
    protected PlayerStateMachine mMachine;
    public BasePlayerState(PlayerStateMachine machine, Player player)
    {
        mPlayer = player;
        mMachine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
}

public class PlayerStateIdle:BasePlayerState
{
    Vector2 dir;

    public PlayerStateIdle(PlayerStateMachine machine, Player player) : base(machine,player) { }

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
            mMachine.Switch(EPlayerState.Fight);
            return;
        }

        // 移动状态
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (h == 0 && v == 0) { return; }
        dir.x = h;
        dir.y = v;
        mPlayer.SetMoveDir(dir.normalized);
        mMachine.Switch(EPlayerState.Run);
    }
}
public class PlayerStateRun : BasePlayerState
{
    Vector2 dir;
    float mTime = 0.6f * 0.5f;
    float mElapsedTime = 0;

    public PlayerStateRun(PlayerStateMachine machine, Player player) : base(machine, player) { }

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
            mMachine.Switch(EPlayerState.Fight);
            return;
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (h == 0 && v == 0)
        {
            mMachine.Switch(EPlayerState.Idle);
            return;
        }
        dir.x = h;
        dir.y = v;
        mPlayer.SetMoveDir(dir.normalized);

        mPlayer.TickMove();


        mElapsedTime += Time.deltaTime;
        if (mElapsedTime >= mTime)
        {
            mPlayer.EnterRun();
            mElapsedTime = 0;
        }
    }
}
public class PlayerStateFight : BasePlayerState
{
    public PlayerStateFight(PlayerStateMachine machine, Player player) : base(machine, player) { }
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
            mMachine.Switch(EPlayerState.Idle);
        }
    }
}

public class PlayerStateMachine {
    Dictionary<EPlayerState, BasePlayerState> mStates = new Dictionary<EPlayerState, BasePlayerState>();
    Player mPlayer; 
    BasePlayerState mCurState;

    public void Init(Player player)
    {
        mPlayer = player;

        mStates.Add(EPlayerState.Idle, new PlayerStateIdle(this,mPlayer));
        mStates.Add(EPlayerState.Run, new PlayerStateRun(this, mPlayer));
        mStates.Add(EPlayerState.Fight, new PlayerStateFight(this, mPlayer));
        mCurState = mStates[EPlayerState.Idle];
    }

    public void Switch(EPlayerState state)
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
