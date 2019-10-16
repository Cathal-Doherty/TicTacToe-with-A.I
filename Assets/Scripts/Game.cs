using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int Turn; // 0 = O and 1 = X
    public int Counter; // This count's the number of tunrs that have been played.
    public int[] MarkerGridSpaces; // This shows which grid location is marked by which player.
    public GameObject[] TurnI; //Displays the players turn.
    public Sprite[] PlayerI; // 0 = O icon and 1 = X.
    public Button[] GridSpaces; // Playable Spaces.
    public Text WinnerTXT; //This holds the winning text component.
    public GameObject[] LineWinner; //Stores the X and O winning line and shows the correct one depending on who wins.
    public GameObject WinnerPopUp; // this enables the winner popup to display when one of the players have one and puts a canvas over the game to stop people from playing.

    // Start is called before the first frame update
    void Start()
    {
        Setup();

    }

    void Setup()
    {
        Turn = 0; // setting the turn to let 0 to go first, 0 = O and X = 1
        Counter = 0; // setting the score counter to start off as 0 for both X and O.
        TurnI[0].SetActive(true); // setting player O to go first
        TurnI[1].SetActive(false); // setting 1 as unactive and goes next.

        for (int i = 0; i < GridSpaces.Length; i++)
        {

            GridSpaces[i].interactable = true; // sets the grid space to open/clickable.
            GridSpaces[i].GetComponent<Image>().sprite = null; // once clicked gets the sprite icon.
        }
        for(int i = 0; i < MarkerGridSpaces.Length; i++)
        {
            MarkerGridSpaces[i] = -100; // set to -1 because there is 9 elements in the array. once selected in the grid the grid value will be assigned the value of either 0 or 1 depending on whos go it was.

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void button(int Numbersel)
    {
        // this sets the drif loacation to the slected players icon X or O and then closes the space so it cannot be clicked on again.
        GridSpaces[Numbersel].image.sprite = PlayerI[Turn];
        GridSpaces[Numbersel].interactable = false;

        MarkerGridSpaces[Numbersel] = Turn +1; // this identifies which grid space has been marked by which player. The plus adds the an extra value to either 0 or 1 making 1 = O and 2 = X.
        Counter++;

        if (Counter > 4) // set it to 4 because no one would be able to win after having placed two counters. however turn 5 someone could get a row, line or diagonal.
        {
            Winner();

        }

        // changes the players turn. if it's = to 0 then goes to players 1 turn and back again till the game is finished.
        if (Turn == 0)
        {

            Turn = 1;
            TurnI[0].SetActive(false);
            TurnI[1].SetActive(true);

        }
        else
        {

            Turn = 0;
            TurnI[0].SetActive(true);
            TurnI[1].SetActive(false);
        }

    }


    void DisplayWin(int indexin)
    {
        WinnerPopUp.gameObject.SetActive(true); // sets the winner text to active.

        // this determines whos turn it is to work out which winner text to display.
        if (Turn == 0)
        {
            WinnerTXT.text = "Player O Wins!";
        }
        else if (Turn == 1)
        {
            WinnerTXT.text = "Player X Wins!";
        }


        LineWinner[indexin].SetActive(true); // this turns on the correct line to display who and where the player has won.



    }

    void Winner()
    {
        // here is all the possible winning outcomes. stored in temp ints.
        int L1 = MarkerGridSpaces[0] + MarkerGridSpaces[1] + MarkerGridSpaces[2]; 
        int L2 = MarkerGridSpaces[3] + MarkerGridSpaces[4] + MarkerGridSpaces[5];
        int L3 = MarkerGridSpaces[6] + MarkerGridSpaces[7] + MarkerGridSpaces[8];
        int L5 = MarkerGridSpaces[2] + MarkerGridSpaces[5] + MarkerGridSpaces[8];
        int L6 = MarkerGridSpaces[1] + MarkerGridSpaces[4] + MarkerGridSpaces[7];
        int L4 = MarkerGridSpaces[0] + MarkerGridSpaces[3] + MarkerGridSpaces[6];
        int L7 = MarkerGridSpaces[0] + MarkerGridSpaces[4] + MarkerGridSpaces[8];
        int L8 = MarkerGridSpaces[2] + MarkerGridSpaces[4] + MarkerGridSpaces[6];

        var Lines = new int[] { L1, L2, L3, L4, L5, L6, L7, L8 }; //this is taking the variables above and saving them into Lines.

        for (int i = 0; i < Lines.Length; i++)
        {
            if (Lines[i] == 3 * (Turn+1)) // This allow us to see who has won the game, if O get a line or a diagonal then it will be 0 +1 which equals 1 *3 = 3 this means in L1 to L8 if somewhere it adds up to 3 the O has won. If it is 2 + 2 + 2 then X has won.
            {
                DisplayWin(i);
                return;

            }

        }
    }

}
