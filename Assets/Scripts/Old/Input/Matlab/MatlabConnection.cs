using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System;
using System.Linq;

// Establishes a TCP connection w/Matlab and receives a stream of integers representing predictions
public class MatlabConnection : MonoBehaviour
{
    //[SerializeField] private InputManager inputManager;

    //private Thread receiveThread;
    //private TcpListener tcpListener;
    //private TcpClient tcpClient;
    //private NetworkStream networkStream;
    //private byte[] incomingDataBuffer;

    //public static event Action OnConnectionEstablished;
    //private bool connectionEstablished = false;

    //GraspTypes.GraspNames currentGrasp = GraspTypes.GraspNames.Rest;

    //public void CloseOutEverything()
    //{
    //    networkStream.Close();
    //    tcpClient.Close();
    //    tcpListener.Stop();
    //    receiveThread.Abort();
    //}

    //// NOTE: Needs to be called from inputmanager!
    //private void Start()
    //{
    //    InitializeMatlabInput();
    //}

    //public void InitializeMatlabInput()
    //{
    //    CreateListenerThread();
    //    StartCoroutine(InvokeConnectionAction());
    //}

    //// Wait until the game is initialized before we signal that the connection is established
    //private IEnumerator InvokeConnectionAction()
    //{
    //    while (!connectionEstablished)
    //    {
    //        yield return new WaitForEndOfFrame();
    //    }

    //    yield return new WaitForSecondsRealtime(0.25f);
    //    OnConnectionEstablished?.Invoke(); // can't invoke this action on the new thread!
    //    yield return null;
    //}

    //// Creating a thread in parallel to continuously and indepently await input from MatLab
    //private void CreateListenerThread()
    //{
    //    try
    //    {
    //        receiveThread = new Thread(new ThreadStart(EstablishConnection));
    //        receiveThread.IsBackground = true;
    //        receiveThread.Start();

    //    } catch (Exception e)
    //    {
    //        Debug.Log("Error forming connection: " + e);
    //    }
    //}

    //private void EstablishConnection()
    //{
    //    IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // localhost
    //    int port = 1925; // arbitrary; make sure it lines up with MatLab/external script
    //    tcpListener = new TcpListener(ipAddress, port);

    //    try
    //    {
    //        tcpListener.Start();
    //        incomingDataBuffer = new byte[1024];

    //        while (true)
    //        {
    //            Debug.Log("Waiting for connection...");
    //            tcpClient = tcpListener.AcceptTcpClient();
    //            Debug.Log("Connected!");
    //            connectionEstablished = true;
    //            networkStream = tcpClient.GetStream();
    //            ListenForIncomingData();
    //        }

    //    } catch (SocketException socketException)
    //    {
    //        Debug.Log("SocketException " + socketException.ToString());
    //    } finally
    //    {
    //        tcpListener.Stop(); // Always disposes of the listener
    //    }
    //}

    //private void ListenForIncomingData()
    //{
    //    int streamLength;
    //    string incomingData = null;
    //    while((streamLength = networkStream.Read(incomingDataBuffer, 0, incomingDataBuffer.Length)) != 0)
    //    {

    //        // This lambda function is registered as an action and put on the action queue in UnitymainThreadDispatcher.cs
    //        UnityMainThreadDispatcher.ExecuteOnMainThread(() =>
    //        {
    //            incomingData = Encoding.UTF8.GetString(incomingDataBuffer, 0, streamLength);

    //            currentGrasp = (GraspTypes.GraspNames)int.Parse(incomingData);
    //            inputManager.HandleNewInput(currentGrasp);

    //            // Debug.Log("Received: " + currentGrasp.ToString());
    //        });
    //    }
    //    tcpClient.Close();

    //}

}
