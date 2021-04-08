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

    public Color colorP1;
    public Color colorP2;

    public GameObject panelP1;
    public GameObject panelP2;

    float startSize = 130f;

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
            panelP1.transform.Find("StartSelectP1").gameObject.SetActive(false);
            panelP1.transform.Find("StartSelectP1").Find("Check").gameObject.SetActive(false);
            panelP2.transform.Find("StartSelectP2").gameObject.SetActive(false);
            panelP2.transform.Find("StartSelectP2").Find("Check").gameObject.SetActive(false);
        }
    }

    private void SpawnCharacterCell(CharacterPickSO character)
    {
        GameObject charCell = Instantiate(charCellPrefab, rootChars);

        charCell.name = character.characterSiglas;

        Transform maskObj = charCell.transform.Find("Mask");

        Text name = maskObj.Find("Nombre").GetComponent<Text>();
        Image portrait = maskObj.Find("RetratoMask").GetComponentInChildren<Image>();

        charCell.transform.Find("Border1").gameObject.SetActive(false);
        charCell.transform.Find("Border2").gameObject.SetActive(false);
        charCell.transform.Find("Check1").gameObject.SetActive(false);
        charCell.transform.Find("Check2").gameObject.SetActive(false);


        portrait.sprite = character.characterSprite;
        name.text = character.characterName;
        cells.Add(charCell);
    }

    private void Update()
    {
        if (!selectP1)
        {
            if (Input.GetKeyDown(KeyCode.W))
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
            if (Input.GetKeyDown(KeyCode.S))
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
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                startP1 = true;
                CheckStart();
            }
        }

        if (!selectP2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
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
            if (Input.GetKeyDown(KeyCode.DownArrow))
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
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                SelectPlayer(2, true);
                selectP2 = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                UpdateCells();
                SelectPlayer(2, false);
                startP2 = false;
                selectP2 = false;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                startP2 = true;
                CheckStart();
            }
        }
    }

    public void CheckStart()
    {
        panelP1.transform.Find("StartSelectP1").Find("Check").gameObject.SetActive(startP1);
        panelP2.transform.Find("StartSelectP2").Find("Check").gameObject.SetActive(startP2);
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
                if (indexP1 < characters.Count -1)
                {
                    panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP1].characterSprite;
                    panelP1.transform.Find("Foto").GetComponent<Image>().color = Color.white;
                    panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[indexP1].characterName;
                    cells[indexP1].transform.Find("Check1").gameObject.SetActive(true);
                    panelP1.transform.Find("StartSelectP1").gameObject.SetActive(true);
                    panelP1.transform.Find("StartSelectP1").Find("Check").gameObject.SetActive(false);
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
                if (indexP2 < characters.Count -1 )
                {
                    panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP2].characterSprite;
                    panelP2.transform.Find("Foto").GetComponent<Image>().color = Color.white;
                    panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[indexP2].characterName;
                    cells[indexP2].transform.Find("Check2").gameObject.SetActive(true);
                    panelP2.transform.Find("StartSelectP2").gameObject.SetActive(true);
                    panelP2.transform.Find("StartSelectP2").Find("Check").gameObject.SetActive(false);
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
                cells[indexP1].transform.Find("Check1").gameObject.SetActive(false);
                panelP1.transform.Find("StartSelectP1").gameObject.SetActive(false);
                panelP1.transform.Find("StartSelectP1").Find("Check").gameObject.SetActive(false);
            }
            else if (player == 2)
            {
                if (!panelP2.activeSelf) { panelP2.SetActive(true); }
                panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP2].siluetaSprite;
                panelP2.transform.Find("Foto").GetComponent<Image>().color = colorP2;
                panelP2.transform.Find("Nombre").GetComponent<Text>().text = "???";
                cells[indexP2].transform.Find("Check2").gameObject.SetActive(false);
                panelP2.transform.Find("StartSelectP2").gameObject.SetActive(false);
                panelP2.transform.Find("StartSelectP2").Find("Check").gameObject.SetActive(false);
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
        int rndChar = Random.Range(0, characters.Count - 1);
        if (player == 1)
        {
            indexP1 = rndChar;
            panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[rndChar].characterSprite;
            panelP1.transform.Find("Foto").GetComponent<Image>().color = Color.white;
            panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[rndChar].characterName;
            cells[indexP1].transform.Find("Check1").gameObject.SetActive(true);
            panelP1.transform.Find("StartSelectP1").gameObject.SetActive(true);
            panelP1.transform.Find("StartSelectP1").Find("Check").gameObject.SetActive(false);
            UpdateCells();
            StopCoroutine(randomRoutine1);
        }
        else if (player == 2)
        {
            indexP2 = rndChar;
            panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[rndChar].characterSprite;
            panelP2.transform.Find("Foto").GetComponent<Image>().color = Color.white;
            panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[rndChar].characterName;
            cells[indexP2].transform.Find("Check2").gameObject.SetActive(true);
            panelP2.transform.Find("StartSelectP2").gameObject.SetActive(true);
            panelP2.transform.Find("StartSelectP2").Find("Check").gameObject.SetActive(false);
            UpdateCells();
            StopCoroutine(randomRoutine2);
        }

    }

    public void UpdateCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            Transform border1 = cells[i].transform.Find("Border1");
            Transform border2 = cells[i].transform.Find("Border2");
            GameObject mark1 = cells[i].transform.Find("Mark1").gameObject;
            GameObject mark2 = cells[i].transform.Find("Mark2").gameObject;
            startSize = border1.GetComponent<RectTransform>().sizeDelta.x;
            if (i == indexP1 && indexP1 == indexP2)
            {
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(true);
                border1.gameObject.GetComponent<Image>().color = colorP1;
                border2.gameObject.GetComponent<Image>().color = colorP2;
                mark1.SetActive(true);
                mark2.SetActive(true);
            }
            else if (i == indexP1 && indexP1 != indexP2)
            {
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(true);
                border1.gameObject.GetComponent<Image>().color = colorP1;
                border2.gameObject.GetComponent<Image>().color = colorP1;
                mark1.SetActive(true);
                mark2.SetActive(false);
            }
            else if (i == indexP2 && indexP1 != indexP2)
            {
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(true);
                border1.gameObject.GetComponent<Image>().color = colorP2;
                border2.gameObject.GetComponent<Image>().color = colorP2;
                mark1.SetActive(false);
                mark2.SetActive(true);
            }
            else
            {
                border1.gameObject.SetActive(false);
                border2.gameObject.SetActive(false);
                border1.gameObject.GetComponent<Image>().color = Color.white;
                border2.gameObject.GetComponent<Image>().color = Color.white;
                mark1.SetActive(false);
                mark2.SetActive(false);
            }
        }
    }
}
