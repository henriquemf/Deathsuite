using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    private string selectedCharacter;
    private static string currentCharacter;
    private static int currentLifePoints;
    private static int currentMaxLifePoints;
    private static int currentAttackPoints;
    private static int currentExcelencePoints;
    private static int currentProficiencyPoints;
    private static AudioSource criticalHit;

    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;

        // Armazenar o nome do personagem selecionado em uma variável estática para uso futuro
        currentCharacter = selectedCharacter;
        //Debug.Log("Personagem selecionado: " + currentCharacter);
        // Setar os atributos base do personagem selecionado
        switch (selectedCharacter)
        {
            case "Hiromasa":
                currentLifePoints = 200;
                currentMaxLifePoints = 200;
                currentAttackPoints = 40;
                currentProficiencyPoints = 10;
                currentExcelencePoints = 5;
                break;
            case "Aldrich":
                currentLifePoints = 150;
                currentMaxLifePoints = 150;
                currentAttackPoints = 50;
                currentProficiencyPoints = 15;
                currentExcelencePoints = 2;
                break;
            case "Stigandr":
                currentLifePoints = 300;
                currentMaxLifePoints = 300;
                currentAttackPoints = 30;
                currentProficiencyPoints = 5;
                currentExcelencePoints = 10;
                break;
        }

        // Carregar a próxima cena
        SceneManager.LoadScene("TransitionScreen");
    }

    public static int MaximizeLifePoints()
    {
        currentLifePoints = currentMaxLifePoints;
        return currentLifePoints;
    }

    public static string GetCurrentCharacter()
    {
        return currentCharacter;
    }

    public static int GetCurrentLifePoints()
    {
        return currentLifePoints;
    }

    public static int GetCurrentMaxLifePoints()
    {
        return currentMaxLifePoints;
    }

    public static int GetCurrentAttackPoints()
    {
        return currentAttackPoints;
    }

    public static int GetCurrentProficiencyPoints()
    {
        return currentProficiencyPoints;
    }

    public static int GetCurrentExcelencePoints()
    {
        return currentExcelencePoints;
    }

    public static void AddLifePoints(int value)
    {
        currentLifePoints += value;
        currentMaxLifePoints += value;
        
        if (currentLifePoints <= 0)
        {
            currentLifePoints = 1;
        }
    }

    public static void TakeHit(int value) {
        currentLifePoints -= value;

        HealthBar.SetHealth(currentLifePoints);

        if (currentLifePoints <= 0)
        {
            // Carregar a cena de Game Over
            EnemySpawner.activeEnemyPrefabs.Clear();
            SceneManager.LoadScene("GameOver");
        }
    }

    public static void AddAttackPoints(int value)
    {
        currentAttackPoints += value;

        if (currentAttackPoints <= 0)
        {
            currentAttackPoints = 1;
        }
    }

    public static void AddProficiencyPoints(int value)
    {
        currentProficiencyPoints += value;

        if (currentProficiencyPoints <= 0)
        {
            currentProficiencyPoints = 1;
        }
    }

    public static void AddExcelencePoints(int value)
    {
        currentExcelencePoints += value;

        if (currentExcelencePoints <= 0)
        {
            currentExcelencePoints = 1;
        }
    }

    public static void SetLifePoints(int value)
    {
        currentLifePoints = value;
    }

    public static void SetAttackPoints(int value)
    {
        currentAttackPoints = value;
    }

    public static void SetProficiencyPoints(int value)
    {
        currentProficiencyPoints = value;
    }

    public static void SetExcelencePoints(int value)
    {
        currentExcelencePoints = value;
    }

    public static float CalculateDamage()
    {
        int criticalChance = currentProficiencyPoints;
        float criticalDamage = 2 + currentExcelencePoints/10;

        if (criticalChance > 100)
        {
            criticalChance = 100;
        }

        if (PlayerCharacter.GetCurrentCharacter() == "Stigandr")
        {
            criticalHit = GameObject.Find("StigandrCritical").GetComponent<AudioSource>();
        }

        else if (PlayerCharacter.GetCurrentCharacter() == "Aldrich")
        {
            criticalHit = GameObject.Find("AldrichCritical").GetComponent<AudioSource>();
        }

        else if (PlayerCharacter.GetCurrentCharacter() == "Hiromasa")
        {
            Debug.Log("Hiromasa Critical");
            criticalHit = GameObject.Find("HiromasaCritical").GetComponent<AudioSource>();
        }
//Debug.Log
        if (Random.Range(0, 100) <= criticalChance)
        {
            criticalHit.Play();
            return criticalDamage * (currentAttackPoints);
        }
        else 
        {
            return currentAttackPoints;
        }
    }

    public static void ResetCharacter()
    {
        switch (currentCharacter)
        {
            case "Hiromasa":
                currentLifePoints = 200;
                currentMaxLifePoints = 200;
                currentAttackPoints = 40;
                currentProficiencyPoints = 10;
                currentExcelencePoints = 5;
                break;
            case "Aldrich":
                currentLifePoints = 150;
                currentMaxLifePoints = 150;
                currentAttackPoints = 50;
                currentProficiencyPoints = 15;
                currentExcelencePoints = 2;
                break;
            case "Stigandr":
                currentLifePoints = 300;
                currentMaxLifePoints = 300;
                currentAttackPoints = 30;
                currentProficiencyPoints = 5;
                currentExcelencePoints = 10;
                break;
        }
    }
}
