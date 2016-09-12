using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

public enum VIDEO_TYPE
{
    VIDEO_2D,
    VIDEO_3D_LR,
    VIDEO_3D_TD
}
public enum PLAY_MODE
{
    MODE_THEATER,
    MODE_180,
    MODE_360
}

public class PlayListItem : DataModelBase
{
    [XmlElement("VideoPath")]
    public string VideoPath = "";

    [XmlIgnore]
    private string _thumbnailFile;

    [XmlElement("ThumbnailFile")]
    public string ThumbnailFile
    {
        get { return _thumbnailFile; }
        set 
        { 
            _thumbnailFile = value;
            string imagePath = ProjectManager._Instance.ProjectSetting.ImagePath + "/" + _thumbnailFile;
            Debug.Log(imagePath);
            var tex = ProjectManager._Instance.DefaultVideoThumbnail;
            if (File.Exists(imagePath))
            {
                tex = new Texture2D(4, 4);
                tex.LoadImage(System.IO.File.ReadAllBytes(imagePath));

                if (Thumbnail != ProjectManager._Instance.DefaultVideoThumbnail)
                {
                    GameObject.Destroy(Thumbnail);
                }
                Thumbnail = tex;
            }
        }
    }

    [XmlIgnore]
    public int _videoType = 0;
    [XmlElement("VideoType")]
    public int VideoType
    {
        get { return _videoType; }
        set { _videoType = value; OnPropertyChanged("VideoType"); }
    }

    [XmlIgnore]
    private int _playMode = 2;
    [XmlElement("PlayMode")]
    public int PlayMode
    {
        get { return _playMode; }
        set { _playMode = value; OnPropertyChanged("PlayMode"); }
    }

    [XmlIgnore]
    private Texture2D _thumbnail;
    [XmlIgnore]
    public Texture2D Thumbnail
    {
        get { return _thumbnail ?? ProjectManager._Instance.DefaultVideoThumbnail; }
        set
        {
            _thumbnail = value;
            OnPropertyChanged("Thumbnail");
        }
    }

    [XmlIgnore]
    public Texture2D ThumbnailSelected;

    public PlayListItem()
    {

    }

    public PlayListItem(string _videoPath)
    {
        VideoPath = _videoPath;
        ThumbnailFile = "1.jpg";
    }

    public VIDEO_TYPE GetVideoType()
    {
        return (VIDEO_TYPE)VideoType;
    }

    public PLAY_MODE GetPlayMode()
    {
        return (PLAY_MODE)PlayMode;
    }
}
