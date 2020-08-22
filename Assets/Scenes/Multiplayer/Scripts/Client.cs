using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{

    //###################bytes and stuff######################//
    private byte reliableChannel;
    private const int MAX_USER = 100;
    private const int PORT = 8080;//100% dangerous cause idk what port to use//
    private const int WEB_PORT = 8081;//100% dangerous cause idk what port to use//
    private const int BYTE_SIZE = 1024;
    private const string SERVER_IP = "127.0.0.1";
    private int hostid;
    private byte error;
    private bool isStarted;
    //####################bytes and stuff######################//

    #region Monobehaviour
    private void Start()
    {
        DontDestroyOnLoad(gameObject);//nevr remove this//
        Init();
    }
    private void Update()
    {
        UpdateMessagePump();
    }
    #endregion

    public void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        cc.AddChannel(QosType.Reliable);

        //can be max_user or max_Player
        HostTopology topo = new HostTopology(cc, MAX_USER);

        //Client only code
        hostid = NetworkTransport.AddHost(topo, 0);


#if UNITY_WEBGL && !UNITY_EDITOR
        //Standalone Client
        NetworkTransport.Connect(hostid, SERVER_IP, PORT, 0, out error);//this stops connection if there is an error(out error)
        Debug.Log('connectin from standalone...');
#else
        //Web Client
        NetworkTransport.Connect(hostid, SERVER_IP, WEB_PORT, 0, out error);//this stops connection if there is an error(out error)
         Debug.Log("connectin from webgl...");
#endif
        Debug.Log(string.Format("attempin to connect on {0}....", SERVER_IP));
        isStarted = true;//just incase somethin goes wrong//
    }

    public void Shutdown()//just incase if somethin goes wrong//
    {
        isStarted = false;
        NetworkTransport.Shutdown();
    }

    public void UpdateMessagePump()
    {
        if (!isStarted)
            return;

        int recHostId;              //web or standalone//
        int connectionId;           //witch user is sendin me this//
        int channelId;              //which lane is he sendin the message from//

        byte[] recBuffer = new byte[BYTE_SIZE];
        int dataSize;

        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, BYTE_SIZE, out dataSize, out error);
        switch (type)
        {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log("we have connected to the server!");
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log("we have disconnected");
                break;

            case NetworkEventType.DataEvent:
                Debug.Log("data");
                break;

            default:
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected Event Has Oucurred");
                break;
        }
    }
    //usefull info//
    //1 = The operation completed successfully
    //2 = WrongHost The operation completed successfully
    //3 = WrongHost The operation completed successfully
    //4 = WrongHost The operation completed successfully
    //5 = BadMessage Not a data message
    //6 = Timeout Connection timed out
    //7 = MessageToLong The message is too long to fit the buffer
    //8 = WrongOperation Operation is not supported
    //9 = CRCMismatch The Networking.ConnectionConfig does not match the other endpoint
    //10 = DNSFailure The address supplied to connect to was invalid or could not be resolved
    //11 = UsageError This error will occur if any function is called with inappropriate parameter values
}