using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ViewBase : MonoBehaviour
{
    private INotifyPropertyChanged _dataContext;
    public INotifyPropertyChanged DataContext
    {
        get { return _dataContext; }
        set
        {
            OnDataContextChanged(_dataContext, value);
        }
    }

    protected virtual void OnDataContextChanged(INotifyPropertyChanged _oldContext, INotifyPropertyChanged _newContext)
    {
        if (_oldContext != null)
        {
            _oldContext.PropertyChanged -= ValidateAndHandlePropertyChanged;
        }

        if (_newContext != null)
        {
            _newContext.PropertyChanged += ValidateAndHandlePropertyChanged;
        }
        _dataContext = _newContext;
    }

    private void ValidateAndHandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
        if (gameObject.activeInHierarchy)
        {
            HandlePropertyChanged(sender_, prop_);
        }

    }

    protected virtual void HandlePropertyChanged(INotifyPropertyChanged sender_, object prop_)
    {
    }
}
