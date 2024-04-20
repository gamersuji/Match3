using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RenderUI : MonoBehaviour
{

    [SerializeField] private Transform[] stackUI;
    [SerializeField] private float goSpeed = 2f;

    // Update is called once per frame
    public void RenderObject(int index,Pickable choosenObject)
    {
        choosenObject.gameObject.layer = LayerMask.NameToLayer("UIObject");
        choosenObject.DisableRigidBody();
        choosenObject.transform.position = stackUI[index].transform.position;
    }

    public void MoveObjectRight(int index, Pickable choosenObject)
    {
        choosenObject.DisableRigidBody();
        choosenObject.transform.DOMove(stackUI[index].transform.position, 0.2f).SetEase(Ease.OutExpo);
    }

    public void MoveObjectLeft(int index, Pickable pickableObjects)
    {
        pickableObjects.transform.DOMove(stackUI[index].transform.position, 0.2f).SetEase(Ease.OutExpo);
    }


    public void GoToPoint(int index, Pickable choosenObject,Action callback)
    {
        choosenObject.transform.localScale = Vector3.one * 0.15f;
        choosenObject.DisableRigidBody();
        choosenObject.transform.eulerAngles = new Vector3(-90, 180, 0);

        choosenObject.transform.DOMove(stackUI[index].transform.position, goSpeed).SetEase(Ease.OutExpo).OnComplete(() => { callback?.Invoke(); });
    }

    public void RemoveObject(int index)
    {
         stackUI[index].gameObject.SetActive(false);
    }

    public async void MatchObjects(Pickable[] matchableItems,int lastIndex,Action callback)
    {   

       matchableItems[2].transform.DOMove(stackUI[lastIndex - 1].transform.position, 0.2f).SetEase(Ease.InBack);
       await matchableItems[0].transform.DOMove(stackUI[lastIndex -1].transform.position, 0.2f).SetEase(Ease.InBack).AsyncWaitForCompletion();

        for (int i = 0; i < matchableItems.Length; i++)
        {
            matchableItems[i].gameObject.SetActive(false);
        }

        callback();
    }
}
    