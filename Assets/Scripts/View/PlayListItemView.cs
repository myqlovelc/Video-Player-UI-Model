using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
public class PlayListItemView : ListItemView
{
    [SerializeField]
    private RawImage Thumbnail;

    [SerializeField]
    private Text Description;

    public Button PlayButton;

    public Button SettingButton;

    public Button DeleteButton;

    public PlayListItem ViewModel
    {
        get { return DataContext as PlayListItem; }
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null) {
            HandlePropertyChanged(_newContext, "Thumbnail");
            HandlePropertyChanged(_newContext, "Description");
        }
    }

    protected override void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        base.HandlePropertyChanged(sender_, prop_);

        string prop = prop_ as string;
        PlayListItem item = sender_ as PlayListItem;

        if (item != null) {
            switch (prop)
            {
                case "Thumbnail":
                    Thumbnail.texture = item.Thumbnail;
                    break;
                case "Description":
                    Description.text = Path.GetFileName(item.VideoPath);
                    break;
            }
        }
    }
}