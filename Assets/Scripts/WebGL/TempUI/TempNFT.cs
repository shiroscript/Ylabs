using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNFT : MonoBehaviour
{
    public bool isSelect = false;
    public GameObject nftBG;
    public int num;

    public void SwitchSelect()
    {
        isSelect = !isSelect;
        nftBG.SetActive(isSelect);
    }
}
