using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaterialCreater : ScriptableObject {

    public virtual Material GetMaterial(int value) {
        Material newMaterial = new Material(Shader.Find("Standard"));            
        return newMaterial; 
    }

}
