using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListView : MonoBehaviour {

    [SerializeField]
    private GameObject ItemTemplate;

    [SerializeField]
    private Transform ContentPresenter;

    private List<ListItemView> _itemViewList = new List<ListItemView>();
    public List<ListItemView> ItemViewList
    {
        get { return _itemViewList; }
        private set { _itemViewList = value; }
    }

    private ListItemView _selectedItem;
    public ListItemView SelectedItem
    {
        get { return _selectedItem; }
        set { _selectedItem = value; OnSelectionChange(); }
    }

    public System.Action<ListItemView> SelectionChanged;
    private void OnSelectionChange()
    {
        if (SelectionChanged != null)
        {
            SelectionChanged(SelectedItem); //TODO: handle multiple selection
        }
    }

    public ListItemView Add(INotifyPropertyChanged _data)
    {
        return Add(_data, false);
    }

    public ListItemView Add(INotifyPropertyChanged _data, bool addToFirst)
    {
        GameObject itemObj = (GameObject)GameObject.Instantiate(ItemTemplate);

        itemObj.transform.SetParent(ContentPresenter, false);
        if (addToFirst) {
            itemObj.transform.SetAsFirstSibling();
        }

        ListItemView item = itemObj.GetComponentInChildren<ListItemView>();
        if (item == null) throw new System.Exception(string.Format("itemObj:{0} there is no ListItem Component attached to template", itemObj));

        item.DataContext = _data;

        item.Selected += (item_, value_) =>
        {
            if (value_ && item_ != SelectedItem)//Select
            {
                if (SelectedItem != null && SelectedItem.IsSelected == value_)
                {
                    SelectedItem.IsSelected = false;
                }
            }

            SelectedItem = item_;
        };

        ItemViewList.Add(item);

        return item;
    }

    public void Delete(ListItemView item)
    {
        item.DataContext = null;
        Destroy(item.gameObject);
        ItemViewList.Remove(item);
    }

    public void Clear()
    {
        for (int i = 0; i < ItemViewList.Count; i++)
        {
            ////TODO:
            //var childViewList = ItemList[i].GetComponentsInChildren<ViewBase>();
            //foreach (var view in childViewList)
            //{
            //    view.DataContext = null;
            //}

            ItemViewList[i].DataContext = null;
            Destroy(ItemViewList[i].gameObject);
            //ItemList[i].gameObject.SetActive(false);
            //ItemList[i].transform.SetParent(null, false);
            //_itemPool.Add(ItemList[i].gameObject);
        }
        ItemViewList.Clear();
    }
}
