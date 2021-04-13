using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScreenCSS : MonoBehaviour
{
    MainManager mainMngr;

    public Transform rootChars;

    public List<CharacterPickSO> characters = new List<CharacterPickSO>();
    public GameObject charCellPrefab;

    public List<GameObject> cells = new List<GameObject>();

    int indexP1;
    int indexP2;

    bool selectP1;
    bool selectP2;
    bool startP1;
    bool startP2;

    public Color colorStart;
    public Color colorP1;
    public Color colorP2;

    public GameObject panelP1;
    public GameObject panelP2;

    public GameObject startBtn;
    public Sprite[] normalStart;
    public Sprite[] selectStart;
    public Sprite[] playerStart;

    float startSize = 130f;

    public Sprite[] frontBorder;
    public Sprite[] selectBorder;
    public Sprite[] playerBorder;

    IEnumerator randomRoutine1;
    IEnumerator randomRoutine2;



    // Start is called before the first frame update
    void Start()
    {
        mainMngr = FindObjectOfType<MainManager>();
        foreach(CharacterPickSO character in characters)
        {
            SpawnCharacterCell(character);
            UpdateCells();
            SelectStart();
        }
    }

    private void SpawnCharacterCell(CharacterPickSO character)
    {
        GameObject charCell = Instantiate(charCellPrefab, rootChars);

        charCell.name = character.characterSiglas;

        Transform maskObj = charCell.transform;

        Image portrait = maskObj.Find("Retrato Mask").Find("Retrato").GetComponentInChildren<Image>();


        portrait.sprite = character.characterSprite;
        cells.Add(charCell);
    }

    private void Update()
    {
        if (!selectP1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (indexP1 == 0)
                {
                    indexP1 = cells.Count - 1;
                }
                else
                {
                    indexP1--;
                }
                UpdateCells();
                SelectPlayer(1, false);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (indexP1 == cells.Count - 1)
                {
                    indexP1 = 0;
                }
                else
                {
                    indexP1++;
                }
                UpdateCells();
                SelectPlayer(1, false);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                SelectPlayer(1, true);
                selectP1 = true;
                SelectStart();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                UpdateCells();
                SelectPlayer(1, false);
                startP1 = false;
                selectP1 = false;
                SelectStart();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                startP1 = true;
                CheckStart();
            }
        }

        if (!selectP2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (indexP2 == 0)
                {
                    indexP2 = cells.Count - 1;
                }
                else
                {
                    indexP2--;
                }
                UpdateCells();
                SelectPlayer(2, false);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (indexP2 == cells.Count - 1)
                {
                    indexP2 = 0;
                }
                else
                {
                    indexP2++;
                }
                UpdateCells();
                SelectPlayer(2, false);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.RightControl))
            {
                SelectPlayer(2, true);
                selectP2 = true;
                SelectStart();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.RightShift))
            {
                UpdateCells();
                SelectPlayer(2, false);
                startP2 = false;
                selectP2 = false;
                SelectStart();
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.RightControl))
            {
                startP2 = true;
                CheckStart();
            }
        }
    }

    public void SelectStart()
    {
        if (selectP1 && !selectP2)
        {
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().sprite = playerStart[0];
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().color = colorP1;
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().sprite = selectStart[1];
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().color = colorP1;
        }
        else if (!selectP1 && selectP2)
        {
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().sprite = selectStart[0];
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().color = colorP2;
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().sprite = playerStart[1];
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().color = colorP2;
        }
        else if (selectP1 && selectP2)
        {
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().sprite = playerStart[0];
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().color = colorP1;
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().sprite = playerStart[1];
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().color = colorP2;
        }
        else if(!selectP1 && !selectP2)
        {
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().sprite = normalStart[0];
            startBtn.transform.Find("StartSelectP1").GetComponent<Image>().color = colorStart;
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().sprite = normalStart[1];
            startBtn.transform.Find("StartSelectP2").GetComponent<Image>().color = colorStart;
        }
    }

    public void CheckStart()
    {
        SelectStart();
        if (startP1 && startP2)
        {
            mainMngr.charP1 = indexP1;
            mainMngr.charP2 = indexP2;
            mainMngr.StartGame();
        }
    }

    public void SelectPlayer(int player,bool select)
    {
        if (select)
        {
            if (player == 1)
            {
                if (!panelP1.activeSelf) { panelP1.SetActive(true); }
                if (cells[indexP1].name != "Rnd")
                {
                    panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP1].characterSprite;
                    panelP1.transform.Find("Foto").GetComponent<Image>().color = Color.white;
                    panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[indexP1].characterName;
                    SelectStart();
                }
                else
                { 
                    if(randomRoutine1 != null)
                    {
                        StopCoroutine(randomRoutine1);
                        randomRoutine1 = null;
                    }
                    randomRoutine1 = RandomCharacter(0.1f,1);
                    StartCoroutine(randomRoutine1);
                }
            }
            else if (player == 2)
            {
                if (!panelP2.activeSelf) { panelP2.SetActive(true); }
                if (cells[indexP2].name != "Rnd")
                {
                    panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP2].characterSprite;
                    panelP2.transform.Find("Foto").GetComponent<Image>().color = Color.white;
                    panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[indexP2].characterName;
                    SelectStart();
                }
                else
                {
                    if (randomRoutine2 != null)
                    {
                        StopCoroutine(randomRoutine2);
                        randomRoutine2 = null;
                    }
                    randomRoutine2 = RandomCharacter(0.1f, 2);
                    StartCoroutine(randomRoutine2);
                }
            }
        }
        else
        {
            if (player == 1)
            {
                if (!panelP1.activeSelf) { panelP1.SetActive(true); }
                panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP1].siluetaSprite;
                panelP1.transform.Find("Foto").GetComponent<Image>().color = colorP1;
                panelP1.transform.Find("Nombre").GetComponent<Text>().text = "???";
                SelectStart();
            }
            else if (player == 2)
            {
                if (!panelP2.activeSelf) { panelP2.SetActive(true); }
                panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP2].siluetaSprite;
                panelP2.transform.Find("Foto").GetComponent<Image>().color = colorP2;
                panelP2.transform.Find("Nombre").GetComponent<Text>().text = "???";
                SelectStart();
            }
        }
    }

    private IEnumerator RandomCharacter(float waitTime, int player)
    {
        for (int i = 0; i < characters.Count -1; i++)
        {
            if(player == 1)
            {
                panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[i].siluetaSprite;
                panelP1.transform.Find("Foto").GetComponent<Image>().color = colorP1;
                panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[i].characterName;
            }
            else if(player == 2)
            {
                panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[i].siluetaSprite;
                panelP2.transform.Find("Foto").GetComponent<Image>().color = colorP2;
                panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[i].characterName;
            }
            yield return new WaitForSeconds(waitTime);
        }
        SelectRandomChar(player);
        yield break;
    }

    public void SelectRandomChar(int player)
    {
        int rndChar = Random.Range(0, characters.Count);
        if (cells[rndChar].name != "Rnd")
        {
            if (player == 1)
            {
                indexP1 = rndChar;
                panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[rndChar].characterSprite;
                panelP1.transform.Find("Foto").GetComponent<Image>().color = Color.white;
                panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[rndChar].characterName;
                SelectStart();
                UpdateCells();
            }
            else if (player == 2)
            {
                indexP2 = rndChar;
                panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[rndChar].characterSprite;
                panelP2.transform.Find("Foto").GetComponent<Image>().color = Color.white;
                panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[rndChar].characterName;
                SelectStart();
                UpdateCells();
            }
        }
        else
        {
            SelectRandomChar(player);
        }
    }

    public void UpdateCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {

            GameObject borderP1_F = cells[i].transform.Find("Border_F_L").gameObject;
            GameObject borderP1_B = cells[i].transform.Find("Border_B_L").gameObject;
            GameObject borderP2_F = cells[i].transform.Find("Border_F_R").gameObject;
            GameObject borderP2_B = cells[i].transform.Find("Border_B_R").gameObject;

            if (i == indexP1 && indexP1 == indexP2)
            {
                borderP1_F.gameObject.GetComponent<Image>().color = colorP1;
                borderP1_B.gameObject.GetComponent<Image>().sprite = playerBorder[0];
                borderP1_B.gameObject.GetComponent<Image>().color = colorP1;
                borderP2_F.gameObject.GetComponent<Image>().color = colorP2;
                borderP2_B.gameObject.GetComponent<Image>().sprite = playerBorder[1];
                borderP2_B.gameObject.GetComponent<Image>().color = colorP2;
            }
            else if (i == indexP1 && indexP1 != indexP2)
            {
                borderP1_F.gameObject.GetComponent<Image>().color = colorP1;
                borderP1_B.gameObject.GetComponent<Image>().sprite = playerBorder[0];
                borderP1_B.gameObject.GetComponent<Image>().color = colorP1;
                borderP2_F.gameObject.GetComponent<Image>().color = colorP1;
                borderP2_B.gameObject.GetComponent<Image>().sprite = selectBorder[1];
                borderP2_B.gameObject.GetComponent<Image>().color = colorP1;
            }
            else if (i == indexP2 && indexP1 != indexP2)
            {                                                             
                borderP1_F.gameObject.GetComponent<Image>().color = colorP2;
                borderP1_B.gameObject.GetComponent<Image>().sprite = selectBorder[0];
                borderP1_B.gameObject.GetComponent<Image>().color = colorP2;
                borderP2_F.gameObject.GetComponent<Image>().color = colorP2;
                borderP2_B.gameObject.GetComponent<Image>().sprite = playerBorder[1];
                borderP2_B.gameObject.GetComponent<Image>().color = colorP2;
            }
            else
            {
                borderP1_F.gameObject.GetComponent<Image>().color = colorStart;
                borderP1_B.gameObject.GetComponent<Image>().sprite = frontBorder[0];
                borderP1_B.gameObject.GetComponent<Image>().color = colorStart;
                borderP2_F.gameObject.GetComponent<Image>().color = colorStart;
                borderP2_B.gameObject.GetComponent<Image>().sprite = frontBorder[1];
                borderP2_B.gameObject.GetComponent<Image>().color = colorStart;
            }
        }
    }
}
