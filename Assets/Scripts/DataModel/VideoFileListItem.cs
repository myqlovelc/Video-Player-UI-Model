using UnityEngine;
using System.Collections;

public class VideoFileListItem : FileListItem {

    private bool _added = false;
    public bool Added
    {
        get { return _added; }
        set { _added = value; OnPropertyChanged("VideoAddedOrRemoved"); }
    }

    public VideoFileListItem(string VideoFileName_, bool Added_)
    {
        _fileDesc = VideoFileName_;
        _added = Added_;
    }

    public VideoFileListItem(string VideoFileName_)
    {
        _fileDesc = VideoFileName_;
    }
}
