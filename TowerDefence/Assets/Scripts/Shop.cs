using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;
    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        Debug.Log("Selected");
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Purchased 2");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

}
