/// <summary>
/// Generic Mono singleton.
/// </summary>
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{
	
	private static T m_Instance = null;
    
    // ����ƽ׶� д�ű� û�й��������� ϣ���ű� ����ģʽ
	public static T instance{
        get{
			if( m_Instance == null ){
                // �ӳ����в���
            	m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if( m_Instance == null ){
                    // 1. ֱ�Ӵ���һ����Ϸ����
                    // 2. Ϊ�����Ϸ������ӽű����
                    m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    // ��ʼ��
					 m_Instance.Init();
                }
               
            }
            return m_Instance;
        }
    }

    // ��ƽ׶Σ�д�ű�������������
    // ��Ŀ����ʱ��ϵͳ��������ǽ��ű���ʵ����Ϊ����new �ű�����
    // ��Ŀ����ʱ ��Awakeʱ���ڳ������ҵ�Ψһʵ������¼��m_Instance��
    private void Awake(){
   
        if( m_Instance == null ){
            m_Instance = this as T;
        }
    }
 
    // �ṩ��ʼ����һ��ѡ�񣬿���ʹ��Init��Start������
    public virtual void Init(){}
 
    // �������˳����������� ����ģʽ = null
    private void OnApplicationQuit(){
        m_Instance = null;
    }
}