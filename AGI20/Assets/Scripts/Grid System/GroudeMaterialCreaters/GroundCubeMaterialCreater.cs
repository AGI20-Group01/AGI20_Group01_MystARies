using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterialCeater", menuName = "Mterial Creaters/CubeMeterial")]
public class GroundCubeMaterialCreater : CubeMaterialCreater
{
    public Texture sumOnTop;
    public Texture autOnTop;
    public Texture witOnTop;
    public Texture sprOnTop;

    public Texture sumOnBottom;
    public Texture autOnBottom;
    public Texture witOnBottom;
    public Texture sprOnBottom;

    public Texture noise;
    public Texture normal;


    [Range(2,50)]
    public int ShadingCells = 2;

    [Range(1,200)]
    public float specualer = 1;

    override public Material GetMaterial(int value) {
        Material newMaterial = new Material(Shader.Find("Custom/GrundeCubeShader"));

        byte bytes = (byte) value;
        BitArray b = new BitArray(new int[] { value });

        newMaterial.SetTexture("_Main1Tex", (b[0]) ? sumOnBottom : sumOnTop);
        newMaterial.SetTexture("_Main2Tex", (b[1]) ? autOnBottom : autOnTop);
        newMaterial.SetTexture("_Main3Tex", (b[2]) ? witOnBottom : witOnTop);
        newMaterial.SetTexture("_Main4Tex", (b[3]) ? sprOnBottom : sprOnTop);
        newMaterial.SetTexture("_NoiseTex", noise);
        newMaterial.SetTexture("_BumpMap", normal);

        newMaterial.SetInt("_cells", ShadingCells);      
        newMaterial.SetFloat("_P", specualer);        
            
        return newMaterial; 
    }
}
