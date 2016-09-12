using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VideoTypeSettingOptionsView : ViewBase {

    public Toggle Toggle_2D;

    public Toggle Toggle_3D_LR;

    public Toggle Toggle_3D_TD;

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
                    SelectVideoType(item.GetVideoType());
                    RegisterUIEvent();
                    break;
                case "VideoType":
                    PlayListManager.Save();
                    break;
            }
        }
    }

    public void SelectVideoType(VIDEO_TYPE type)
    {
        switch (type)
        {
            case VIDEO_TYPE.VIDEO_2D:
                Toggle_2D.isOn = true;
                break;
            case VIDEO_TYPE.VIDEO_3D_LR:
                Toggle_3D_LR.isOn = true;
                break;
            case VIDEO_TYPE.VIDEO_3D_TD:
                Toggle_3D_TD.isOn = true;
                break;
        }
    }

    public void RegisterUIEvent()
    {
        Toggle_2D.onValueChanged.AddListener(OnToggle2DValueChanged);
        Toggle_3D_LR.onValueChanged.AddListener(OnToggle3DLRValueChanged);
        Toggle_3D_TD.onValueChanged.AddListener(OnToggle3DTDValueChanged);
    }

    public void UnregisterUIEvent()
    {
        Toggle_2D.onValueChanged.RemoveListener(OnToggle2DValueChanged);
        Toggle_3D_LR.onValueChanged.RemoveListener(OnToggle3DLRValueChanged);
        Toggle_3D_TD.onValueChanged.RemoveListener(OnToggle3DTDValueChanged);
        ResetToggleState();
    }

    private void ResetToggleState() {

        Debug.Log("ResetToggleState");
        Toggle_2D.isOn = false;
        Toggle_3D_LR.isOn = false;
        Toggle_3D_TD.isOn = false;
    }

    public void OnToggle2DValueChanged(bool check)
    {
        if (check) {
            Debug.Log("2D");
            ViewModel.VideoType = (int)VIDEO_TYPE.VIDEO_2D;
        }
    }

    public void OnToggle3DLRValueChanged(bool check)
    {
        if (check)
        {
            Debug.Log("3D_LR");
            ViewModel.VideoType = (int)VIDEO_TYPE.VIDEO_3D_LR;
        }
    }

    public void OnToggle3DTDValueChanged(bool check)
    {
        if (check)
        {
            Debug.Log("3D_TD");
            ViewModel.VideoType = (int)VIDEO_TYPE.VIDEO_3D_TD;
        }
    }

}
