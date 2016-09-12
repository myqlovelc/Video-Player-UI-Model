using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class VideoFileListItemView : ListItemView {

    [SerializeField]
    private Text VideoFileDesc;

    public Button AddButton;

    public Button RemoveButton;

    public VideoFileListItem ViewModel
    {
        get { return DataContext as VideoFileListItem; }
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null)
        {
            HandlePropertyChanged(_newContext, "VideoFile");
        }
    }

    protected override void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        base.HandlePropertyChanged(sender_, prop_);

        string prop = prop_ as string;
        VideoFileListItem item = sender_ as VideoFileListItem;

        if (item != null)
        {
            switch (prop)
            {
                case "VideoFile":
                    VideoFileDesc.text = Path.GetFileName(item.FileDesc);
                    SetButtonState(item.Added);
                    break;
                case "VideoAddedOrRemoved":
                    SetButtonState(item.Added);
                    break;
            }
        }
    }

    private void SetButtonState(bool Added)
    {
        AddButton.interactable = !Added;
        RemoveButton.interactable = Added;
    }
}
