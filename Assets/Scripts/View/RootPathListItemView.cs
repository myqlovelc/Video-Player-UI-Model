using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RootPathListItemView : ListItemView
{
    [SerializeField]
    private Text RootPathDesc;

    public RootPathListItem ViewModel
    {
        get { return DataContext as RootPathListItem; }
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null)
        {
            HandlePropertyChanged(_newContext, "RootPathDesc");
        }
    }

    protected override void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        base.HandlePropertyChanged(sender_, prop_);

        string prop = prop_ as string;
        RootPathListItem item = sender_ as RootPathListItem;

        if (item != null)
        {
            switch (prop)
            {
                case "RootPathDesc":
                    RootPathDesc.text = item.PathDesc;
                    break;
            }
        }
    }
}
