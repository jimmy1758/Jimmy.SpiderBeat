using System.Collections.Generic;

//ID : 歌曲ID(int)
//MusicName : 歌曲名字(string)
//CoverPath : 歌曲封面(string)
//EasyPath : 简单难度关卡文件路径(string)
//NormalPath : 普通难度关卡文件路径(string)
//CrazyPath : 疯狂难度关卡文件路径(string)

public class MusicData : Singleton<MusicData>
{
    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {1001, new Dictionary<string, string>(){ {"MusicName", "ThisIsWhatYouCameFor"}, {"CoverPath", "MusicCover/ThisIsWhatYouCameFor"}, {"EasyPath", "SoneBeatsData/ThisIsWhatYouCameFor_Easy"}, {"NormalPath", "SoneBeatsData/ThisIsWhatYouCameFor_Normal"}, {"CrazyPath", "SoneBeatsData/ThisIsWhatYouCameFor_Crazy"}, } },
        {1002, new Dictionary<string, string>(){ {"MusicName", "Rise"}, {"CoverPath", "MusicCover/Rise"}, {"EasyPath", ""}, {"NormalPath", ""}, {"CrazyPath", ""}, } },
        {1003, new Dictionary<string, string>(){ {"MusicName", "Escape"}, {"CoverPath", "MusicCover/Escape"}, {"EasyPath", ""}, {"NormalPath", ""}, {"CrazyPath", ""}, } },
    };

}
