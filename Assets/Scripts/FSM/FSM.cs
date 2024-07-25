using UnityEngine;

public class FSM<T> : MonoBehaviour
{
    //---------------------------------------
    private T owner;    //	상태 소유자..
    private IFSMState<T> currentState = null;   //	현재 상태..
    private IFSMState<T> previousState = null;  //	이전 상태..
                                                //---------------------------------------
    public IFSMState<T> CurrentState { get { return currentState; } }
    public IFSMState<T> PreviousState { get { return previousState; } }
    //---------------------------------------
    //	초기 상태와 상태 소유자 설정..
    protected void InitState(T owner, IFSMState<T> initialState)
    {
        this.owner = owner;
        ChangeState(initialState);
    }
    //---------------------------------------
    //	각 상태의 실시간 처리..
    protected void FSMUpdate() { if (currentState != null) currentState.Execute(owner); }
    //--------------------------------------- 
    //	상태 변경..
    public void ChangeState(IFSMState<T> newState)
    {
        //	이전 상태 교체..
        previousState = currentState;

        //	이전 상태 종료!!
        if (previousState != null)
            previousState.Exit(owner);

        //	현재 상태 교체..
        currentState = newState;

        //	현재 상태 시작!!
        if (currentState != null)
            currentState.Enter(owner);

    }
     //	이전 상태로 전환..
    public void RevertState()
    {
        if (previousState != null)
            ChangeState(previousState);
    }
}