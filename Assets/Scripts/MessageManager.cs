using System;
using System.Collections.Generic;

public class MessageManager
{
    public delegate void funsig();

    public delegate void funsig<T>(T t1);

    public delegate void funsig<T1, T2>(T1 t1, T2 t2);

    public delegate void funsig<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    public delegate void funsig<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

    public delegate void funsig<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

    public delegate T funsigret<T>();

    public delegate T funsigret<T, T1>(T1 t1);

    public delegate T funsigret<T, T1, T2>(T1 t1, T2 t2);

    public delegate T funsigret<T, T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    public delegate T funsigret<T, T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

    public delegate T funsigret<T, T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

    public enum Event
    {
        COIN_UPDATE,
    }

    public static MessageManager instance = new MessageManager();

    private List<Delegate> _msgMap;

    public MessageManager()
    {
        this._msgMap = new List<Delegate>();
        for (int i = 0; i < 1; i++)
        {
            this._msgMap.Add(null);
        }
    }

    public void Dispose()
    {
        this._msgMap.Clear();
    }

    public void Subscribe(MessageManager.Event signal, Delegate listener)
    {
        if (listener == null)
        {
            return;
        }
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            @delegate = Delegate.Combine(@delegate, listener);
        }
        else
        {
            @delegate = listener;
        }
        this._msgMap[(int)signal] = @delegate;
    }

    public void Unsubscribe(MessageManager.Event signal, Delegate listener)
    {
        if (listener == null)
        {
            return;
        }
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            @delegate = Delegate.RemoveAll(@delegate, listener);
        }
        this._msgMap[(int)signal] = @delegate;
    }

    public void Publish(MessageManager.Event signal)
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsig funsig = (MessageManager.funsig)@delegate;
            funsig();
        }
    }

    public void Publish<T1>(MessageManager.Event signal, T1 args)
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsig<T1> funsig = (MessageManager.funsig<T1>)@delegate;
            funsig(args);
        }
    }

    public void Publish<T1, T2>(MessageManager.Event signal, T1 arg1, T2 arg2)
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsig<T1, T2> funsig = (MessageManager.funsig<T1, T2>)@delegate;
            funsig(arg1, arg2);
        }
    }

    public void Publish<T1, T2, T3>(MessageManager.Event signal, T1 arg1, T2 arg2, T3 arg3)
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsig<T1, T2, T3> funsig = (MessageManager.funsig<T1, T2, T3>)@delegate;
            funsig(arg1, arg2, arg3);
        }
    }

    public void Publish<T1, T2, T3, T4>(MessageManager.Event signal, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsig<T1, T2, T3, T4> funsig = (MessageManager.funsig<T1, T2, T3, T4>)@delegate;
            funsig(arg1, arg2, arg3, arg4);
        }
    }

    public void Publish<T1, T2, T3, T4, T5>(MessageManager.Event signal, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsig<T1, T2, T3, T4, T5> funsig = (MessageManager.funsig<T1, T2, T3, T4, T5>)@delegate;
            funsig(arg1, arg2, arg3, arg4, arg5);
        }
    }

    public T PublishRet<T>(MessageManager.Event signal) where T : new()
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsigret<T> funsigret = (MessageManager.funsigret<T>)@delegate;
            return funsigret();
        }
        return Activator.CreateInstance<T>();
    }

    public T PublishRet<T, T1>(MessageManager.Event signal, T1 args) where T : new()
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsigret<T, T1> funsigret = (MessageManager.funsigret<T, T1>)@delegate;
            return funsigret(args);
        }
        return Activator.CreateInstance<T>();
    }

    public T PublishRet<T, T1, T2>(MessageManager.Event signal, T1 arg1, T2 arg2) where T : new()
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsigret<T, T1, T2> funsigret = (MessageManager.funsigret<T, T1, T2>)@delegate;
            return funsigret(arg1, arg2);
        }
        return Activator.CreateInstance<T>();
    }

    public T PublishRet<T, T1, T2, T3>(MessageManager.Event signal, T1 arg1, T2 arg2, T3 arg3) where T : new()
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsigret<T, T1, T2, T3> funsigret = (MessageManager.funsigret<T, T1, T2, T3>)@delegate;
            return funsigret(arg1, arg2, arg3);
        }
        return Activator.CreateInstance<T>();
    }

    public T PublishRet<T, T1, T2, T3, T4>(MessageManager.Event signal, T1 arg1, T2 arg2, T3 arg3, T4 arg4) where T : new()
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsigret<T, T1, T2, T3, T4> funsigret = (MessageManager.funsigret<T, T1, T2, T3, T4>)@delegate;
            return funsigret(arg1, arg2, arg3, arg4);
        }
        return Activator.CreateInstance<T>();
    }

    public T PublishRet<T, T1, T2, T3, T4, T5>(MessageManager.Event signal, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) where T : new()
    {
        Delegate @delegate = this._msgMap[(int)signal];
        if (@delegate != null)
        {
            MessageManager.funsigret<T, T1, T2, T3, T4, T5> funsigret = (MessageManager.funsigret<T, T1, T2, T3, T4, T5>)@delegate;
            return funsigret(arg1, arg2, arg3, arg4, arg5);
        }
        return Activator.CreateInstance<T>();
    }
}
