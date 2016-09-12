using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class FileListItemView : ListItemView {

    [SerializeField]
    private Text FileDesc;

    [SerializeField]
    private RawImage FileIcon;

    public Texture FolderTex;

    public Texture VideoTex;

    public Button AddOrRemoveListButton;

    public FileListItem ViewModel
    {
        get { return DataContext as FileListItem; }
    }

    public void ChangeVideoFileListItemState(PlayListSetting Setting)
    {
        var _videoItem = ViewModel as VideoFileListItem;
        if (_videoItem != null)
        {
            _videoItem.Added = Setting.ContainsVideoByName(_videoItem.FileDesc);
        }
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null)
        {
            HandlePropertyChanged(_newContext, "File");
        }
    }

    protected override void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        base.HandlePropertyChanged(sender_, prop_);

        string prop = prop_ as string;
        var item = sender_ as FileListItem;

        if (item != null)
        {
            switch (prop)
            {
                case "File":
                    FileDesc.text = Path.GetFileName(item.FileDesc);
                    var folder = item as FolderListItem;
                    FileIcon.texture = folder != null ? FolderTex : VideoTex;
                    break;
                case "VideoAddedOrRemoved":
                    var videoFile = item as VideoFileListItem;
                    if (videoFile != null) {
                        FileDesc.color = videoFile.Added ? Color.red : Color.black;
                    }
                    break;
            }
        }
    }
}
