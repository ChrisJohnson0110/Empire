using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empire
{
    public string empireName;
    public Material OwnedMaterial; //material for land owned by this empire
    public List<City> cities = new List<City>();
    //score ?


    //values from owned
    //number of worked resources

    public Empire(string a_empireName, Material material)
    {
        empireName = a_empireName;
        OwnedMaterial = material;
    }

    

}
