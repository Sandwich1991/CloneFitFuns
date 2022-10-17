using UnityEngine;
using Random = UnityEngine.Random;

public class PlayZone : MonoBehaviour
{
    [SerializeField] private Transform aZone;
    [SerializeField] private Transform bZone;
    [SerializeField] private Transform cZone;

    private BoxCollider aBox;
    private BoxCollider bBox;
    private BoxCollider cBox;

    private int aQuantity = 10;
    private int bQuantity = 10;
    private int cQuantity = 10;
    
    public static int ACurItems = 0;
    public static int BCurItems = 0;
    public static int CCurItems = 0;

    private void Awake()
    {
        aBox = aZone.GetComponent<BoxCollider>();
        bBox = bZone.GetComponent<BoxCollider>();
        cBox = cZone.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        MakeItem(aBox, ref aQuantity, ref ACurItems, "ItemA");
        MakeItem(bBox, ref bQuantity, ref BCurItems, "ItemB");
        MakeItem(cBox, ref cQuantity, ref CCurItems, "ItemC");
        
        print($"Azone : {ACurItems}  Bzone : {BCurItems}  Czone : {CCurItems}");
    }

    private void MakeItem(BoxCollider boxCollider, ref int itemQuantity, ref int curItems, string itemPath)
    {

        for (int i = curItems; i < itemQuantity; i++)
        {
            if (curItems >= itemQuantity)
            {
                return;
            }
            
            float xpos = boxCollider.size.x / 2f;
            float ypos = boxCollider.size.z / 2f;

            Vector2 pos = new Vector2(Random.Range(-xpos, xpos), Random.Range(-ypos, ypos));
        
            GameObject item = Managers.Resource.Instantiate(itemPath, boxCollider.transform);
            item.transform.localPosition = new Vector3(pos.x, 1, pos.y);

            curItems++;
        }
    }
}
