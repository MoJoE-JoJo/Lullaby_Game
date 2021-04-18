using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Globalization;
using System.Threading;

public class Telemetry : MonoBehaviour
{
    //used for all surveys
    private static Guid runID;

    // === Player movement tracking ===
    public struct PlayerMovement
    {
        public string checkpoint;
        public TimeSpan timestamp;
    }
    private const string PlayerMovementURL= "https://docs.google.com/forms/d/e/1FAIpQLSfr2e5yW7SNHtHaSDIS_nF389BAqTu2Z4MMosTJ9ZhYQ9qQog/";

    private const string player_movement_form_run_ID = "entry.2064545113";
    private const string player_movement_form_checkpoint = "entry.279454377";
    private const string player_movement_form_timestamp = "entry.1360576512";

    public static IEnumerator SubmitPlayerMovement(PlayerMovement data)
    {
        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = PlayerMovementURL + "formResponse";
        WWWForm form = new WWWForm();

        // == preprocess values ==
        //var poop = 1+2;

        // == Build the form request ==
        form.AddField(player_movement_form_run_ID, GUIDToShortString(runID));
        form.AddField(player_movement_form_checkpoint, data.checkpoint);
        form.AddField(player_movement_form_timestamp, data.timestamp.ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            //yield return www.SendWebRequest(); //TODO
            yield return null;
            print("request sent");
        }
    }


    // === puzzle_completion tracking ===
    public struct PuzzleCompletion
    {
        public string puzzle_name;

        public float startTime;
        public float endTime;

        public float time_singing;
        public float wagon_movement;
        public int note_lock_count;
        public int note_unlock_count;
    }
    private const string puzzle_completion_URL = "https://docs.google.com/forms/d/e/1FAIpQLSdfjEDzXpGSscKO8EtFXk9er3OxBLcHTU2BwB9hHDcj5Yap8A/";

    private const string puzzle_completion_form_run_ID = "entry.2064545113";
    private const string puzzle_completion_form_puzzle_name = "entry.279454377";
    private const string puzzle_completion_form_time_taken_to_beat = "entry.1360576512";
    private const string puzzle_completion_form_total_time_singing = "entry.940067483";
    private const string puzzle_completion_form_total_wagon_negative_movement = "entry.954712932";
    private const string puzzle_completion_form_node_lock_count = "entry.238299487";
    private const string puzzle_completion_form_total_node_unlock_count = "entry.1741935023";

    public static IEnumerator SubmitPuzzleCompletion(PuzzleCompletion data)
    {
        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = puzzle_completion_URL + "formResponse";
        WWWForm form = new WWWForm();

        // == preprocess values ==
        var time_taken = TimeSpan.FromSeconds(data.endTime - data.startTime);

        // == Build the form request ==
        form.AddField(puzzle_completion_form_run_ID, GUIDToShortString(runID));
        form.AddField(puzzle_completion_form_puzzle_name, data.puzzle_name);
        form.AddField(puzzle_completion_form_time_taken_to_beat, time_taken.ToString());

        form.AddField(puzzle_completion_form_total_time_singing, data.time_singing.ToString());
        form.AddField(puzzle_completion_form_total_wagon_negative_movement, data.wagon_movement.ToString());

        form.AddField(puzzle_completion_form_node_lock_count, data.note_lock_count.ToString());
        form.AddField(puzzle_completion_form_total_node_unlock_count, data.note_unlock_count.ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            yield return www.SendWebRequest();
            yield return null;
            print("request sent");
        }
    }



    // === Used by all ===

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
