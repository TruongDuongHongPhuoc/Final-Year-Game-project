using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UDP_Client : MonoBehaviour
{
   public static UdpClient client;
   private void Start() {
    client = new UdpClient();
   //  Debug.Log("UDP client created");
   }
}
