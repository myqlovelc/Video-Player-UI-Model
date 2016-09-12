using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class FolderListItemView : ListItemView {

    [SerializeField]
    private Text FolderDesc;

    public FolderListItem ViewModel
    {
        get { return DataContext as FolderListItem; }
    }

    protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null)
        {
            HandlePropertyChanged(_newContext, "FolderDesc");
        }
    }

    protected override void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        base.HandlePropertyChanged(sender_, prop_);

        string prop = prop_ as string;
        FolderListItem item = sender_ as FolderListItem;

        if (item != null)
        {
            switch (prop)
            {
                case "FolderDesc":
                    FolderDesc.text = Path.GetFileName(item.FileDesc);
                    break;
            }
        }
    }
}
