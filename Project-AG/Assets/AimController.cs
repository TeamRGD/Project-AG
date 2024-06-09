using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

public class AimController : MonoBehaviour
{
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private Vector2 targetPosition;
    private bool isTargetPositionReceived = false;

    public Transform playerBody; // Player's body Transform
    public Camera playerCamera;  // Player's camera

    public float sensitivity = 1.0f; // Sensitivity for mouse movement

    // Windows API functions for simulating mouse events
    [DllImport("user32.dll", SetLastError = true)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;

    void Start()
    {
        tcpListenerThread = new Thread(new ThreadStart(ListenForData));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    void Update()
    {
        if (isTargetPositionReceived)
        {
            // Convert the received position to screen position
            Vector3 screenPoint = new Vector3(targetPosition.x, targetPosition.y, playerCamera.nearClipPlane);
            Vector3 worldPoint = playerCamera.ScreenToWorldPoint(screenPoint);
            Vector3 direction = (worldPoint - playerCamera.transform.position).normalized;

            // Calculate the horizontal (Yaw) and vertical (Pitch) angles
            float yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float pitch = Mathf.Asin(direction.y) * Mathf.Rad2Deg;

            // Rotate the player body to face the target direction horizontally
            playerBody.rotation = Quaternion.Euler(0, yaw, 0);
            //playerBody.rotation = Quaternion.Euler(0, yaw, 0);

            // Rotate the camera to face the target direction vertically
            playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);

            // Invoke SimulateMouseClick with a 0.1 second delay
            Invoke("SimulateMouseClick", 0.1f);

            // Reset the flag
            isTargetPositionReceived = false;
        }
    }

    private void ListenForData()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
            tcpListener.Start();
            Debug.Log("Server is listening");

            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (TcpClient client = tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            string clientMessage = Encoding.ASCII.GetString(incomingData);
                            Debug.Log("Received message: " + clientMessage);

                            // Parse the received message
                            string[] coordinates = clientMessage.Split(',');
                            if (coordinates.Length == 2 &&
                                float.TryParse(coordinates[0], out float x) &&
                                float.TryParse(coordinates[1], out float y))
                            {
                                targetPosition = new Vector2(x, y);
                                isTargetPositionReceived = true;
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    private void SimulateMouseClick()
    {
        // Simulate mouse click
        Debug.Log("Simulating mouse click");
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    private void OnApplicationQuit()
    {
        tcpListener.Stop();
    }
}