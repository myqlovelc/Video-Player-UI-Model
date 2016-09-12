using UnityEngine;
using System.Collections;

public class VideoSettingManager : ViewBase {

    public VideoTypeSettingOptionsView VideoTypeSettingOptionsView;

    public PlayModeSettingOptionsView PlayModeSettingOptionsView;

    public void SetDataContext(PlayListItem _data) {
        VideoTypeSettingOptionsView.SetDataContext(_data);
        PlayModeSettingOptionsView.SetDataContext(_data);
        //DataContext = _data;
    }

    public PlayListItem ViewModel
    {
        get { return DataContext as PlayListItem; }
    }

    public void OnCloseVideoSettingButtonClicked()
    {
        VideoTypeSettingOptionsView.UnregisterUIEvent();
        PlayModeSettingOptionsView.UnregisterUIEvent();
        ScreenManager._Instance.CloseVideoSettingPanel();
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null)
        {
            HandlePropertyChanged(_newContext, "VideoType");
            HandlePropertyChanged(_newContext, "PlayMode");
        }
    }

    protected override void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        base.HandlePropertyChanged(sender_, prop_);

        string prop = prop_ as string;
        PlayListItem item = sender_ as PlayListItem;

        if (item != null)
        {
            switch (prop)
            {
                case "VideoType":
                    VideoTypeSettingOptionsView.SelectVideoType(item.GetVideoType());
                    break;
                case "PlayMode":
                    PlayModeSettingOptionsView.SelectPlayMode(item.GetPlayMode());
                    break;
            }
        }
    }
}
