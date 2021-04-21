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
            //yield return www.SendWebRequest();
            yield return null;
            print("request sent");
        }
    }


    // === Player Death tracking ===
    public struct PlayerDeath
    {
        public string puzzle_name;
        public float timestamp;
    }
    private const string PlayerDeathURL = "https://docs.google.com/forms/d/e/1FAIpQLSeStbRl1fjuXZiQhGlOMtY17lj8eFINMovKrjTL3RGnrGSeeQ/";

    private const string player_death_form_run_ID = "entry.2064545113";
    private const string player_death_form_puzzle_name = "entry.279454377";
    private const string player_death_form_timestamp = "entry.1360576512";

    public static IEnumerator SubmitPlayerDeath(PlayerDeath data)
    {

        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = PlayerDeathURL + "formResponse";
        WWWForm form = new WWWForm();

        // == Build the form request ==
        form.AddField(player_death_form_run_ID, GUIDToShortString(runID));
        form.AddField(player_death_form_puzzle_name, data.puzzle_name);
        form.AddField(player_death_form_timestamp, TimeSpan.FromSeconds(data.timestamp).ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            //yield return www.SendWebRequest();
            yield return null;
            print("request sent");
        }
    }

    // === Helper Screen tracking ===
    public struct ControlScreen
    {
        public string puzzle_name;
        public float OpenedTimestamp;
        public float ClosedTimestamp;
    }

    private const string ControlScreenURL = "https://docs.google.com/forms/d/e/1FAIpQLSdsIAseEaQU1pAzNLk1dhVqR_mh03Q1ThkV33PqfKr0zlxT9w/";

    private const string control_screen_form_run_ID = "entry.2064545113";
    private const string control_screen_form_puzzle_name = "entry.279454377";
    private const string control_screen_form_open_timestamp = "entry.1360576512";
    private const string control_screen_form_close_timestamp = "entry.552596013";

    public static IEnumerator SubmitControlScreen(ControlScreen data)
    {

        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = ControlScreenURL + "formResponse";
        WWWForm form = new WWWForm();

        // == Build the form request ==
        form.AddField(control_screen_form_run_ID, GUIDToShortString(runID));
        form.AddField(control_screen_form_puzzle_name, data.puzzle_name);
        form.AddField(control_screen_form_open_timestamp, TimeSpan.FromSeconds(data.OpenedTimestamp).ToString());
        form.AddField(control_screen_form_close_timestamp, TimeSpan.FromSeconds(data.ClosedTimestamp).ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            //yield return www.SendWebRequest();
            yield return null;
            print("request sent");
        }
    }

    // === puzzle solving tracking ===
    public struct PuzzleSolvingStats
    {
        public string puzzle_name;
        public string current_correct_notes;
        public string notes_played;
        public float sing_duration;
        public float timestamp;
    }

    private const string puzzleSolvingURL = "https://docs.google.com/forms/d/e/1FAIpQLSfKeBFWuDPyugtUOs9LrbUonJBiEyWnqK66rIAl5BO2xApuTw/";

    private const string puzzle_solving_singing_form_run_ID = "entry.2064545113";
    private const string puzzle_solving_singing_form_puzzle_name = "entry.279454377";
    private const string puzzle_solving_singing_form_correct_notes = "entry.940067483";
    private const string puzzle_solving_singing_form_played_notes = "entry.954712932";
    private const string puzzle_solving_singing_form_sing_duration = "entry.238299487";
    private const string puzzle_solving_singing_form_timestamp = "entry.1741935023";

    public static IEnumerator SubmitPuzzleSolving(PuzzleSolvingStats data)
    {

        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = puzzleSolvingURL + "formResponse";
        WWWForm form = new WWWForm();

        // == Build the form request ==
        form.AddField(puzzle_solving_singing_form_run_ID, GUIDToShortString(runID));
        form.AddField(puzzle_solving_singing_form_puzzle_name, data.puzzle_name);
        form.AddField(puzzle_solving_singing_form_correct_notes, data.current_correct_notes);
        form.AddField(puzzle_solving_singing_form_played_notes, data.notes_played);
        form.AddField(puzzle_solving_singing_form_sing_duration, TimeSpan.FromSeconds(data.sing_duration).ToString());
        form.AddField(puzzle_solving_singing_form_timestamp, TimeSpan.FromSeconds(data.timestamp).ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            yield return www.SendWebRequest();
            yield return null;
            print("request sent");
        }
    }


    // === Lock puzzle tracking ===
    public struct LockPuzzleStat
    {
        public int state;
        public bool successful;
        public float timestamp;
    }

    private const string LockPuzzleURL = "https://docs.google.com/forms/d/e/1FAIpQLSfthiDtAJ4xKK68eT-VFf_uMsY_LtxGyr88EHLKvjo1w0zSlA/";

    private const string lock_puzzle_form_run_ID = "entry.2064545113";
    private const string lock_puzzle_form_state = "entry.220580347";
    private const string lock_puzzle_form_successful = "entry.279454377";
    private const string lock_puzzle_form_timestamp = "entry.1360576512";

    public static IEnumerator SubmitLockPuzzleStat(LockPuzzleStat data)
    {

        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = LockPuzzleURL + "formResponse";
        WWWForm form = new WWWForm();

        // == Build the form request ==
        form.AddField(lock_puzzle_form_run_ID, GUIDToShortString(runID));
        form.AddField(lock_puzzle_form_state, data.state.ToString());
        form.AddField(lock_puzzle_form_successful, data.successful.ToString());
        form.AddField(lock_puzzle_form_timestamp, TimeSpan.FromSeconds(data.timestamp).ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            //yield return www.SendWebRequest();
            yield return null;
            print("request sent");
        }
    }



    // === Overall run tracking ===
    public struct OverallStats
    {
        public int total_deaths;
        public float time_to_beat;
        public float total_time_singing_doing_puzzle;
        public float total_time_singing_outside_puzzle;
        public float note_activation_in_puzzle;
        public float note_activation_outside_puzzle;
    }

    private const string OverallURL = "https://docs.google.com/forms/d/e/1FAIpQLSf31-p09ta5N4Qft1EigWk3de0CP_Cp_HKTXaoLmblW5ZgJNw/";

    private const string overall_form_run_ID = "entry.2064545113";
    private const string overall_form_total_deaths = "entry.279454377";
    private const string overall_form_time_to_beat = "entry.1360576512";
    private const string overall_form_total_time_singing_doing_puzzle = "entry.940067483";
    private const string overall_form_total_time_singing_outside_puzzle = "entry.954712932";
    private const string overall_form_note_activation_in_puzzle = "entry.238299487";
    private const string overall_form_note_activation_outside_puzzle = "entry.1741935023";

    public static IEnumerator SubmitOverallData(OverallStats data)
    {

        // == config request ==
        CultureInfo ci = CultureInfo.GetCultureInfo("en-GB");
        Thread.CurrentThread.CurrentCulture = ci;
        string fullURL = OverallURL + "formResponse";
        WWWForm form = new WWWForm();

        // == Build the form request ==
        form.AddField(overall_form_run_ID, GUIDToShortString(runID));
        form.AddField(overall_form_total_deaths, data.total_deaths.ToString());
        form.AddField(overall_form_time_to_beat, TimeSpan.FromSeconds(data.time_to_beat).ToString());
        form.AddField(overall_form_total_time_singing_doing_puzzle, TimeSpan.FromSeconds(data.total_time_singing_doing_puzzle).ToString());
        form.AddField(overall_form_total_time_singing_outside_puzzle, TimeSpan.FromSeconds(data.total_time_singing_outside_puzzle).ToString());
        form.AddField(overall_form_note_activation_in_puzzle, data.note_activation_in_puzzle.ToString());
        form.AddField(overall_form_note_activation_outside_puzzle, data.note_activation_outside_puzzle.ToString());

        // == Send the request ==
        using (UnityWebRequest www = UnityWebRequest.Post(fullURL, form))
        {
            //yield return www.SendWebRequest();
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
