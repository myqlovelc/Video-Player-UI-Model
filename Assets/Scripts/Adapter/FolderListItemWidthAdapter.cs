using UnityEngine;
using System.Collections;

public class FolderListItemWidthAdapter : MonoBehaviour
{

    private GameObject ScrollBar;

    public Transform InfoPanel;

    private bool Adapted = false;

    private float AdpatedWidth = 20.0f;

	// Use this for initialization
    void Start()
    {
        ScrollBar = transform.parent.parent.Find("Scrollbar").gameObject;

        //StartCoroutine("AdaptItemWidth");
	}

    IEnumerator AdaptItemWidth()
    {
        yield return null;
        yield return null;
        if (!ScrollBar.activeInHierarchy)
        {
            Debug.Log("myqTmac");
            RectTransform rt = InfoPanel.GetComponent<RectTransform>();
            float x = rt.sizeDelta.x + AdpatedWidth;
            rt.sizeDelta = new Vector2(x, 0);
            //rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            //rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 100);
            Adapted = true;
        }

    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(this + " " + ScrollBar.activeInHierarchy);
        if (!Adapted)
        {
            if (ScrollBar.activeInHierarchy)
            {

                RectTransform rt = InfoPanel.GetComponent<RectTransform>();
                float x = rt.sizeDelta.x - AdpatedWidth;
                rt.sizeDelta = new Vector2(x, 0);
                //rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                //rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 100);
                Adapted = true;
            }
        }
	}
}
