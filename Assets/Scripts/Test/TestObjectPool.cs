using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class TestObjectPool : MonoBehaviour
{

    GameObjectPool pool = null;

    string[] objectname = { "Cube", "Sphere" };

    public GameObject[] prefabs = null;

    private void Start()
    {
        // 创建对象池
        pool = GameObjectPool.instance;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("创建游戏对象"))
        {
            if (pool == null) return;
            Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            pool.CreateObject(objectname[Random.Range(0, objectname.Length)], 
                prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity);
        }

        if (GUILayout.Button("释放"))
        {
            if (pool == null) return;
            pool.ClearAll();
        }

        if (GUILayout.Button("回收"))
        {
            if (pool == null) return;
            string name = objectname[Random.Range(0, objectname.Length)];
            print(name);
            GameObject go = GameObject.FindGameObjectWithTag(name);
            pool.CollectObject(go);
        }
    }

}
