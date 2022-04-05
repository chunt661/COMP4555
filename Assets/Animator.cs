using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public GameObject _Object;
    public GameObject _Object2;
    public GameObject _Object3;
    public GameObject _Object4;
    public GameObject _Object5;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _Object4.SetActive(true);
            _Object2.SetActive(false);
            _Object.SetActive(false);
            _Object5.SetActive(true);
            

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            _Object.SetActive(true);
            _Object2.SetActive(false);
        }


    }
}
