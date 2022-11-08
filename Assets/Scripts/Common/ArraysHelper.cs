using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 选择委托
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <param name="t"></param>
/// <returns></returns>
public delegate TKey SelectHandler<T, TKey>(T t);

/// <summary>
/// 条件委托
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="t"></param>
/// <returns></returns>
public delegate bool FindHandler<T>(T t);

public static class ArraysHelper
{

    /// <summary>
    /// 升序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sums"></param>
    /// <returns></returns>
    public static T[] OrderBy<T>(T[] sums) where T : IComparable<T>
    {
        for (int i = 0; i < sums.Length; i++)
        {
            for (int j = 0; j < sums.Length - i - 1; j++)
            {
                if (sums[j].CompareTo(sums[j + 1]) > 0)
                {
                    // 交换 第i个和第i+1的顺序
                    T temp = sums[j];
                    sums[j] = sums[j + 1];
                    sums[j + 1] = temp;
                }
            }
        }
        return sums;
    }

    /// <summary>
    /// 任意数组类型的排序（升序）
    /// </summary>
    /// <typeparam name="T">什么类型的？</typeparam>
    /// <typeparam name="Tkey">想比较这个类型的什么属性？</typeparam>
    /// <param name="sums"></param>
    /// <param name="handler">委托对象</param>
    /// <returns>排序后的数组</returns>
    public static T[] OrderBy<T, Tkey>(T[] sums, SelectHandler<T, Tkey> handler) where Tkey : IComparable<Tkey>
    {
        for (int i = 0; i < sums.Length; i++)
        {
            for (int j = 0; j < sums.Length - i - 1; j++)
            {
                if (handler(sums[j]).CompareTo(handler(sums[j + 1])) > 0)
                {
                    // 交换 第i个和第i+1的顺序
                    T temp = sums[j];
                    sums[j] = sums[j + 1];
                    sums[j + 1] = temp;
                }
            }
        }
        return sums;
    }

    /// <summary>
    /// 降序
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="Tkey">数据类型字段的类型</typeparam>
    /// <param name="sums">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    /// <returns></returns>
    public static T[] OrderByDescending<T, Tkey>(T[] sums, SelectHandler<T, Tkey> handler) where Tkey : IComparable<Tkey>
    {
        for (int i = 0; i < sums.Length; i++)
        {
            for (int j = 0; j < sums.Length - i - 1; j++)
            {
                if (handler(sums[j]).CompareTo(handler(sums[j + 1])) < 0)
                {
                    // 交换 第i个和第i+1的顺序
                    T temp = sums[j];
                    sums[j] = sums[j + 1];
                    sums[j + 1] = temp;
                }
            }
        }
        return sums;
    }


    /// <summary>
    /// 返回最大的
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="Tkey">数据类型字段的类型</typeparam>
    /// <param name="sums">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    /// <returns></returns>
    public static T Max<T, Tkey>(T[] sums, SelectHandler<T, Tkey> handler) where Tkey : IComparable<Tkey>
    {
        T maxT = default(T);
        maxT = sums[0];
        for (int i = 0; i < sums.Length; i++)
        {
            if (handler(sums[i]).CompareTo(handler(maxT)) > 0)
            {
                maxT = sums[i];
            }
        }
        return maxT;
    }

    /// <summary>
    /// 返回最小的
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="Tkey">数据类型字段的类型</typeparam>
    /// <param name="sums">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    /// <returns></returns>
    public static T Min<T, Tkey>(T[] sums, SelectHandler<T, Tkey> handler) where Tkey : IComparable<Tkey>
    {
        T minT = default(T);
        minT = sums[0];
        for (int i = 0; i < sums.Length; i++)
        {
            if (handler(sums[i]).CompareTo(handler(minT)) < 0)
            {
                minT = sums[i];
            }
        }
        return minT;
    }

    /// <summary>
    /// 根据条件查找某一个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arrays"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public static T Find<T>(T[] arrays, FindHandler<T> handler)
    {
        T temp = default(T); // T是什么类型，它默认就是什么类型的数据
        for (int i = 0; i < arrays.Length; i++)
        {
            if (handler(arrays[i]))
            {
                return arrays[i];
            }
        }

        return temp;
    }

    /// <summary>
    /// 根据条件查找多个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arrays"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public static T[] FindAll<T>(T[] arrays, FindHandler<T> handler)
    {
        List<T> list = new List<T>();

        for (int i = 0; i < arrays.Length; i++)
        {
            if (handler(arrays[i]))
            {
                list.Add(arrays[i]);
            }
        }

        return list.ToArray();
    }

    /// <summary>
    /// 选择：选取数组中对象的某些成员形成一个独立的数组 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public static TKey[] Select<T, TKey>(T[] arrays, SelectHandler<T, TKey> handler)
    {
        TKey[] sums = new TKey[arrays.Length];
        for (int i = 0; i < arrays.Length; i++)
        {
            sums[i] = handler(arrays[i]);
        }
        return sums;
    }

}
