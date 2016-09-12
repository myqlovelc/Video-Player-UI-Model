using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RootPathListItem : DataModelBase
{
    private string _pathDesc = "";
    public string PathDesc
    {
        get { return _pathDesc; }
        set { _pathDesc = value; OnPropertyChanged("RootPath"); }
    }

    public RootPathListItem(string PathDesc_)
    {
        _pathDesc = PathDesc_;
    }
}