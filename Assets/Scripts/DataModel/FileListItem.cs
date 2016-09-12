using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FileListItem : DataModelBase
{
    protected string _fileDesc = "";
    public string FileDesc
    {
        get { return _fileDesc; }
        set { _fileDesc = value; OnPropertyChanged("FileDesc"); }
    }

    protected Texture2D _icon;
    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; OnPropertyChanged("FileIcon"); }
    }

}
