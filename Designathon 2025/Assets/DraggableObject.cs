using UnityEngine;
using System.Collections;

public class DraggableObject : MonoBehaviour
{
    public bool overPet = false;
    GameObject petCareManager;
    public AudioSource audioSource;
    public AudioClip sfxClip;
    PetCare petCare;
    public GameObject floatingImagePrefab;
    public Transform spawnParent;
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

            if (audioSource != null && sfxClip != null)
            {
                audioSource.PlayOneShot(sfxClip);
            }

            if (floatingImagePrefab != null)
            {
                StartCoroutine(SpawnAndFloatImage());
            }
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

    private IEnumerator SpawnAndFloatImage()
    {
        // Spawn the image
        GameObject imageObj = Instantiate(floatingImagePrefab, spawnParent);
        RectTransform rt = imageObj.GetComponent<RectTransform>();
        CanvasGroup cg = imageObj.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = imageObj.AddComponent<CanvasGroup>();

        rt.localPosition = new Vector3(145, -14, 0);
        cg.alpha = 1f;

        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = rt.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 100, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            rt.localPosition = Vector3.Lerp(startPos, endPos, t);
            cg.alpha = Mathf.Lerp(1f, 0f, t);

            yield return null;
        }

        cg.alpha = 0f;
        Destroy(imageObj);
    }
}
