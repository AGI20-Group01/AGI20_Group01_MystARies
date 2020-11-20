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
    private GroundTracker groundTracker;
    [SerializeField]
    private Transform traveler;
    [SerializeField]
    private TravelerNetworking travelerNetworking;


    public override void Start()
    {
        base.Start();
        setupEvents();
    }

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
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            pos = ArTOWorldPos(pos);
            groundTracker.AddCube(pos, 0);

        });

         On("RemoveCube", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            pos = ArTOWorldPos(pos);
            groundTracker.RemoveCube(pos);

        });

        On("MoveTraveler", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            pos = ArTOWorldPos(pos);
            //traveler.position = pos / 1000;
            travelerNetworking.setTargetPos(pos);
        });


        On("RotateTraveler", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 rot = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            //Debug.Log(rot);
            //traveler.rotation = Quaternion.Euler(rot / 1000); 
            travelerNetworking.setTargetRot(rot);
        });

        On("RotateWorld", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 rot = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            //Debug.Log(rot);
            //traveler.rotation = Quaternion.Euler(rot / 1000); 
            groundTracker.transform.rotation = Quaternion.Euler(rot);
        });
    }


    public void snedAddCube(Vector3 pos) {
        pos = WorldToArPos(pos);
        Emit("AddCube", new JSONObject("{\"id\":\"" + id + "\",\"x\":" + pos.x + ",\"y\":" + pos.y + ",\"z\":" + pos.z + "}" ));
    }

    public void snedRemoveCube(Vector3 pos) {
        pos = WorldToArPos(pos);
        Emit("RemoveCube", new JSONObject("{\"id\":\"" + id + "\",\"x\":" + pos.x + ",\"y\":" + pos.y + ",\"z\":" + pos.z + "}" ));
    }

    public void sendMoveTraveler(Vector3 pos) {
        pos = pos / 1000;
        pos = WorldToArPos(pos);
        Emit("MoveTraveler", new JSONObject( "{\"id\":\"" + id + "\",\"x\":" +  pos.x + ",\"y\":" +  pos.y + ",\"z\":" +  pos.z + "}" ));
    }

    public void sendRotate(Vector3 rot) {
        rot = rot / 1000;
        Emit("RotateTraveler", new JSONObject( "{\"id\":\"" + id + "\",\"x\":" +  rot.x + ",\"y\":" +  rot.y + ",\"z\":" +  rot.z + "}" ));
    }

    public void sendWorldRotate(Vector3 rot) {
        Emit("RotateWorld", new JSONObject( "{\"id\":\"" + id + "\",\"x\":" +  rot.x + ",\"y\":" +  rot.y + ",\"z\":" +  rot.z + "}" ));
    }

    private Vector3 WorldToArPos(Vector3 pos) {
        return pos - groundTracker.transform.position;
    }


    private Vector3 ArTOWorldPos(Vector3 pos) { 
        return groundTracker.transform.position + pos;
    }

}
