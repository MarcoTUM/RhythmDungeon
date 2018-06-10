using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour {
	// Use this for initialization
    public Camera Cam;
    private Renderer _rend;
    private Image _curImage;
    public Color normColor, activeColor;
    public float holdTime;
    private float curTimer;
    private GestureController _gestureController;
    public float z;
  void Start () {
        _gestureController = GameObject.Find("GestureController").GetComponent<GestureController>();
       _rend = GetComponent<Renderer>();
        curTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (_gestureController.detectPlayer())
        {
            Vector2 handPos = _gestureController.getMappedRightHandPosition();
            transform.position = Cam.ViewportToWorldPoint(new Vector3(handPos.x, handPos.y, 1));
            transform.position = new Vector3(transform.position.x, transform.position.y, z);

            if (_gestureController.getRightHandState())
            {
                _rend.material.color = normColor;
                RaycastUIObjects();
            }
            else
            {
                _rend.material.color = activeColor;
                curTimer = 0;
                if (_curImage != null)
                {
                    _curImage.fillAmount = 0;
                    _curImage = null;
                }
            }

        }
        else
        {
           // transform.position = new Vector3(0, -100, 0);
        }
	}

    void RaycastUIObjects()
    {
        var pointer = new PointerEventData(EventSystem.current);
        // convert to screen space
        pointer.position = Camera.main.WorldToScreenPoint(transform.position);
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        if (raycastResults.Count > 1)
            Debug.LogWarning("More than one target hit by raycast");
   
        foreach (RaycastResult result in raycastResults)
        {
            //Debug.Log("Hit " + raycastResults.Count + result.gameObject.name);

            
            Button b = result.gameObject.GetComponent<Button>();
            Image i  = result.gameObject.GetComponent<Image>();
            if(i != _curImage)
            {
                if(_curImage != null)
                    _curImage.fillAmount = 0;
                _curImage = i;
                curTimer = 0;
            }
            if(b == null){
                b = result.gameObject.GetComponentInParent<Button>();
            }
            
            if (b != null && b.IsInteractable())
            {
                curTimer += Time.unscaledDeltaTime;
                //Debug.Log(curTimer);
                if(_curImage != null)
                {
              
                    _curImage.fillAmount = _curImage.fillAmount + Time.unscaledDeltaTime * (holdTime+0.2f);
                }
                if (curTimer > holdTime) {
                    b.onClick.Invoke();
                }
            }
        }
        if (raycastResults.Count == 0)
        {
            curTimer = 0;
            if (_curImage != null)
            {
                _curImage.fillAmount = 0;
                _curImage = null;
            }
                
        }


    }

}
