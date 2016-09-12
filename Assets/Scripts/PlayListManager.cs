using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class PlayListManager : MonoBehaviour{

    private static string playListFile = "Config/PlayListSetting.xml";
    private PlayListSetting _playListSetting = null;
    public PlayListSetting PlayListSetting
    {
        get { return _playListSetting; }
        set { _playListSetting = value; OnPlayListSettingChange(); }
    }

    public System.Action<PlayListSetting> PlayListSettingChanged;
    private void OnPlayListSettingChange()
    {
        if (PlayListSettingChanged != null)
        {
            PlayListSettingChanged(PlayListSetting); //TODO: handle multiple selection
        }
    }

    public ListView PlayListView;

    public ListView PlayListSketchView;

    public VideoSettingManager VideoSettingManager;

    public PlaybackControlManager PlaybackControlManager;

    public Button PlayVideoButton;

    public Button RemoveVideoButton;

    public Button ClearVideosButton;

	// Use this for initialization
	void Start () {

        PlayListSettingChanged += AddPlayListItemsToPanel;

        PlayListSetting = XMLUtil.LoadSetting<PlayListSetting>(playListFile);

        //AddPlayListItemsToPanel();

        RegisterUIEvent();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RegisterUIEvent()
    {
        PlayVideoButton.onClick.AddListener(OnPlayVideoButtonClicked);
        RemoveVideoButton.onClick.AddListener(OnRemoveVideoButtonClicked);
        ClearVideosButton.onClick.AddListener(OnClearVideosButtonClicked);
    }

    private void AddPlayListItemsToPanel(PlayListSetting Setting)
    {
        PlayListView.Clear();
        PlayListSketchView.Clear();
        foreach (var playListItem in Setting.PlayList)
        {
            PlayListItemView item = PlayListView.Add(playListItem, true) as PlayListItemView;
            //item.SettingButton.onClick.AddListener(delegate()
            //{
            //    this.OnSettingButtonClicked(item);
            //});
            //item.DeleteButton.onClick.AddListener(delegate()
            //{
            //    this.OnDeleteButtonClicked(item);
            //});
            item.PlayButton.onClick.AddListener(delegate()
            {
                this.OnPlayListItemViewClicked(item);
            });

            PlayListSketchItemView sketchItem = PlayListSketchView.Add(playListItem, true) as PlayListSketchItemView;
        }
    }

    public void AddNewVideo(string VideoPath) {
        //GenerateThumbnailImage(VideoPath);
        PlayListSetting.AddVideoByName(VideoPath);
        PlayListSetting = PlayListSetting;
        Save();
        //AddPlayListItemsToPanel();
    }

    public void GenerateThumbnailImage(string VideoPath)
    {
        string ffmpegfile = ProjectManager._Instance.ProjectSetting.FfmpegFile;
        string imgpath = ProjectManager._Instance.ProjectSetting.ImagePath + "/" + Path.GetFileNameWithoutExtension(VideoPath) + ".jpg";
        string imgsize = "300*160";
        FfmepgUtil.GetImageFromVideo(ffmpegfile, VideoPath, imgpath, imgsize);
    }

    public void RemoveVideo(string VideoPath)
    {
        PlayListSetting.RemoveVideoByName(VideoPath);
        PlayListSetting = PlayListSetting;
        Save();
        //AddPlayListItemsToPanel();
    }

    public void RemoveAllVideos()
    {
        PlayListSetting.RemoveAllVideos();
        PlayListSetting = PlayListSetting;
        Save();
        //AddPlayListItemsToPanel();
    }

    public bool IsVideoAdded(string VideoPath)
    {
        return PlayListSetting.ContainsVideoByName(VideoPath);
    }

    public void OnDeleteButtonClicked(PlayListItemView item)
    {
        PlayListSetting.RemoveVideoByItem(item.ViewModel);
        Save();
        PlayListView.Delete(item);
        PlayListSketchView.Delete(item);
    }

    public void OnSettingButtonClicked(PlayListItemView item)
    {
        VideoSettingManager.SetDataContext(item.ViewModel);
        ScreenManager._Instance.ShowVideoSettingPanel();
    }

    public void OnAddVideoButtonClicked()
    {
        ScreenManager._Instance.ShowFileBrowserPanel();
    }

    public void OnPlayListItemViewClicked(PlayListItemView item)
    {
        PlaybackControlManager.DataContext = item.ViewModel;
        ScreenManager._Instance.ShowPlaybackControlPanel();
    }

    public void OnPlayVideoButtonClicked()
    {
        var _sketchItem = PlayListSketchView.SelectedItem as PlayListSketchItemView;
        PlaybackControlManager.DataContext = _sketchItem.ViewModel;
        ScreenManager._Instance.ShowPlaybackControlPanel();
    }

    public void OnRemoveVideoButtonClicked()
    {
        var _sketchItem = PlayListSketchView.SelectedItem as PlayListSketchItemView;
        if (_sketchItem != null)
        {
            RemoveVideo(_sketchItem.ViewModel.VideoPath);
        }
    }

    public void OnClearVideosButtonClicked()
    {
        RemoveAllVideos();
    }

    public void Save()
    {
        XMLUtil.SaveSetting<PlayListSetting>(playListFile, PlayListSetting);
    }
}

public class PlayListSetting
{ 
    public List<PlayListItem> PlayList = new List<PlayListItem>();

    public void AddVideoByName(string VideoPath) {
        PlayList.Add(new PlayListItem(VideoPath));
    }

    public void AddVideoByItem(PlayListItem item)
    {
        PlayList.Add(item);
    }

    public void RemoveVideoByName(string VideoPath)
    {
        PlayListItem item = null;
        foreach (var p in PlayList)
        {
            if (p.VideoPath == VideoPath)
            {
                item = p;
                break;
            }
        }
        RemoveVideoByItem(item);
    }

    public void RemoveVideoByItem(PlayListItem item)
    {
        PlayList.Remove(item);
    }

    public void RemoveAllVideos()
    {
        PlayList.Clear();
    }

    public bool ContainsVideoByName(string VideoPath)
    {
        bool contain = false;
        foreach (var item in PlayList)
        {
            if (item.VideoPath == VideoPath)
            {
                contain = true;
                break;
            }
        }
        return contain;
    }

    public bool ContainsVideoByItem(PlayListItem item)
    {
        return PlayList.Contains(item);
    }
}
