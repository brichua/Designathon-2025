using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public bool overPet = false;
    GameObject petCareManager;
    PetCare petCare;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        petCareManager = GameObject.Find("PetCare Manager");
        petCare = petCareManager.GetComponent<PetCare>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 offset;

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        if (overPet)
        {
            petCare.selectTool();
            overPet = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Pet"))
        {
            overPet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pet"))
        {
            overPet = false;
        }
    }

}
