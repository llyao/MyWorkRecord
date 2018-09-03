using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T:new()
{

	private static T m_Instance=default(T);
    private static readonly object syslock = new object();

    public static T Instance
    {
        get
        {
            if(m_Instance==null)
            {
                lock(syslock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new T();
                    }

                }
            }
            return m_Instance;
        }
        
    }

}
