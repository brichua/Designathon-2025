using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

public static class PatientDataUploader
{
    public static string GeneratePatientDescription(PatientReport report, bool isMale)
    {
        List<string> lines = new List<string>();

        lines.Add($"Gender: {(isMale ? "Male" : "Female")}");

        if (report.hasPain)
        {
            List<string> painParts = new List<string>();
            if (report.painHead) painParts.Add("head");
            if (report.painArm) painParts.Add("arm");
            if (report.painLegs) painParts.Add("legs");
            if (report.painStomach) painParts.Add("stomach");
            if (report.painOther) painParts.Add("other");

            lines.Add($"Pain in: {string.Join(", ", painParts)}");

            foreach (var kvp in report.painSeverity)
                lines.Add($"Pain severity in {kvp.Key}: {kvp.Value}/5");
        }

        if (report.hasCoughed) lines.Add("Has coughed recently");
        if (report.hasVomited) lines.Add("Has vomited recently");
        if (report.skinItchy) lines.Add("Skin is itchy");
        if (report.feelHot) lines.Add("Feels hot");
        if (report.feelTired) lines.Add("Feels tired");
        if (report.feelDizzy) lines.Add("Feels dizzy");
        if (report.feelUncomfortable) lines.Add("Feels uncomfortable");
        if (report.hardToHear) lines.Add("Hard to hear");
        if (report.hardToSee) lines.Add("Hard to see");
        if (report.runnyNose) lines.Add("Runny nose");
        if (report.itchyNose) lines.Add("Itchy nose");
        if (report.runnyEyes) lines.Add("Runny eyes");
        if (report.itchyEyes) lines.Add("Itchy eyes");
        if (report.hurtBreathe) lines.Add("Pain while breathing");
        if (report.hurtWalk) lines.Add("Pain while walking");

        return string.Join("\n", lines);
    }

    public static void UploadPatientData(string patientName, PatientReport report, bool isMale)
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        var patientData = new PatientData
        {
            Name = patientName,
            Description = GeneratePatientDescription(report, isMale)
        };

        string docId = System.Guid.NewGuid().ToString();
        firestore.Collection("Patients").Document(docId).SetAsync(patientData)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("Patient data uploaded successfully!");
                else
                    Debug.LogError("Error uploading patient data: " + task.Exception);
            });
    }
}
