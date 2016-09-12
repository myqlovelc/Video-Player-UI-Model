using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FileBrowserManager : MonoBehaviour
{
    public ListView RootPathListView;

    public Text CurrentPathDisplay;

    //version 1.0.0
    //public ListView FolderListView;

    //public ListView VideoFileListView;

    //version 1.0.1
    public ListView FileListView;

    public Button BackButton;

    public Button ForwardButton;

    public Button LastLevelButton;

    public ListView PlayListSketchView;

    public PlayListManager PlayListManager;

    private List<string> folderList = new List<string>();
    private int _currentPos = 0;
    private int CurrentPos
    {
        get { return _currentPos; }
        set 
        { 
            _currentPos = value;
            CurrentFolder = folderList.ElementAt(_currentPos);
            ChangeButtonState(); 
        }
    }

    private string _currentFolder;
    public string CurrentFolder
    {
        get { return _currentFolder; }
        set
        {
            _currentFolder = value;
            CurrentPathDisplay.text = _currentFolder;
            RefreshFileDisplayInCurrentFolder(_currentFolder);
        }
    }

    void Start()
    {

        string[] drives = Directory.GetLogicalDrives();
        foreach (string drive in drives)
        {
            RootPathListItemView item = RootPathListView.Add(new RootPathListItem(drive)) as RootPathListItemView;
            item.GetComponent<Button>().onClick.AddListener(delegate()
            {
                this.OnFolderSelectButtonClicked(item);
            });
        }
        AddFolderToList(drives[0]);

        RegisterUIEvent();

        PlayListSketchView.SelectionChanged += item_ =>
        {
            var _sketchItem = item_ as PlayListSketchItemView;
            if (_sketchItem != null)
            {
                AddFolderToList(Path.GetDirectoryName(_sketchItem.ViewModel.VideoPath));
            }
        };
    }

    private void RegisterUIEvent()
    {
        BackButton.onClick.AddListener(OnBackButtonClicked);
        ForwardButton.onClick.AddListener(OnForwardButtonClicked);
        LastLevelButton.onClick.AddListener(OnLastLevelButtonClicked);
    }

    public void AddFolderToList(string folder)
    {
        if (folderList.Count != 0)
        {
            int num = folderList.Count - (CurrentPos + 1);
            folderList.RemoveRange(CurrentPos + 1, num);

        }
        folderList.Add(folder);
        CurrentPos = folderList.Count - 1;
    }

    public void RefreshFileDisplayInCurrentFolder(string folder)
    {
        //version 1.0.0
        /*FolderListView.Clear();
        VideoFileListView.Clear();

        if (Directory.Exists(folder))
        {
            var subFolders = Directory.GetDirectories(folder);
            foreach (string subFolder in subFolders)
            {
                FolderListItemView item = FolderListView.Add(new FolderListItem(subFolder)) as FolderListItemView;
                item.GetComponent<Button>().onClick.AddListener(delegate()
                {
                    this.OnFolderSelectButtonClicked(item);
                });
            }

            var videoFiles = Directory.GetFiles(folder, "*.*")
                .Where(s => VideoValidator.ValidateVideoFormat(s));
            foreach (string videoPath in videoFiles)
            {
                bool VideoAdded = GetComponent<PlayListManager>().IsVideoAdded(videoPath);
                VideoFileListItemView item = VideoFileListView.Add(new VideoFileListItem(videoPath, VideoAdded)) as VideoFileListItemView;
                item.AddButton.onClick.AddListener(delegate()
                {
                    this.OnAddButtonClicked(item);
                });
                item.RemoveButton.onClick.AddListener(delegate()
                {
                    this.OnRemoveButtonClicked(item);
                });
            }
        }*/

        //version 1.0.1
        FileListView.Clear();

        if (Directory.Exists(folder))
        {
            var subFolders = Directory.GetDirectories(folder);
            foreach (string subFolder in subFolders)
            {
                FileListItemView item = FileListView.Add(new FolderListItem(subFolder)) as FileListItemView;
                item.GetComponent<Button>().onClick.AddListener(delegate()
                {
                    this.OnFolderSelectButtonClicked(item);
                });
            }

            var videoFiles = Directory.GetFiles(folder, "*.*")
                .Where(s => VideoValidator.ValidateVideoFormat(s));
            foreach (string videoPath in videoFiles)
            {
                bool VideoAdded = GetComponent<PlayListManager>().IsVideoAdded(videoPath);
                FileListItemView item = FileListView.Add(new VideoFileListItem(videoPath)) as FileListItemView;
                (item.ViewModel as VideoFileListItem).Added = VideoAdded;
                item.AddOrRemoveListButton.onClick.AddListener(delegate()
                {
                    this.OnAddOrRemoveListButtonClicked(item);
                });

                PlayListManager.PlayListSettingChanged += item.ChangeVideoFileListItemState;
            }
        }
    }

    public void OnFolderSelectButtonClicked(ListItemView item)
    {
        var rootItemView = item as RootPathListItemView;
        if (rootItemView != null)
        {
            AddFolderToList(rootItemView.ViewModel.PathDesc);
        }

        //version 1.0.0
        /*var folderItemView = item as FolderListItemView;
        if (folderItemView != null)
        {
            AddFolderToList(folderItemView.ViewModel.FileDesc);
        }*/
        
        //version 1.0.1
        var folderItemView = item as FileListItemView;
        if (folderItemView != null)
        {
            AddFolderToList(folderItemView.ViewModel.FileDesc);
        }
    }

    public void OnBackButtonClicked()
    {
        CurrentPos--;
    }

    public void OnForwardButtonClicked()
    {
        CurrentPos++;
    }

    public void OnLastLevelButtonClicked()
    {
        string parentFolder = Path.GetDirectoryName(CurrentFolder);
        if (parentFolder != null) {
            AddFolderToList(parentFolder);
        }
    }

    public void OnAddButtonClicked(VideoFileListItemView item)
    {
        GetComponent<PlayListManager>().AddNewVideo(item.ViewModel.FileDesc);
        //item.ViewModel.Added = true;
    }

    public void OnAddOrRemoveListButtonClicked(FileListItemView itemView)
    {
        var item = itemView.ViewModel as VideoFileListItem;
        if (item != null)
        {
            if (!item.Added) {
                GetComponent<PlayListManager>().AddNewVideo(item.FileDesc);
                //item.Added = true;
            }
            else
            {
                GetComponent<PlayListManager>().RemoveVideo(item.FileDesc);
                //item.Added = false;
            }
        }
    }

    public void OnRemoveButtonClicked(VideoFileListItemView item)
    {
        GetComponent<PlayListManager>().RemoveVideo(item.ViewModel.FileDesc);
        //item.ViewModel.Added = false;
    }

    public void OnCloseFileBrowserButtonClicked()
    {
        ScreenManager._Instance.CloseFileBrowserPanel();
    }

    void Update ()
    {
    }


    private void ChangeButtonState()
    {
        ForwardButton.interactable = (CurrentPos < folderList.Count - 1);
        BackButton.interactable = (CurrentPos > 0);
        LastLevelButton.interactable = (Path.GetDirectoryName(CurrentFolder) != null);
    }
}
