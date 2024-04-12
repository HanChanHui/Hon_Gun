using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T instance;
    private static bool isApplicationQuit = false;

    public static T Instance
    {
        get
        {
            if(isApplicationQuit)
            {
                return null;
            }

            if(instance == null)
            {
                T[] _finds = FindObjectsOfType<T>();
                if(_finds.Length > 0)
                {
                    instance = _finds[0];
                    for(int i = 0; i < _finds.Length; ++i)
                    {
                        DontDestroyOnLoad(_finds[i].gameObject);
                    }
                }

                if (_finds.Length > 1)
                {
                    for(int i = 1; i < _finds.Length; ++i)
                    {
                        Destroy(_finds[i].gameObject);
                    }
                    Debug.LogError("There is more than one " + typeof(T).Name + "in the Scene.");
                }

                if(instance == null)
                {
                    GameObject _createGameObject = new GameObject(typeof(T).Name);
                    DontDestroyOnLoad(_createGameObject);
                    instance = _createGameObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        isApplicationQuit = true;
        instance = null;
    }
}
