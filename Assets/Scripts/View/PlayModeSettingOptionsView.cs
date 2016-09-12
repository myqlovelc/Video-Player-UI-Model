using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayModeSettingOptionsView : ViewBase {

    public Toggle Toggle_Theater;

    public Toggle Toggle_180;

    public Toggle Toggle_360;

    public PlayListManager PlayListManager;

    public void SetDataContext(PlayListItem _data)
    {
        DataContext = _data;
    }

    public PlayListItem ViewModel
    {
        get { return DataContext as PlayListItem; }
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null)
        {
            HandlePropertyChanged(_newContext, "PlayListItem");
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
                case "PlayListItem":
                    UnregisterUIEvent();
                    SelectPlayMode(item.GetPlayMode());
                    RegisterUIEvent();
                    break;
                case "PlayMode":
                    PlayListManager.Save();
                    break;
            }
        }
    }

    public void SelectPlayMode(PLAY_MODE mode)
    {
        switch (mode)
        {
            case PLAY_MODE.MODE_THEATER:
                Toggle_Theater.isOn = true;
                break;
            case PLAY_MODE.MODE_180:
                Toggle_180.isOn = true; ;
                break;
            case PLAY_MODE.MODE_360:
                Toggle_360.isOn = true;
                break;
        }
    }

    public void RegisterUIEvent()
    {
        Toggle_Theater.onValueChanged.AddListener(OnToggleTheaterValueChanged);
        Toggle_180.onValueChanged.AddListener(OnToggle180ValueChanged);
        Toggle_360.onValueChanged.AddListener(OnToggle360ValueChanged);
    }

    public void UnregisterUIEvent()
    {
        Toggle_Theater.onValueChanged.RemoveListener(OnToggleTheaterValueChanged);
        Toggle_180.onValueChanged.RemoveListener(OnToggle180ValueChanged);
        Toggle_360.onValueChanged.RemoveListener(OnToggle360ValueChanged);
        ResetToggleState();
    }

    private void ResetToggleState()
    {
        Toggle_Theater.isOn = false;
        Toggle_180.isOn = false;
        Toggle_360.isOn = false;
    }

    public void OnToggleTheaterValueChanged(bool check)
    {
        if (check)
        {
            Debug.Log("Theater");
            ViewModel.PlayMode = (int)PLAY_MODE.MODE_THEATER;
        }
    }

    public void OnToggle180ValueChanged(bool check)
    {
        if (check)
        {
            Debug.Log("180");
            ViewModel.PlayMode = (int)PLAY_MODE.MODE_180;
        }
    }

    public void OnToggle360ValueChanged(bool check)
    {
        if (check)
        {
            Debug.Log("360");
            ViewModel.PlayMode = (int)PLAY_MODE.MODE_360;
        }
    }
}
