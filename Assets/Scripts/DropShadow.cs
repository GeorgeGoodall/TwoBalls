using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    private Vector2 ShadowOffset = new Vector2(0.05f,-0.05f);
    SpriteRenderer casterSR;
    SpriteRenderer shadowSR;

    private Transform transCaster;
    private Transform transShadow;

    public Material ShadowMaterial;
    public Color shadowColor = new Color(0,0,0,1);

    public int sortingOrder;

    void Start()
    {
        transCaster = transform;
        transShadow = new GameObject("shadow").transform;
        transShadow.parent = transCaster;
        transShadow.localRotation = Quaternion.identity;

        casterSR = GetComponent<SpriteRenderer>();
        shadowSR = transShadow.gameObject.AddComponent<SpriteRenderer>();

        shadowSR.material = ShadowMaterial;
        shadowSR.color = shadowColor;
        shadowSR.sortingLayerName = casterSR.sortingLayerName;
        shadowSR.sortingOrder = casterSR.sortingOrder - 1;
        sortingOrder = casterSR.sortingOrder - 1;
        
    }

    void LateUpdate()
    {
        transShadow.position = new Vector2(
            transCaster.position.x + ShadowOffset.x,
            transCaster.position.y + ShadowOffset.y
        );

        if(shadowSR.sprite != casterSR.sprite) 
            shadowSR.sprite = casterSR.sprite;

        
        Vector3 rescale = transShadow.localScale;
        rescale.y = casterSR.bounds.size.y * rescale.y / shadowSR.bounds.size.y;
        rescale.x = casterSR.bounds.size.x * rescale.x / shadowSR.bounds.size.x;
        transShadow.localScale = rescale;
        
    }

    public void setLayer(int order){
        shadowSR.sortingOrder = sortingOrder + order;
    }

    public void setActive(bool active){
        shadowSR.enabled = active;
    }
}