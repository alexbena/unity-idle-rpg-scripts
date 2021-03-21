using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// REFERENCES TO ALL ASSETS NEEDED
public class AssetsManager : MonoBehaviour
{
    private static AssetsManager _i;

    public static AssetsManager i {
        get 
        {
            if (_i == null) _i = Instantiate(Resources.Load<AssetsManager>("AssetsManager"));

            return _i;
        }
    }


}
