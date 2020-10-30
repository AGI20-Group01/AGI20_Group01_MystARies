using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterialCeater", menuName = "Mterial Creaters/TestCubeMeterial")]
public class TestCubeMaterialCreater : CubeMaterialCreater
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

    public Color topCol1;
    public Color topCol2;
    public Color topCol3;
    public Color topCol4;

    public Color sideCol1;
    public Color sideCol2;


    [Range(2,50)]
    public int ShadingCells = 2;

    [Range(1,200)]
    public float specualer = 1;

    override public Material GetMaterial(int value) {
        Material newMaterial = new Material(Shader.Find("Custom/GrundeCubeTestShader"));

        byte bytes = (byte) value;
        BitArray b = new BitArray(new int[] { value });

        newMaterial.SetTexture("_Main1Tex", (b[0]) ? sumOnBottom : sumOnTop);
        newMaterial.SetTexture("_Main2Tex", (b[1]) ? autOnBottom : autOnTop);
        newMaterial.SetTexture("_Main3Tex", (b[2]) ? witOnBottom : witOnTop);
        newMaterial.SetTexture("_Main4Tex", (b[3]) ? sprOnBottom : sprOnTop);
        newMaterial.SetTexture("_NoiseTex", noise);
        newMaterial.SetTexture("_BumpMap", normal);

        newMaterial.SetColor("_topColor1", topCol1);
        newMaterial.SetColor("_topColor2", topCol2);
        newMaterial.SetColor("_topColor3", topCol3);
        newMaterial.SetColor("_topColor4", topCol4);

        newMaterial.SetColor("_sideColor1", sideCol1);
        newMaterial.SetColor("_sideColor2", sideCol2);

        newMaterial.SetInt("_cells", ShadingCells);      
        newMaterial.SetFloat("_P", specualer);        
            
        return newMaterial; 
    }
}
