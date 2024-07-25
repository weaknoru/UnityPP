using UnityEngine;

public class FSM<T> : MonoBehaviour
{
    //---------------------------------------
    private T owner;    //	���� ������..
    private IFSMState<T> currentState = null;   //	���� ����..
    private IFSMState<T> previousState = null;  //	���� ����..
                                                //---------------------------------------
    public IFSMState<T> CurrentState { get { return currentState; } }
    public IFSMState<T> PreviousState { get { return previousState; } }
    //---------------------------------------
    //	�ʱ� ���¿� ���� ������ ����..
    protected void InitState(T owner, IFSMState<T> initialState)
    {
        this.owner = owner;
        ChangeState(initialState);
    }
    //---------------------------------------
    //	�� ������ �ǽð� ó��..
    protected void FSMUpdate() { if (currentState != null) currentState.Execute(owner); }
    //--------------------------------------- 
    //	���� ����..
    public void ChangeState(IFSMState<T> newState)
    {
        //	���� ���� ��ü..
        previousState = currentState;

        //	���� ���� ����!!
        if (previousState != null)
            previousState.Exit(owner);

        //	���� ���� ��ü..
        currentState = newState;

        //	���� ���� ����!!
        if (currentState != null)
            currentState.Enter(owner);

    }
     //	���� ���·� ��ȯ..
    public void RevertState()
    {
        if (previousState != null)
            ChangeState(previousState);
    }
}