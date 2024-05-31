using System.Collections.Generic;
using UnityEngine;

public class ScrollEnableObjects : MonoBehaviour
{
    // List of GameObjects to enable
    [SerializeField] List<GameObject> objectsToEnable;

    // Current index in the list
    private int currentIndex = 0;

    void Start()
    {
        // Ensure the initial state is correct
        UpdateObjectStates();
    }

    void Update()
    {
        // Get the scroll input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f)
        {
            // Scroll up, move to the next object
            currentIndex = (currentIndex + 1) % objectsToEnable.Count;
            UpdateObjectStates();
        }
        else if (scrollInput < 0f)
        {
            // Scroll down, move to the previous object
            currentIndex = (currentIndex - 1 + objectsToEnable.Count) % objectsToEnable.Count;
            UpdateObjectStates();
        }
    }

    void UpdateObjectStates()
    {
        // Enable the current object and disable others
        for (int i = 0; i < objectsToEnable.Count; i++)
        {
            objectsToEnable[i].SetActive(i == currentIndex);
        }
    }
}
