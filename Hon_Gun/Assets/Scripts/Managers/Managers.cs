using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance { get { Init(); return instance; } }

    #region Managers
    private MemoryPoolManager memoryPool = new MemoryPoolManager();
    private ResourceManager resource = new ResourceManager();
    private UIManager ui = new UIManager();
    private DataManager data = new DataManager();


    public static MemoryPoolManager MemoryPool => Instance.memoryPool;
    public static ResourceManager Resource => Instance.resource;
    public static UIManager UI => Instance.ui;
    public static DataManager Data => Instance.data;

    #endregion

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
            instance.memoryPool.Init();
            instance.data.Init();
        }
    }

    public void Clear()
    {
        MemoryPool.Clear();
    }

}
