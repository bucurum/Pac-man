using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> avaliableDirections {get; private set;}

    void Start()
    {
        this.avaliableDirections = new List<Vector2>();

        
    }
}
