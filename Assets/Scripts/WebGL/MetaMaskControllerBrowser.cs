using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class MetaMaskControllerBrowser : MonoBehaviour
{
    public static MetaMaskControllerBrowser instance;

    [DllImport("__Internal")] private static extern void ConnectMetamask();
    [DllImport("__Internal")] private static extern void SetCookie(string cname, string cvalue, int exdays);
    [DllImport("__Internal")] private static extern void GetCookie(string cname);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void ConnectMetaMaskWeb()
    {
        ConnectMetamask();
    }

    public void SetCookieWeb(string cname, string cvalue, int exdays)
    {
        SetCookie(cname, cvalue, exdays);
    }

    //recive cookie
    public void ReciveCookieMsg(string str)
    {

        float x = Convert.ToSingle(str);
    }

    public void SetAddress(string address)
    {
        // UserInfo.userAddress = address;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
