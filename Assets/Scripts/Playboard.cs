using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class Playboard : MonoBehaviour {

    enum GameStates
    {
        Null,
        DisplayPair,
        AwaitingResponse,
        PostAnswer,
        StackComplete
    }

    enum Directions
    {
        Up,
        Left,
        Right,
        Down
    }

    public Text WordTarget,
        WordUp,
        WordLeft,
        WordRight,
        WordDown;

    GameStates GameState;
    Kalimat.Player Player;

    Kalimat.Vocab.Stack PairStack;
    int PairCurrent;            // Index of the current word pair
    List<int> PairsPending;     // List of all word pair indices that need answering
    Directions PairAnswer;      // Direction to swipe to match the pair correctly
    float PairTime;             // The time the word pair is posted
    bool AnswerCorrect;         // Whether the player's answer is correct?

    

    void Start ()
    {
        Player = Kalimat.Serialize.Load();

        // Hack: Set this TestStack as the running word stack...
        PairStack = new Kalimat.Vocab.Test_Spanish();

        PairsPending = new List<int>();
        for (int i = 0; i < PairStack.WordPairs.Count; i++)
            PairsPending.Add(i);

        GameState = GameStates.DisplayPair;     // Start the stack!
	}

    void Finish ()
    {
        Kalimat.Serialize.Save(Player);
        GameState = GameStates.Null;
        Application.Quit();
    }
	
	void Update ()
    {
        switch (GameState)
        {
            case GameStates.DisplayPair: DisplayPair(); break;
            case GameStates.AwaitingResponse: ResponseProcess(); break;
            case GameStates.PostAnswer: PostAnswer(); break;
            case GameStates.StackComplete: StackComplete(); break;

            case GameStates.Null:
            default:
                break;
        }
	}

    void DisplayPair()
    {
        if (PairsPending.Count == 0)
        {
            GameState = GameStates.StackComplete;
            return;
        }

        PairCurrent = PairsPending[UnityEngine.Random.Range(0, PairsPending.Count)];
        PairsPending.Remove(PairCurrent);
        PairAnswer = (Directions)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Directions)).Length);
        PairTime = Time.time;
        WordTarget.text = PairStack.WordPairs[PairCurrent][Kalimat.Vocab.WordPair.Target.GetHashCode()];

        List<string[]> unusedPairs = new List<string[]>(PairStack.WordPairs);
        unusedPairs.RemoveAt(PairCurrent);
        int unusedIndex;

        if (PairAnswer == Directions.Up)    // Is this WordDirection the intended answer?
            WordUp.text = PairStack.WordPairs[PairCurrent][Kalimat.Vocab.WordPair.Source.GetHashCode()];    // If yes, display the PairCurrent word
        else
        {   // Or else display a random pair word from a list of unused words... then remove the used word from the unused pile.
            unusedIndex = UnityEngine.Random.Range(0, unusedPairs.Count);
            WordUp.text = unusedPairs[unusedIndex][Kalimat.Vocab.WordPair.Source.GetHashCode()];
            unusedPairs.RemoveAt(unusedIndex);
        }

        if (PairAnswer == Directions.Left)
            WordLeft.text = PairStack.WordPairs[PairCurrent][Kalimat.Vocab.WordPair.Source.GetHashCode()];
        else
        {
            unusedIndex = UnityEngine.Random.Range(0, unusedPairs.Count);
            WordLeft.text = unusedPairs[unusedIndex][Kalimat.Vocab.WordPair.Source.GetHashCode()];
            unusedPairs.RemoveAt(unusedIndex);
        }

        if (PairAnswer == Directions.Right)
            WordRight.text = PairStack.WordPairs[PairCurrent][Kalimat.Vocab.WordPair.Source.GetHashCode()];
        else
        {
            unusedIndex = UnityEngine.Random.Range(0, unusedPairs.Count);
            WordRight.text = unusedPairs[unusedIndex][Kalimat.Vocab.WordPair.Source.GetHashCode()];
            unusedPairs.RemoveAt(unusedIndex);
        }

        if (PairAnswer == Directions.Down)
            WordDown.text = PairStack.WordPairs[PairCurrent][Kalimat.Vocab.WordPair.Source.GetHashCode()];
        else
        {
            unusedIndex = UnityEngine.Random.Range(0, unusedPairs.Count);
            WordDown.text = unusedPairs[unusedIndex][Kalimat.Vocab.WordPair.Source.GetHashCode()];
            unusedPairs.RemoveAt(unusedIndex);
        }

        GameState = GameStates.AwaitingResponse;
    }

    void ResponseProcess()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AnswerCorrect = (PairAnswer == Directions.Up);
            GameState = GameStates.PostAnswer;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnswerCorrect = (PairAnswer == Directions.Left);
            GameState = GameStates.PostAnswer;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AnswerCorrect = (PairAnswer == Directions.Right);
            GameState = GameStates.PostAnswer;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AnswerCorrect = (PairAnswer == Directions.Down);
            GameState = GameStates.PostAnswer;
        }
    }

    void PostAnswer()
    {

        float PointsEarned = 0f;
        float AnswerTime = Time.time - PairTime;

        if (AnswerCorrect)
        {
            PointsEarned = (4 - (AnswerTime / 2)) > 1 ? (4 - (AnswerTime / 2)) : 1;
            PointsEarned *= (PairStack.Difficulty.GetHashCode() + 1);
        }
        else
            PointsEarned = -1;

        Player.Points += (int)PointsEarned;


        Debug.Log(String.Format("You answered in {0} seconds; points earned {1}", AnswerTime, PointsEarned));
        Debug.Log(String.Format("You answered {0} - Total Points: {1}", AnswerCorrect ? "right!" : "wrong...", Player.Points));
        GameState = GameStates.DisplayPair;
    }

    void StackComplete()
    {
        Debug.Log("Stack complete-");
        Finish();
    }
}
