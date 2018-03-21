using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemsManager : MonoBehaviour
{
    public Button buttonPath;
    public Button buttonTrap;
    public Button buttonEnergy;
    public Button buttonVoodoo;

    public GameObject path;
    public GameObject trap;

    private float pathTime = 0.0f;
    private float trapTime = 0.0f;
    private float energyTime = 0.0f;
    private float voodooTime = 0.0f;
    private float pathCooldown = 60.0f;
    private float trapCooldown = 30.0f;
    private float energyCooldown = 45.0f;
    private float voodooCooldown = 90.0f;

    public static Action<int> EnergyEvent;


    // Use this for initialization
    private void Start()
    {
        if (GameEngine.GameDifficultyLevel == DifficultyLevel.Hardcore)
        {
            buttonPath.interactable = false;
            buttonTrap.interactable = false;
            buttonEnergy.interactable = false;
            buttonVoodoo.interactable = false;
            gameObject.GetComponent<ItemsManager>().enabled = false;
        }

        buttonPath.onClick.AddListener(UsePath);
        buttonTrap.onClick.AddListener(UseTrap);
        buttonEnergy.onClick.AddListener(UseEnergy);
        buttonVoodoo.onClick.AddListener(UseVoodoo);

        pathTime = pathCooldown;
        trapTime = trapCooldown;
        energyTime = energyCooldown;
        voodooTime = voodooCooldown;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.GameIsStarted && !GameEngine.GameIsPaused)
        {
            pathTime += Time.deltaTime;
            trapTime += Time.deltaTime;
            energyTime += Time.deltaTime;
            voodooTime += Time.deltaTime;
        }

        if (PlayerPrefs.GetInt("ItemPathCount") == 0 || pathTime < pathCooldown || GameEngine.GameIsFinished)
        {
            buttonPath.interactable = false;
        }
        else
        {
            buttonPath.interactable = true;
        }

        if (PlayerPrefs.GetInt("ItemTrapCount") == 0 || trapTime < trapCooldown || GameEngine.GameIsFinished)
        {
            buttonTrap.interactable = false;
        }
        else
        {
            buttonTrap.interactable = true;
        }

        if (PlayerPrefs.GetInt("ItemEnergyCount") == 0 || energyTime < energyCooldown || GameEngine.GameIsFinished)
        {
            buttonEnergy.interactable = false;
        }
        else
        {
            buttonEnergy.interactable = true;
        }

        if (PlayerPrefs.GetInt("ItemVoodooCount") == 0 || voodooTime < voodooCooldown || GameEngine.GameIsFinished)
        {
            buttonVoodoo.interactable = false;
        }
        else
        {
            buttonVoodoo.interactable = true;
        }
    }

    public void UsePath()
    {
        pathTime = 0;
        Instantiate(path);
        PlayerPrefs.SetInt("ItemPathCount", PlayerPrefs.GetInt("ItemPathCount") - 1);
    }

    public void UseTrap()
    {
        trapTime = 0;
        trap.transform.position = GameEngine.PlayerCoordinates + new Vector3(0, 0.01f,0);
        Instantiate(trap);
        PlayerPrefs.SetInt("ItemTrapCount", PlayerPrefs.GetInt("ItemTrapCount") - 1);
    }

    public void UseEnergy()
    {
        energyTime = 0;
        if (EnergyEvent != null)
        {
            EnergyEvent(Random.Range(15,25));
        }
        PlayerPrefs.SetInt("ItemCount", PlayerPrefs.GetInt("ItemCount") - 1);
    }

    public void UseVoodoo()
    {
        voodooTime = 0;
        GameEngine.Stunned();
        PlayerPrefs.SetInt("ItemVoodooCount", PlayerPrefs.GetInt("ItemVoodooCount")-1);
    }
}
