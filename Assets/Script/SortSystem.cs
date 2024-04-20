using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSystem : MonoBehaviour
{
    [SerializeField]private Pickable[] sortedArray = new Pickable[7];

    private int freeIndex;

    [SerializeField]private RenderUI renderUI;

    private int totalItems = 20;
    private int matchedPair = 0;

    [SerializeField] private GameManager gameManager;
 

    public void PickedItem(Pickable picked)
    {

       if(freeIndex< sortedArray.Length)
        {
            int count = 0;
            int index = 0;
            int totalUsedIndex = 0;
            bool matchingType = false;

            for (int i = 0; i < sortedArray.Length; i++)
            {
                if (sortedArray[i] != null)
                {
                    ++totalUsedIndex;

                    if (sortedArray[i].type == picked.type)
                    {
                        ++count;
                        index = i;
                    }
                }

                if(sortedArray.Length -1 == i)
                {
    
                    if(count > 0) //match available
                    {

                        if (totalUsedIndex == sortedArray.Length - 1 && count != 2)
                        {
                            gameManager.GameOver();
                        }
                        else
                        {
                            AddMatchingItem(count, index, totalUsedIndex, picked);

                        }
                    }
                    else //no match
                    {
                        if (totalUsedIndex == sortedArray.Length - 1 && count != 2)
                        {
                            gameManager.GameOver();
                        }
                        else
                            AddNewItem(totalUsedIndex,picked);
                      
                    }

                }
            }
        }
    }

    private void AddMatchingItem(int count,int lastItemIndex,int totalUsedIndex, Pickable picked)
    {
        if (count == 2) //a match  
        {
            //match

            ++matchedPair;

            //sort the rest animation
            if (totalUsedIndex < sortedArray.Length)
            {
                if (sortedArray[lastItemIndex + 1] != null) SortTheRest(lastItemIndex + 1, totalUsedIndex);
                else AddNewItem(lastItemIndex + 1, picked);

            }
            sortedArray[lastItemIndex + 1] = sortedArray[lastItemIndex];

            AddNewItem(lastItemIndex + 1, picked,()=>
            {
                DisableMatchedItems(lastItemIndex + 1, () =>
                {
                    MoveItemsAfterMatch(lastItemIndex + 2, totalUsedIndex);
                });

            }
            );

            if(matchedPair == totalItems)
            {
                gameManager.GameWin();
            }

        }
        else if (count == 1) //less than a match
        {
            //add up

            if (sortedArray[lastItemIndex + 1] == null) // add at the end
            {
                //add up animation
                sortedArray[lastItemIndex + 1] = sortedArray[lastItemIndex];
                AddNewItem(lastItemIndex + 1, picked);
            }
            else //add at the end by making a gap
            {
                //sort the rest animation
                if (totalUsedIndex < sortedArray.Length && sortedArray[lastItemIndex + 1] != null) SortTheRest(lastItemIndex + 1, totalUsedIndex);
                sortedArray[lastItemIndex + 1] = sortedArray[lastItemIndex];
                AddNewItem(lastItemIndex + 1, picked);
                //add up animation

            }
        }
    }

    private void SortTheRest(int indexToSortFrom,int lastIndex)
    {
        for (int i = lastIndex; i > indexToSortFrom; i--)
        {
            sortedArray[i] = sortedArray[i-1]; //you have to start moving from the last element to the next element of the last element
            renderUI.MoveObjectRight(i, sortedArray[i]);

        }
    }
    private void DisableMatchedItems(int lastItemIndex,Action callback)
    {
     

        Pickable[] sortedArray2 =  { sortedArray[lastItemIndex-2], sortedArray[lastItemIndex-1] , sortedArray[lastItemIndex]};
        for (int i = lastItemIndex; i >= lastItemIndex - 2; i--)
        {
            sortedArray[i] = null;

        }

        renderUI.MatchObjects(sortedArray2, lastItemIndex, () => { callback(); }); 
    }
    private void MoveItemsAfterMatch(int indexToSortFrom, int lastIndex)
    {
        for (int i = indexToSortFrom; i <= lastIndex; i++)
        {
            sortedArray[i -3] = sortedArray[i];
            renderUI.MoveObjectLeft(i -3, sortedArray[i -3]);
            sortedArray[i] = null;

        }
    }

    private void AddNewItem(int indexToPut,Pickable picked,Action callback = null)
    {
        sortedArray[indexToPut] = picked;
        //renderUI.RenderObject(indexToPut, picked);
        renderUI.GoToPoint(indexToPut, picked,callback);



    }


}
