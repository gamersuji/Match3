using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]private Pickable[] pickableResource;

    [SerializeField] private int turns;
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        Spawner();
    }

    private void Spawner()
    {

        int currentTurn = 0;


        while(currentTurn < turns)
        {
            for (int i = 0; i < pickableResource.Length; i++)
            {

                float x = Random.Range(-1.35f, 1.35f);
                float y = Random.Range(2, -1);

                GameObject instance = Instantiate(pickableResource[i].gameObject, transform);
                //instance.transform.position = camera.ScreenToWorldPoint(new Vector3(x,1,y));
                instance.transform.localPosition = new Vector3(x, 2, y);

            }

            ++currentTurn;

        }
    }
}
