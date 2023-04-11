using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WebType
{
    http,
    https,
    ipsf
}

public static class BrowserHelper
{
    public static void AccessBrowser(string address, WebType type)
    {
        switch (type)
        {
            case WebType.https:
                address = "https://" + address;
                break;
            case WebType.http:
                address = "http://" + address;
                break;
            default:
                break;
        }

        System.Diagnostics.Process.Start(address);
    }

    public static void AccessBrowser(string address)
    {
        if (address.StartsWith("http"))
        {
            System.Diagnostics.Process.Start(address);
        }
    }
}
