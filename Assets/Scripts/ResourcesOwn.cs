using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceOwn : MonoBehaviour
{
    [SerializeField]
    TextMeshPro resourceDisplay;

    public int resource
    {
        set { resource = value; resourceDisplay.text = resource.ToString(); }
        get { return resource; }
    }
}
