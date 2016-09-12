using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VideoFileListItemWidthAdapter : MonoBehaviour
{

    private GameObject ScrollBar;

    public Transform InfoPanel;

    public Transform ButtonPanel;

    private bool Adapted = false;

    private float AdpatedWidth = 20.0f;

	// Use this for initialization
    void Start()
    {
        ScrollBar = transform.parent.parent.Find("Scrollbar").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (!Adapted)
        {
            if (!ScrollBar.activeInHierarchy)
            {
                RectTransform rt = InfoPanel.GetComponent<RectTransform>();
                float x = rt.sizeDelta.x + AdpatedWidth;
                rt.sizeDelta = new Vector2(x, 0);
                //rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                //rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 100);
                ButtonPanel.GetComponent<RectTransform>().Translate(AdpatedWidth, 0, 0);
                Adapted = true;
                enabled = false;
            }
        }
	}
}
