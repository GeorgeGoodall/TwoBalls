using UnityEngine;
using UnityEngine.Events;
using System.Collections;
    
public class CustomOnClick : MonoBehaviour 
{
    public RopeParams ropeParams;
    public MyOnClickEvent myOnClickEvent;
	
	public void OnClick ()
    {
        myOnClickEvent.Invoke(ropeParams);
    }
}

[System.Serializable]
public class RopeParams
{
    public Color colour;
    public Material material;
    public LineTextureMode textureMode;
    public float width = 0.1f;
}

[System.Serializable]
public class MyOnClickEvent : UnityEvent<RopeParams> {}