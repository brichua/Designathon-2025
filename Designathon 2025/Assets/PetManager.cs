using UnityEngine;

public class PetManager : MonoBehaviour
{
    public Character_Manager characterManager;
    public GameObject questionnaireManager;
    [SerializeField] Questionnaire questionnaire;
    [SerializeField] GameObject sneeze;
    [SerializeField] GameObject runnyNose;

    public BodyPart[] bodyParts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //characterManager = GetComponent<Character_Manager>();   
        questionnaire = questionnaireManager.GetComponent<Questionnaire>();


    }

    public void nextButton()
    {
        PatientReport patientReport = questionnaire.report;
        if (patientReport.hasPain)
        {

            if (patientReport.painArm || patientReport.painLegs)
            {
                characterManager.shiftSprite(bodyParts[2], 1);
            }
            else
            {
                characterManager.shiftSprite(bodyParts[2], 0);
            }
        }
        else
        {
            characterManager.shiftSprite(bodyParts[2], 2);
        }

        if (patientReport.hardToHear)
        {
            characterManager.shiftSprite(bodyParts[2], 1);
        }

        if (patientReport.feelDizzy || patientReport.feelUncomfortable)
        {
            characterManager.shiftSprite(bodyParts[4], 2);
        }
        else if (patientReport.feelHot || patientReport.feelTired)
        {
            characterManager.shiftSprite(bodyParts[4], 1);
        }
        else
        {
            characterManager.shiftSprite(bodyParts[4], 0);
        }

        if (patientReport.hasPain || patientReport.feelUncomfortable || patientReport.hurtBreathe || patientReport.hurtWalk
            || patientReport.hasVomited || patientReport.feelDizzy)
        {
            characterManager.shiftSprite(bodyParts[5], 0);
        }
        else
        {
            characterManager.shiftSprite(bodyParts[5], 1);
        }

        if (patientReport.runnyNose || patientReport.itchyNose)
        {
            runnyNose.SetActive(true);
        }

        if (patientReport.hasCoughed)
        {
            // sneeze.SetActive(true);
        }

        if (patientReport.hardToSee)
        {
            characterManager.shiftSprite(bodyParts[3], 1);
        }
        else if (patientReport.itchyEyes)
        {
            characterManager.shiftSprite(bodyParts[3], 2);
        }
        else if (patientReport.runnyEyes)
        {
            characterManager.shiftSprite(bodyParts[3], 3);
        }
        else
        {
            characterManager.shiftSprite(bodyParts[3], 0);
        }

        for (int i = 0; i < bodyParts.Length; i++)
        {
            //bodyParts[i].color = GetPetColor(patientReport);

            characterManager.changeColor(bodyParts[i], GetPetColor(patientReport));
        }

    }

    // Update is called once per frame
    void Update()
    {
        PatientReport patientReport = questionnaire.report;
        if (patientReport.hasPain)
        {

            if (patientReport.painArm || patientReport.painLegs)
            {
                characterManager.shiftSprite(bodyParts[2], 1);
            }
            else
            {
                characterManager.shiftSprite(bodyParts[2], 0);
            }
        }
        else
        {
            characterManager.shiftSprite(bodyParts[2], 2);
        }

        if (patientReport.hardToHear)
        {
            characterManager.shiftSprite(bodyParts[2], 1);
        }

        if (patientReport.feelDizzy || patientReport.feelUncomfortable)
        {
            characterManager.shiftSprite(bodyParts[4], 2);
        }
        else if (patientReport.feelHot || patientReport.feelTired)
        {
            characterManager.shiftSprite(bodyParts[4], 1);
        }
        else
        {
            characterManager.shiftSprite(bodyParts[4], 0);
        }

        if (patientReport.hasPain || patientReport.feelUncomfortable || patientReport.hurtBreathe || patientReport.hurtWalk
            || patientReport.hasVomited || patientReport.feelDizzy)
        {
            characterManager.shiftSprite(bodyParts[5], 0);
        }
        else
        {
            characterManager.shiftSprite(bodyParts[5], 1);
        }

        if (patientReport.runnyNose || patientReport.itchyNose)
        {
            runnyNose.SetActive(true);
        }

        if (patientReport.hasCoughed)
        {
            // sneeze.SetActive(true);
        }

        if (patientReport.hardToSee)
        {
            characterManager.shiftSprite(bodyParts[3], 1);
        }
        else if (patientReport.itchyEyes)
        {
            characterManager.shiftSprite(bodyParts[3], 2);
        }
        else if (patientReport.runnyEyes)
        {
            characterManager.shiftSprite(bodyParts[3], 3);
        }
        else
        {
            characterManager.shiftSprite(bodyParts[3], 0);
        }

        for (int i = 0; i < bodyParts.Length; i++)
        {
            //bodyParts[i].color = GetPetColor(patientReport);

            characterManager.changeColor(bodyParts[i], GetPetColor(patientReport));
        }
    }

    Color GetPetColor(PatientReport report)
    {
        // Priority logic: severe first
        if (report.hasPain && (report.hurtBreathe || report.hurtWalk))
            return new Color(0.5f, 0f, 0f); // maroon
        if (report.hurtBreathe || report.hasCoughed)
            return new Color(0.12f, 0.56f, 1f); // dodger blue
        if (report.feelHot || report.feelUncomfortable)
            return new Color(1f, 0.55f, 0f); // dark orange
        if (report.hasVomited || report.painStomach)
            return new Color(1f, 0.84f, 0f); // gold
        if (report.feelTired || report.feelDizzy)
            return new Color(0.58f, 0.44f, 0.86f); // medium purple
        if (report.skinItchy || report.itchyEyes || report.itchyNose)
            return new Color(1f, 0.71f, 0.76f); // light pink
        if (report.hardToSee || report.hardToHear)
            return new Color(0.44f, 0.5f, 0.56f); // slate gray
        if (report.hasPain)
            return new Color(1f, 0.42f, 0.42f); // soft red

        return new Color(0.96f, 0.91f, 0.86f);
    }
}
