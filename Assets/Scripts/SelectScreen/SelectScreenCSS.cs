using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScreenCSS : MonoBehaviour
{
    public Transform rootChars;

    public List<CharacterPickSO> characters = new List<CharacterPickSO>();
    public GameObject charCellPrefab;

    public List<GameObject> cells = new List<GameObject>();

    int indexP1;
    int indexP2;

    bool selectP1;
    bool selectP2;

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
        foreach(CharacterPickSO character in characters)
        {
            SpawnCharacterCell(character);
            UpdateCells();
        }
    }

    private void SpawnCharacterCell(CharacterPickSO character)
    {
        GameObject charCell = Instantiate(charCellPrefab, rootChars);

        charCell.name = character.characterSiglas;

        Transform maskObj = charCell.transform.Find("Mask");

        Image portrait = maskObj.Find("Retrato").GetComponent<Image>();
        Text name = maskObj.Find("Nombre").GetComponentInChildren<Text>();

        charCell.transform.Find("Border1").gameObject.SetActive(false);
        charCell.transform.Find("Border2").gameObject.SetActive(false);


        portrait.sprite = character.characterSprite;
        name.text = character.characterName;
        cells.Add(charCell);
    }

    private void Update()
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            SelectPlayer(1,true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SelectPlayer(2,true);
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
                    panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[indexP1].characterName;
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
                    panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[indexP2].characterName;
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
                panelP1.transform.Find("Nombre").GetComponent<Text>().text = "???";
            }
            else if (player == 2)
            {
                if (!panelP2.activeSelf) { panelP2.SetActive(true); }
                panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[indexP2].siluetaSprite;
                panelP2.transform.Find("Nombre").GetComponent<Text>().text = "???";
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
                panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[i].characterName;
            }
            else if(player == 2)
            {
                panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[i].siluetaSprite;
                panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[i].characterName;
            }
            yield return new WaitForSeconds(waitTime);
        }
        int rndChar = Random.Range(0, characters.Count - 1);
        if (player == 1)
        {
            panelP1.transform.Find("Foto").GetComponent<Image>().sprite = characters[rndChar].characterSprite;
            panelP1.transform.Find("Nombre").GetComponent<Text>().text = characters[rndChar].characterName;
            StopCoroutine(randomRoutine1);
        }
        else if (player == 2)
        {
            panelP2.transform.Find("Foto").GetComponent<Image>().sprite = characters[rndChar].characterSprite;
            panelP2.transform.Find("Nombre").GetComponent<Text>().text = characters[rndChar].characterName;
            StopCoroutine(randomRoutine2);
        }

    }

    public void UpdateCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            Transform border1 = cells[i].transform.Find("Border1");
            Transform border2 = cells[i].transform.Find("Border2");
            startSize = border1.GetComponent<RectTransform>().sizeDelta.x;
            if (i == indexP1 && indexP1 == indexP2)
            {
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(true);
                border1.gameObject.GetComponent<Image>().color = colorP1;
                border2.gameObject.GetComponent<Image>().color = colorP2;
            }
            else if (i == indexP1 && indexP1 != indexP2)
            {
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(true);
                border1.gameObject.GetComponent<Image>().color = colorP1;
                border2.gameObject.GetComponent<Image>().color = colorP1;
            }
            else if (i == indexP2 && indexP1 != indexP2)
            {
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(true);
                border1.gameObject.GetComponent<Image>().color = colorP2;
                border2.gameObject.GetComponent<Image>().color = colorP2;
            }
            else
            {
                border1.gameObject.SetActive(false);
                border2.gameObject.SetActive(false);
                border1.gameObject.GetComponent<Image>().color = Color.white;
                border2.gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
