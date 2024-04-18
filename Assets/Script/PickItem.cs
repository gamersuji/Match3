
using System.Diagnostics;
using UnityEngine;

public class PickItem : MonoBehaviour
{

    [SerializeField]private SortSystem sortSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the mouse position into the scene

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Access the object that was hit
                GameObject hitObject = hit.collider.gameObject;

                // Do something with the hit object
                if (hitObject.GetComponent<Pickable>() != null)
                {
                    sortSystem.PickedItem(hitObject.GetComponent<Pickable>());
                    UnityEngine.Debug.Log("Hit object: " + hitObject.name);
                }
            }
        }
    }
}
