using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkClient : SocketIOComponent
{
    // Start is called before the first frame update

    //public GameObject avatar;
    //private Dictionary<string, GameObject> serverObjects;
    private string id;
    [SerializeField]
    private string player = "Spirit";
    [SerializeField]
    private GroundTracker groundTracker;
    [SerializeField]
    private Transform traveler;
    private Vector3 sentPos;

    public override void Start()
    {
        base.Start();
        init();
        setupEvents();
    }

    private void init() {
        //serverObjects = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void setupEvents() {
        On("open", (E) => {
            Debug.Log("connected to server");
        });

        On("userId", (E) => {
            string id = E.data["id"].ToString().Replace("\"", "");
            this.id = id;
        });

        On("AddCube", (E) => {
            string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            groundTracker.AddCube(pos, 0);

        });

         On("RemoveCube", (E) => {
            string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            groundTracker.RemoveCube(pos);

        });

        On("MoveTraveler", (E) => {
            string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            traveler.position = pos / 1000;
        });
    }


    public void snedAddCube(Vector3 pos) {
        Emit("AddCube", new JSONObject("{\"id\":\"" + id + "\",\"x\":" + pos.x + ",\"y\":" + pos.y + ",\"z\":" + pos.z + "}" ));
    }

    public void snedRemoveCube(Vector3 pos) {
        Emit("RemoveCube", new JSONObject("{\"id\":\"" + id + "\",\"x\":" + pos.x + ",\"y\":" + pos.y + ",\"z\":" + pos.z + "}" ));
    }

    public void sendMoveTraveler(Vector3 pos) {
        float x = Mathf.Round(pos.x * 1000) ;
        float y = Mathf.Round(pos.y * 1000) ;
        float z = Mathf.Round(pos.z * 1000) ;

        Vector3 newPos = new Vector3(x,y,z);
        if (newPos == sentPos && player != "Spirit") {
            return;
        }
        sentPos = newPos;

        Position thePos = new Position();
        thePos.x = x;
        thePos.y = y;
        thePos.z = z;
        thePos.id = id;

        //Debug.Log("move: " + pos);
        Emit("MoveTraveler", new JSONObject( "{\"id\":\"" + id + "\",\"x\":" +  x + ",\"y\":" +  y + ",\"z\":" +  z + "}" ));
        //Emit("MoveTraveler", new JSONObject(JsonUtility.ToJson(thePos)));
        //Emit("MoveTraveler", new JSONObject( "{\"id\": \"hej\",\"x\": 1,\"y\": 1,\"z\": 1}" ));
    }

    [System.Serializable]
    class Position {
        public string id;
        public float x;
        public float y;
        public float z;

    }
}
