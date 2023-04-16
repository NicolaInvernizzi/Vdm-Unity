using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public List<Data> datas = new List<Data>();
    private GameOver gameOver_Script;
    private LoadManager loadManager;
    public bool playerRotation;
    
    private void Awake()
    {
        loadManager = FindObjectOfType<LoadManager>();
        gameOver_Script = GetComponent<GameOver>();

        if (loadManager.load)
        {
            LoadFromJson();
            loadManager.load = false;
        }
    }
    public void SaveToJson()
    {
        DataFile dataFile = new DataFile(datas, gameOver_Script.counter);
        string stringData = JsonUtility.ToJson(dataFile);
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        Debug.Log(filePath);
        File.WriteAllText(filePath, stringData);
        Debug.Log("Saved");
    }
    public void LoadFromJson()
    {
        playerRotation = true;
        if (!File.Exists(Application.persistentDataPath + "/InventoryData.json"))
        {
            Debug.Log("No saving data");
            return;
        }

        bool ignoreDestory;
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        string stringData = File.ReadAllText(filePath);
        DataFile dataFile = JsonUtility.FromJson<DataFile>(stringData);

        gameOver_Script.counter = dataFile.counter;
        gameOver_Script.updateTextBox();

        foreach (Data data in datas)
        {
            ignoreDestory = false;
            data.GetData();
            foreach (ObjectData objData in dataFile.objectDataList)
            {
                if (objData.id == data.id)
                {
                    data.RefreshData(objData);
                    ignoreDestory = true;
                    break;
                }
            }
            if(!ignoreDestory)
            {
                Destroy(data.gameObject);
                Debug.Log(data.id + "has been destoried");
            }
        }
        Debug.Log("Loaded");
        playerRotation = true;
    }
    public void DeleteSaveFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/InventoryData.json"))
        {
            Debug.Log("No saving data");
            return;
        }
        File.Delete(Application.persistentDataPath + "/InventoryData.json");
        Debug.Log("File deleted");
    }
}

[System.Serializable]
public class ObjectData
{
    public string id;
    public bool isActive;
    public float[] position;
    public float[] rotation;

    public ObjectData(Data data)
    {
        id = data.id;
        isActive = data.isActive;
        position = Data.FromVector3ToArray(data.position);
        rotation = Data.FromQuaternionToArray(data.rotation);
    }
}

[System.Serializable]
public class DataFile
{
    public int counter;
    public List<ObjectData> objectDataList;
    public DataFile(List<Data> datas, int counter) 
    {
        this.counter = counter;

        objectDataList = new List<ObjectData>();
        foreach (Data data in datas)
        {
            data.GetData();
            objectDataList.Add(new ObjectData(data));
        }
    }
}

