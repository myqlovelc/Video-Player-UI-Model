using UnityEngine;
using System.Collections;

public class FolderListItem : FileListItem {

    public FolderListItem(string FolderDesc_)
    {
        _fileDesc = FolderDesc_;
        //_icon = Resources.Load("unity_builtin_extra/Background", typeof(Sprite)) as Sprite;
    }
}
