using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour
{

    public int point = 0;

    public TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "PickUp")
        {
            point++;
            pointText.text = "PESCADOS: " + point.ToString();
            Debug.Log(point);
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Cielo")
        {
            Destroy(other.gameObject);  
        }
    }

}
