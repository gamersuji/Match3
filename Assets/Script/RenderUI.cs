using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderUI : MonoBehaviour
{

    [SerializeField] private RectTransform[] stackUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void RenderObject(int index,Pickable choosenObject)
    {

        //var worldCoords = stackUI[index].TransformPoint(stackUI[index].rect.center);
        //choosenObject.transform.localPosition = Camera.main.ScreenToWorldPoint(worldCoords); ;

        choosenObject.gameObject.layer = LayerMask.NameToLayer("UIObject");
        Debug.Log("Stack UI " + stackUI[index].transform.position);
        choosenObject.DisableRigidBody();

        choosenObject.transform.position = stackUI[index].transform.position;
    }

    public void RemoveObject(int index)
    {

        //var worldCoords = stackUI[index].TransformPoint(stackUI[index].rect.center);
        //choosenObject.transform.localPosition = Camera.main.ScreenToWorldPoint(worldCoords); ;

         stackUI[index].gameObject.SetActive(false);
    }
}
    