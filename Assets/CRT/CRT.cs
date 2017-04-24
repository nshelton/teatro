using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
public class CRT : MonoBehaviour {
    #region Variables
    public Shader curShader;
    public float Distortion = 0.1f;
    public float InputGamma = 2.4f;
    public float OutputGamma = 2.2f;
    private Material curMaterial;
    #endregion
   
    #region Properties
    Material material
    {
        get
        {
            if(curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave; 
            }
            return curMaterial;
        }
    }
    #endregion
    // Use this for initialization
    void Start ()
    {
        if(!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }
   
    void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if(curShader != null)
        {
            material.SetFloat("_Distortion", Distortion);
            material.SetFloat("_InputGamma", InputGamma);
            material.SetFloat("_OutputGamma", OutputGamma);
            material.SetVector("_TextureSize", new Vector2(512.0f, 512.0f));
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture); 
        }
       
       
    }
   
    // Update is called once per frame
    void Update ()
    {
       
    }
   
    void OnDisable ()
    {
        if(curMaterial)
        {
            DestroyImmediate(curMaterial); 
        }
       
    }
   
   
}