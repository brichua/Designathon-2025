using UnityEngine;

public class PetManager : MonoBehaviour
{
    Character_Manager characterManager;
    public GameObject uiManager;
    [SerializeField] Questionnaire questionnaire;
    [SerializeField] GameObject sneeze;
    [SerializeField] GameObject runnyNose;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterManager = GetComponent<Character_Manager>();   
        questionnaire = uiManager.GetComponent<Questionnaire>();

        PatientReport patientReport = questionnaire.report;
        if (patientReport.hasPain)
        {
            
            if(patientReport.painArm || patientReport.painLegs)
            {
                characterManager.shiftSprite(characterManager.bodyParts[2], 1);
            }
            else
            {
                characterManager.shiftSprite(characterManager.bodyParts[2], 0);
            }
        }
        else
        {
            characterManager.shiftSprite(characterManager.bodyParts[2], 2);
        }

        if (patientReport.hardToHear)
        {
           characterManager.shiftSprite(characterManager.bodyParts[2], 1);
        }

        if(patientReport.feelDizzy || patientReport.feelUncomfortable)
        {
            characterManager.shiftSprite(characterManager.bodyParts[4], 2);
        }
        else if (patientReport.feelHot || patientReport.feelTired)
        {
            characterManager.shiftSprite(characterManager.bodyParts[4], 1);
        }
        else
        {
            characterManager.shiftSprite(characterManager.bodyParts[4], 0);
        }

        if(patientReport.hasPain || patientReport.feelUncomfortable || patientReport.hurtBreathe || patientReport.hurtWalk
            || patientReport.hasVomited || patientReport.feelDizzy)
        {
            characterManager.shiftSprite(characterManager.bodyParts[5], 0);
        }
        else
        {
            characterManager.shiftSprite(characterManager.bodyParts[5], 1);
        }

        if(patientReport.runnyNose || patientReport.itchyNose)
        {
            runnyNose.SetActive(true);
        }

        if (patientReport.hasCoughed)
        {
            sneeze.SetActive(true);
        }

        if (patientReport.hardToSee)
        {
            characterManager.shiftSprite(characterManager.bodyParts[3], 1);
        }else if (patientReport.itchyEyes)
        {
            characterManager.shiftSprite(characterManager.bodyParts[3], 2);
        }else if (patientReport.runnyEyes)
        {
            characterManager.shiftSprite(characterManager.bodyParts[3], 3);
        }
        else
        {
            characterManager.shiftSprite(characterManager.bodyParts[3], 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
