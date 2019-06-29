using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicModel : Singleton<MusicModel> {

    Dictionary<int, Dictionary<string, string>> data;

    public MusicModel()
    {
        data = MusicData.Instance.data;
    }

    public int GetNextMusicID(ref int currentIndex, bool right)
    {
        int res;
        List<int> musicIDs = data.Keys.ToList();
        if (right)
        {
            currentIndex++;
            if (currentIndex == musicIDs.Count)
                currentIndex = 0;
        }
        else
        {
            currentIndex--;
            if (currentIndex == -1)
                currentIndex = musicIDs.Count - 1;
        }
        res = musicIDs[currentIndex];
        return res;
    }


    public string GetMusicName(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["MusicName"];
        }
        return res;
    }

    public string GetCoverPath(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["CoverPath"];
        }
        return res;
    }

    public string GetEasyPath(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["EasyPath"];
        }
        return res;
    }

    public string GetNormalPath(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["NormalPath"];
        }
        return res;
    }

    public string GetCrazyPath(int id)
    {
        string res = "";
        if (data.ContainsKey(id))
        {
            res = data[id]["CrazyPath"];
        }
        return res;
    }
}
