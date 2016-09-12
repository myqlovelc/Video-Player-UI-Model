using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class PlayListSketchItemView : ListItemView {

    [SerializeField]
    private Text Description;

    public Button UpButton;

    public Button DownButton;

    public PlayListItem ViewModel
    {
        get { return DataContext as PlayListItem; }
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSketchItemClicked);

        Selected += (item_, value_) =>
        {
            Description.color = value_ ? Color.red : Color.black;
        };
    }

    public void OnSketchItemClicked()
    {
        IsSelected = true;
    }

	protected override void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        base.OnDataContextChanged(_oldContext, _newContext);

        if (_newContext != null) {
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
                case "Description":
                    Description.text = Path.GetFileName(item.VideoPath);
                    break;
            }
        }
    }
}
