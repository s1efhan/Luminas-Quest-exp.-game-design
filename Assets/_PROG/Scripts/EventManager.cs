using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EventManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("EventManager");
                    _instance = obj.AddComponent<EventManager>();
                }
            }
            return _instance;
        }
    }

    public event Action OnLeverPulled;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LeverPulled()
    {
        OnLeverPulled?.Invoke();
    }

    void OnDestroy()
    {
        _instance = null;
    }
}
