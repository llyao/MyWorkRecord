using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{


    public static TimeManager Instance;
    public  Action UpdateAction;
    public  Action LateUpdateAction;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateAction!=null)
        UpdateAction();
        ExectTimeWheel();
    }
    void LateUpdate()
    {
        if (LateUpdateAction != null)

            LateUpdateAction();
    }
    #region timewheel 算法实现

    class Timer
    {
        public float dura;
        public bool repeat;
        public float start;
        public Action callback;
    }
    private List<Timer> Timers = new List<Timer>();
   
    private int timeWheelCount = 0;

    float dura01=0f;
    float dura2 =0f;
    public void ExectTimeWheel()
    {
        for (int i = Timers.Count-1; i >=0; i--)
        {
            if (Timers[i].start+Timers[i].dura<=Time.time)
            {
                Timers[i].callback();
                if(Timers[i].repeat)
                {
                    var temp = Timers[i];
                    Timers.RemoveAt(i);
                    AddInvokeRepeating(temp);
                }
                else
                {
                    Timers.RemoveAt(i);

                }
            }
            else
            {
                break;
            }
        }
    }
    public void AddInvoke(float dura, Action callback)
    {

        Timer t = new Timer();
        t.dura = dura;
        t.repeat = false;
        t.start = Time.time;
        t.callback = callback;
        bool find = false;
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            if (Timers[i].dura - (Time.time - Timers[0].start) > dura)
            {
                Timers.Insert(i + 1, t);
                find = true;
                break;
            }
        }
        if (!find)
        {
            Timers.Insert(0, t);
        }

    }

    public void AddInvokeRepeating(float dura, Action callback)
    {
        Timer t = new Timer();
        t.dura = dura;
        t.repeat = true;
        t.start = Time.time;
        t.callback = callback;
        bool find = false;
        for (int i = Timers.Count-1; i>=0 ; i--)
        {
            if (Timers[i].dura - (Time.time - Timers[0].start) > dura)
            {
                Timers.Insert(i+1, t);
                find = true;
                break;
            }
        }
        if (!find)
        {
            Timers.Insert(0,t);
        }

    }
    private void AddInvokeRepeating(Timer t)
    {
        t.start +=t.dura;
        bool find = false;
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            if (Timers[i].dura - (Time.time - Timers[0].start) > t.dura)
            {
                Timers.Insert(i + 1, t);
                find = true;
                break;
            }
        }
        if (!find)
        {
            Timers.Insert(0, t);
        }

    }
    public void CancelInvoke(Action callback)
    {
        Timers.RemoveAll(it=>it.callback==callback);
        
    }

    #endregion
}
