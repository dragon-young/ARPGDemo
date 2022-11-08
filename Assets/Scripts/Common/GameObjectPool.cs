using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameObjectPool:MonoSingleton<GameObjectPool>
{

    private GameObjectPool() { }

    // 1. 创建对象池
    private Dictionary<string, List<GameObject>> cache = new Dictionary<string, List<GameObject>>();

    // 2 创建使用池的元素【游戏对象】一个对象并使用对象
    // 池中有，则从池中返回; 池中没有，则加载，放入池中再返回 
    public GameObject CreateObject(string key, GameObject go, Vector3 position, Quaternion rotation)
    {
        // 2.1 查找池中有无可用游戏对象
        GameObject tempGo = FindUsable(key);
        if(tempGo != null) // 2.2 如果池中有对象，则加载对象
        {
            tempGo.transform.position = position;
            tempGo.transform.rotation = rotation;
            tempGo.SetActive(true);
        }
        else // 如果池中没有对象，则添加对象
        {
            tempGo = Instantiate(go, position, rotation) as GameObject;
            // 放入池中
            Add(key, tempGo);
        }
        // 作为池对象的子物体
        tempGo.transform.parent = transform;
        return tempGo;
    }

    private GameObject FindUsable(string key)
    {
        if (cache.ContainsKey(key))
        {
            return cache[key].Find(p=>!p.activeSelf);   // p.activeSelf该物体是否激活
        }
        return null;
    }

    private void Add(string key, GameObject go)
    {
        // 先检查是否有key，如果没有，就将其放到字典列表中
        if (!cache.ContainsKey(key))
        {
            cache.Add(key, new List<GameObject>());
        }
        // 再将游戏对象放到池中
        cache[key].Add(go);
    }

    // 3 释放资源：从池中删除对象！
    // 3.1释放部分：按Key释放 
    public void Clear(string key)
    {
        if (cache.ContainsKey(key))
        {
            for (int i = 0; i < cache[key].Count; i++)
            {
                Destroy(cache[key][i]);
            }
            cache.Remove(key);
        }
    }

    //3.2释放全部 循环调用Clear(string key)
    public void ClearAll()
    {
        // 方式一
        List<string> keys = new List<string>(cache.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            Clear(keys[i]);
        }
        // 方式二
        //foreach (var item in cache.Keys)
        //{
        //    Clear(item);
        //}
    }

    // 4 回收对象：使用完对象返回池中【从画面中消失】
    // 4.1即时回收对象
    public void CollectObject(GameObject go)
    {
        go.SetActive(false);//本质：画面小时 设置属性
    }

    //4.2延时回收对象 等待一定的时间 协程
    public void CollectObject(GameObject go, float delay)
    {
        StartCoroutine(CollectDelay(go, delay));
    }
    private IEnumerator CollectDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        CollectObject(go);
    }

}

