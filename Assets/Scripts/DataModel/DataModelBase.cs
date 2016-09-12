using UnityEngine;
using System;
using System.Collections.Generic;

public interface INotifyPropertyChanged
{
    event Action<INotifyPropertyChanged, object> PropertyChanged;
}

public class DataModelBase : INotifyPropertyChanged
{
    public event System.Action<INotifyPropertyChanged, object> PropertyChanged;
    protected void OnPropertyChanged(object prop_)
    {

        if (PropertyChanged != null) {
            PropertyChanged(this, prop_);
        }
    }
}