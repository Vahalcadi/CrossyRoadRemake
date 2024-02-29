using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> unlockedCharacters;
    [NonSerialized] public int selectedCharacter = 0;

    private void Start()
    {
        int unlockedCharactersCount = PlayerPrefs.GetInt("unlockedSkins");

        for (int i = 0; i < unlockedCharactersCount; i++)
        {
            unlockedCharacters.Add(characters[i]);
        }
    }

    public void NextCharacter()
    {
        unlockedCharacters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % unlockedCharacters.Count;
        unlockedCharacters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        unlockedCharacters[selectedCharacter].SetActive(false);
        selectedCharacter--;

        if (selectedCharacter < 0)
            selectedCharacter += unlockedCharacters.Count;

        unlockedCharacters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene("MainScene");
    }
}
