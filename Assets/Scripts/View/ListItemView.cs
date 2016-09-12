using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ListItemView : ViewBase {

    #region Properties

    private bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; OnSelected(); }
    }

    #endregion

    #region OnSelected

    public event Action<ListItemView, bool> Selected;
    protected void OnSelected()
    {
        if (Selected != null)
        {
            Selected(this, IsSelected);
        }
    }

    #endregion
}
