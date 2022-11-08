/// <summary>
/// Generic Mono singleton.
/// </summary>
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{
	
	private static T m_Instance = null;
    
    // 在设计阶段 写脚本 没有挂载物体上 希望脚本 单例模式
	public static T instance{
        get{
			if( m_Instance == null ){
                // 从场景中查找
            	m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if( m_Instance == null ){
                    // 1. 直接创建一个游戏对象
                    // 2. 为这个游戏对象添加脚本组件
                    m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    // 初始化
					 m_Instance.Init();
                }
               
            }
            return m_Instance;
        }
    }

    // 设计阶段，写脚本，挂在物体上
    // 项目运行时【系统会帮助我们将脚本类实例化为对象（new 脚本）】
    // 项目运行时 在Awake时，在场景中找到唯一实例，记录在m_Instance中
    private void Awake(){
   
        if( m_Instance == null ){
            m_Instance = this as T;
        }
    }
 
    // 提供初始化的一种选择，可以使用Init、Start都可以
    public virtual void Init(){}
 
    // 当程序退出做清理工作！ 单例模式 = null
    private void OnApplicationQuit(){
        m_Instance = null;
    }
}