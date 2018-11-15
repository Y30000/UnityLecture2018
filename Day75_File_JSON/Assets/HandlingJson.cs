using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class HandlingJson : MonoBehaviour {
    string pathJson;
    GameScore gs;

	// Use this for initialization
	void Start ()
    {
        pathJson = Application.persistentDataPath + "/gameScore.json";  //기본 저장 위치 + /파일 이름
        print(pathJson);

        gs = new GameScore();
        gs.level = 10;
        gs.timeElasped = 150.1f;
        gs.playerName = "shJeong";

        Item item = new Item();
        item.itemID = 1000;
        item.iconImage = "image1.png";
        item.price = 10000;

        gs.items = new List<Item>();

        gs.items.Add(item);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveJsonFile();
        if (Input.GetKeyDown(KeyCode.L))
            LoadJsonFile();
    }

    private void LoadJsonFile()
    {
        if (File.Exists(pathJson))
        {
            string dataAsJson = File.ReadAllText(pathJson);
            GameScore newGS = JsonUtility.FromJson<GameScore>(dataAsJson);
            print(newGS);
            print(newGS.level == 10);
        }
        else
        {
            print("no file!");
        }
    }

    private void SaveJsonFile()
    {
        string dataAsJson = JsonUtility.ToJson(gs, true);
        print(dataAsJson);
        File.WriteAllText(pathJson, dataAsJson);
    }
}

[Serializable]
public class GameScore
{
    public int level;
    public float timeElasped;
    public string playerName;

    public List<Item> items;

    [NonSerialized]         //저장 금지!!
    public string dontCare; //저장하고 싶지 않은 필드

    public override string ToString()   //프린트 용도
    {
        return level + ", " + timeElasped + ", " + playerName; //+ ", " + items;
    }
}

[Serializable]      //알아서 array처럼 바꿔주   //using system;
public class Item
{
    public int itemID;
    public string iconImage;
    public int price;

    public override string ToString()
    {
        return itemID + ", " + iconImage + ", " + price;
    }
}
