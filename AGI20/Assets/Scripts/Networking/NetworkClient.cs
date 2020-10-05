﻿using System.Collections;
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
            groundTracker.AddCube(pos, 0);

        });

         On("RemoveCube", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            groundTracker.RemoveCube(pos);

        });

        On("MoveTraveler", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 pos = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            traveler.position = pos / 1000;
        });


        On("RotateTraveler", (E) => {
            //string id = E.data["id"].ToString().Replace("\"", "");
            Vector3 rot = new Vector3(float.Parse(E.data["x"].ToString()), float.Parse(E.data["y"].ToString()),float.Parse(E.data["z"].ToString()));
            Debug.Log(rot);
            traveler.rotation = Quaternion.Euler(rot / 1000);
        });
    }


    public void snedAddCube(Vector3 pos) {
        Emit("AddCube", new JSONObject("{\"id\":\"" + id + "\",\"x\":" + pos.x + ",\"y\":" + pos.y + ",\"z\":" + pos.z + "}" ));
    }

    public void snedRemoveCube(Vector3 pos) {
        Emit("RemoveCube", new JSONObject("{\"id\":\"" + id + "\",\"x\":" + pos.x + ",\"y\":" + pos.y + ",\"z\":" + pos.z + "}" ));
    }

    public void sendMoveTraveler(Vector3 pos) {
        Emit("MoveTraveler", new JSONObject( "{\"id\":\"" + id + "\",\"x\":" +  pos.x + ",\"y\":" +  pos.y + ",\"z\":" +  pos.z + "}" ));
    }

    public void sendRotate(Vector3 rot) {
        Emit("RotateTraveler", new JSONObject( "{\"id\":\"" + id + "\",\"x\":" +  rot.x + ",\"y\":" +  rot.y + ",\"z\":" +  rot.z + "}" ));
    }

}
