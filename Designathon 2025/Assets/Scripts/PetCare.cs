using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using NUnit.Framework.Constraints;

public class PetCare : MonoBehaviour
{
    public Questionnaire questionnaire;
    public GameObject egg;
    public Transform eggTransform;
    public GameObject pet;
    public Transform petTransform;
    public GameObject character;
    public Transform characterTransform;
    public GameObject tool;
    public Sprite[] toolSprites;
    public TextMeshProUGUI instructions;
    [SerializeField] int toolIndex;
    [TextArea][SerializeField] string[] toolDescriptions;
    public GameObject sneeze;
    public GameObject runnyNose;
    void Start()
    {
        toolIndex = -1;
    }

    void Update()
    {
        
    }

    public void finishCare(){
        instructions.text = "Your pet's feeling a lot better and soon a doctor will help you feel better too!";
    }

    public void startCare(){
        StartCoroutine(eggHatch());
    }

    private IEnumerator eggHatch()
    {
        eggTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -10));
        yield return new WaitForSeconds(0.25f);
        eggTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 10));
        yield return new WaitForSeconds(0.25f);
        eggTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -10));
        yield return new WaitForSeconds(0.25f);
        eggTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 10));
        yield return new WaitForSeconds(0.25f);
        egg.SetActive(false);
        
        characterTransform.position = new Vector3(-7, 0, 0);
        petTransform.position = new Vector3(-7, 2, 0);
        character.SetActive(true);
        pet.SetActive(true);
        
        StartCoroutine(moveToPosition(characterTransform, new Vector3(-1, 0, 0), 1));
        StartCoroutine(moveToPosition(petTransform, new Vector3(6, 2, 0), 1));
        
        instructions.text = "Let's help your pet feel better!";
    }

    private IEnumerator moveToPosition(Transform transform, Vector3 endPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = endPosition;

        tool.SetActive(true);
        
        selectTool();

    }

    public void selectTool(){
        toolIndex++;
        if(toolIndex >= toolSprites.Length)
        {
            tool.GetComponent<Transform>().position = new Vector3(0, 0, 0);
            tool.GetComponent<SpriteRenderer>().sprite = null;
            finishCare();
            return;
        }
        if (toolIndex >= 0)
        {
            instructions.text = toolDescriptions[toolIndex];
        }
        tool.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        tool.GetComponent<SpriteRenderer>().sprite = toolSprites[toolIndex];
    }

    public void dragObject(){
        Transform toolTransform = tool.GetComponent<Transform>();
        toolTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
