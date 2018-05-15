using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    private Text txtToolTip;
    private Text txtContent;
    private CanvasGroup canvasGroup;

    public float targetAlpha = 1f;
    public int smoothSpeed = 4;

	// Use this for initialization
	void Start ()
	{
	    txtToolTip = GetComponent<Text>();
	    txtContent = transform.Find("TextContent").GetComponent<Text>();
	    canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (canvasGroup.alpha != targetAlpha)
	    {
	        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothSpeed * Time.deltaTime);
	        if (Mathf.Abs(canvasGroup.alpha-targetAlpha) < 0.01f)
	        {
	            canvasGroup.alpha = targetAlpha;
	        }
	    }	
	}

    public void Show(string content)
    {
        txtToolTip.text = content;
        txtContent.text = content;
        targetAlpha = 1;
    }

    public void Hide()
    {
        targetAlpha = 0;
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
