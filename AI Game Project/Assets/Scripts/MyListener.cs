using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
public class MyListener : MonoBehaviour
{
    Thread thread;
    public int connectionPort = 25001;
    TcpListener server;
    TcpClient client;
    bool running;


    // Start is called before the first frame update
    void Start()
    {
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();
    }

    void GetData()
    {
        //Create the server
        server = new TcpListener(IPAddress.Any, connectionPort);
        server.Start();

        // Create a client to get the data stream
        client = server.AcceptTcpClient();

        // Start listening
        running = true;
        while (running)
        {
            Connection();
        }
        server.Stop();
    }

    void Connection()
    {
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into a string
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        // Make sure we're not getting an empty string
        //dataReceived.Trim();
        if (dataReceived != null && dataReceived != "")
        {
            // Convert the received string of data to the format we are using
            string[] data = ParseData(dataReceived);

            if (data[0].Equals("p"))
            {
                Vector3 result = new Vector3(
                    float.Parse(data[1]),
                    float.Parse(data[2]),
                    float.Parse(data[3]));
                position = result;
                Debug.Log(position);
            }
            else if(data[0].Equals("s"))
            {
                if (data[1].Equals("m"))
                    scale *= float.Parse(data[2]);
                else if (data[1].Equals("d"))
                    scale /= float.Parse(data[2]);
                else if (data[1].Equals("a"))
                {
                    float adding = float.Parse(data[2]);
                    scale.x += adding;
                    scale.y += adding;
                    scale.z += adding;
                }
                else if (data[1].Equals("s"))
                {
                    float subtract = float.Parse(data[2]);
                    scale.x -= subtract;
                    scale.y -= subtract;
                    scale.z -= subtract;
                }
            }
            nwStream.Write(buffer, 0, bytesRead);
        }
    }

    // Use-case specific function, need to re-write this to interpret whatever data is being sent
    public static string[] ParseData(string dataString)
    {
        Debug.Log(dataString);
        // Remove the parentheses
        if (dataString.StartsWith("(") && dataString.EndsWith(")"))
        {
            dataString = dataString.Substring(1, dataString.Length - 2);
        }

        // Split the elements into an array
        string[] stringArray = dataString.Split(',');

        return stringArray;
    }

    // Position and Scale are the data being received in this example
    Vector3 position = new Vector3(30, 6, 0);
    
    Vector3 scale = new Vector3(1, 1, 1);

    void Update()
    {
        // Set this object's position in the scene according to the position received
        transform.position = position;
        transform.localScale = scale;
    }
}
