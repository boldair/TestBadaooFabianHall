using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    public Card cardPf;
    [SerializeField] private TextMeshProUGUI victoryText;
    public Card[,] cards = new Card[3,3];
    public Card[,] results = new Card[3,3];
    public int[] result = new int[9];

    public Sprite[] sprites;

    private Vector2 lastMove;


    // Start is called before the first frame update
    void Start()
    {
        Init();

        //let's shuffle a lot 
        //for (int i = 0; i < 100; i++)
        //{
        Shuffle();

        //}


    }
    void Init()
    {
        
        //create a 3x3 array and instantiate the card on it
        int index = 0;
        for(int y = 2 ; y>=0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                Card card = Instantiate(cardPf, new Vector2(x,y), Quaternion.identity);

                card.Init(x, y, index, sprites[index], ClickToSwap);
                cards[x, y] = card;
                results[x, y] = card;

                index++;
            }
        }
    }
    void ClickToSwap(int x, int y)
    {
        int dx = GetDx(x, y);
        int dy = GetDy(x, y);
        Swap(x, y , dx, dy);

        if (CheckVictory())
        {
            victoryText.gameObject.SetActive(true);
            StartCoroutine(goToMenuCR());
        }
    }

    IEnumerator goToMenuCR()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Home");
    }
    private bool CheckVictory()
    {
        
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (cards[i, j] != results[i, j])
                {
                    return false;
                }        
            }
        }
        
        return true;
    }

    void Swap(int x, int y, int dx, int dy)
    {

        Card from = cards[x, y];
        Card to = cards[x+dx, y+dy];

        //swap those 2 cards
        cards[x,y] = to;
        cards[x+dx, y+dy] = from;

        from.UpdatePos(x+dx, y+dy);
        to.UpdatePos(x, y);
    }

    int GetDx(int x, int y)
    {
        //right empty
        if(x < 2 && cards[x+1,y].IsEmpty())
        {
            return 1;
        }
        //left empty
        if (x > 0 && cards[x - 1, y].IsEmpty())
        {
            return -1;
        }
        return 0;
    }

    int GetDy(int x, int y)
    {
        //is top empty
        if (y < 2 && cards[x, y + 1].IsEmpty())
        {
            return 1;
        }
        //is bottom empty
        if (y > 0 && cards[x, y - 1].IsEmpty())
        {
            return -1;
        }
        return 0;
    }
    void Shuffle()
    {
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                if (cards[i, j].IsEmpty())
                {
                    Vector2 pos = getValidMove(i, j);
                    Swap(i, j, (int)pos.x, (int)pos.y);
                }
            }
        }
         
        
    }
    Vector2 getValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();

        //for the 4 possible movement : right, left, top, bot
        do
        {
            int n = Random.Range(0, 4);

            if (n == 0)
            {
                pos = Vector2.left;
            }
            else if(n == 1)
            {
                pos = Vector2.right;
            }
            else if(n == 2)
            {
                pos = Vector2.up;
            }
            else
            {
                pos = Vector2.down;
            }
            
        } while(!(isValidRange(x + (int) pos.x) && isValidRange(y + (int)pos.y)) || isRepeatMove(pos) );

        lastMove = pos;
        return pos;
    }
    bool isValidRange(int n)
    {
        return n >= 0 && n <= 2;
    }

    //check if we don't make the same move while shuffling
    bool isRepeatMove(Vector2 pos)
    {
        return pos * -1 == lastMove;
    }
}
