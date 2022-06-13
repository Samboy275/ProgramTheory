using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{

    [System.Serializable]
    class SaveData
    {
        public int score;
        public string Name;
        public int wavesSurvived;
    }

    // ENCAPSULATION
    public static MainManager _Instance{
        get;
        private set;
    }

    // control variables
    private SaveData data;
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
        }
        _Instance = this;
        data = new SaveData();
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerName(string name)
    {
        data.Name = name;
    }


    public bool Save(int score, int waves)
    {   
        SaveData sdata = new SaveData();
        sdata.Name = data.Name;
        sdata.score = score;
        sdata.wavesSurvived = waves;

        int highScore = LoadData();

        if (score > highScore)
        {
            string path = Application.persistentDataPath + "/savefile.json";

            string json = JsonUtility.ToJson(sdata);
            File.WriteAllText(path, json);
            return true;
        }
        return false;
    }

    public int LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData loadedDate = JsonUtility.FromJson<SaveData>(json);
            data.Name = loadedDate.Name;
            data.wavesSurvived = loadedDate.wavesSurvived;
            return loadedDate.score;
        }
        return 0;
    }
    public string GetName()
    {
        return data.Name;
    }
}
