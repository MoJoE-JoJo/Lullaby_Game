using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Globalization;
using System.Threading;

public class Telemetry : MonoBehaviour
{
    public struct LevelData
    {
        public TimeSpan starttime;
        public TimeSpan endtime;
        public string checkpoint;
        public string test;
    }

    private const string GoogleFormBaseUrl = "https://docs.google.com/forms/d/e/1FAIpQLSfr2e5yW7SNHtHaSDIS_nF389BAqTu2Z4MMosTJ9ZhYQ9qQog/";

    // Temeletry form variables

    private const string _form_userID = "entry.2064545113";
    private const string _form_Checkpoint = "entry.279454377";
    private const string _form_TimeStamp = "entry.1360576512";
    private const string _form_Test = "entry.1188483497";

    private static Guid runID;

    public static IEnumerator SubmitGoogleForm(LevelData data)
    {

        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;

        string urlGoogleFormResponse = GoogleFormBaseUrl + "formResponse";

        WWWForm form = new WWWForm();

        var timeTaken = data.endtime - data.starttime;

        form.AddField(_form_userID, GUIDToShortString(runID));
        form.AddField(_form_Checkpoint, data.checkpoint);
        form.AddField(_form_TimeStamp, timeTaken.ToString());
        form.AddField(_form_Test, data.test);
        //UnityWebRequest www = UnityWebRequest.Post(urlGoogleFormResponse, form);
        using (UnityWebRequest www = UnityWebRequest.Post(urlGoogleFormResponse, form))
        {
            yield return www.SendWebRequest();
            yield return null;
            Debug.Log(GUIDToShortString(runID));
            Debug.Log(data.checkpoint);
            Debug.Log(timeTaken);
            print("request sent");
        }
    }

    public static void GenerateNewRunID()
    {
        runID = Guid.NewGuid();
    }

    public static string GUIDToShortString(Guid guid)
    {
        var base64Guid = Convert.ToBase64String(guid.ToByteArray());

        // Replace URL unfriendly characters with better ones
        base64Guid = base64Guid.Replace('+', '-').Replace('/', '_');

        // Remove the trailing ==
        return base64Guid.Substring(0, base64Guid.Length - 2);
    }

}
