using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShopPanel : MonoBehaviour
{
    public List<GameObject> panelList = new List<GameObject>();

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (i == 0)
            {
                panelList[i].SetActive(true);
            }
            else
            {
                panelList[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void ShowPanel(int number)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (i == number)
            {
                panelList[i].SetActive(true);
            }
            else
            {
                panelList[i].SetActive(false);
            }
        }
    }
}
