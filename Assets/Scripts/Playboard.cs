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
        AwaitResponse,
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

    Kalimat.Vocab.Stack PairStack;
    int PairCurrent;            // Index of the current word pair
    List<int> PairsPending;     // List of all word pair indices that need answering
    Directions PairAnswer;      // Direction to swipe to match the pair correctly

    

    void Start () {

        // Hack: Set this TestStack as the running word stack...
        PairStack = new Kalimat.Vocab.Test_Spanish();

        PairsPending = new List<int>();
        for (int i = 0; i < PairStack.WordPairs.Count; i++)
            PairsPending.Add(i);

        GameState = GameStates.DisplayPair;     // Start the stack!
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
            GameState = GameStates.DisplayPair;

        switch (GameState)
        {
            case GameStates.DisplayPair:
                if (PairsPending.Count == 0)
                {
                    GameState = GameStates.StackComplete;
                    break;
                }

                PairCurrent = PairsPending[UnityEngine.Random.Range(0, PairsPending.Count)];
                PairsPending.Remove(PairCurrent);         // EXCEPTION: OUT OF RANGE
                PairAnswer = (Directions)UnityEngine.Random.Range(0, 3);

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

                GameState = GameStates.AwaitResponse;

                break;


            case GameStates.AwaitResponse:
                Debug.Log(String.Format("Awaiting response- answer is {0}", PairAnswer.ToString()));
                break;

            case GameStates.PostAnswer:
                Debug.Log("Posting answer-");
                break;

            case GameStates.StackComplete:
                Debug.Log("Stack complete-");
                break;

            case GameStates.Null:
            default:
                break;
        }
	}
}
