using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSystem : MonoBehaviour
{
    [SerializeField]private Pickable[] sortedArray = new Pickable[7];

    private int freeIndex;

    [SerializeField]private RenderUI renderUI;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
                    //bool empty = false;
                    //for (int j = 0; j < sortedArray.Length; j++)
                    //{
                    //    if (sortedArray[j] != null)
                    //    {
                    //        index = j;
                    //        empty = true;
                    //    }
                    //}
                    //if(empty)
                    //{
                    //    index = 0;
                    //}

                    if(count > 0) //match available
                    {
                        AddMatchingItem(count, index, totalUsedIndex,picked);
                    }
                    else //no match
                    {
                        AddNewItem(totalUsedIndex,picked);
                    }

                }
            }


            ///check for gameover
            ///
        }
    }

    private void AddMatchingItem(int count,int lastItemIndex,int totalUsedIndex, Pickable picked)
    {
        if (count == 2) //a match  
        {
            //match

            //sort the rest animation
            if (totalUsedIndex < sortedArray.Length)
            {
                if (sortedArray[lastItemIndex + 1] != null) SortTheRest(lastItemIndex + 1, totalUsedIndex);
                else AddNewItem(lastItemIndex + 1, picked);

            }
            sortedArray[lastItemIndex + 1] = sortedArray[lastItemIndex];

            AddNewItem(lastItemIndex + 1, picked);

            DisableMatchedItems(lastItemIndex+1);
            //vanish

            MoveItemsAfterMatch(lastItemIndex + 2,totalUsedIndex);


            
            //move elements if filled
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
        Debug.Log("Index to sort from: "+indexToSortFrom+" / last index: "+lastIndex);


        for (int i = lastIndex; i > indexToSortFrom; i--)
        {

            sortedArray[i] = sortedArray[i-1]; //you have to start moving from the last element to the next element of the last element
            renderUI.RenderObject(i, sortedArray[i]);

        }
    }
    private void DisableMatchedItems(int lastItemIndex)
    {
        for (int i = lastItemIndex; i >= lastItemIndex-2; i--)
        {
            sortedArray[i].gameObject.SetActive(false);
            sortedArray[i] = null;
            //renderUI.RemoveObject(i);

        }
    }
    private void MoveItemsAfterMatch(int indexToSortFrom, int lastIndex)
    {
        for (int i = indexToSortFrom; i <= lastIndex; i++)
        {
            Debug.Log("MoveItemsAfterMatch - i " + (i - 3)+" i - "+i);
            sortedArray[i -3] = sortedArray[i];
            Debug.Log(sortedArray[i].gameObject.name+" MoveItemsAfterMatch - the object to move" + sortedArray[i - 3].gameObject.name);

            renderUI.RenderObject(i -3, sortedArray[i -3]);
            sortedArray[i] = null;

        }
    }

    private void AddNewItem(int indexToPut,Pickable picked)
    {
        sortedArray[indexToPut] = picked;
        renderUI.RenderObject(indexToPut, picked);
    }   

    
}
