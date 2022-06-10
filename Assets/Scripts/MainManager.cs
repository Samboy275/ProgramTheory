using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // ENCAPSULATION
    public static MainManager _Instance{
        get;
        private set;
    }

    // control variables

    private string playerName;
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
        }
        _Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
