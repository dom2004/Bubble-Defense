using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")] 
    [SerializeField]
    private TowerUI[] towers; 

    private int currentSelectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public TowerUI GetSelectedTower()
    {
        return towers[currentSelectedTower]; 
    }

    public void SetSelectedTower(int selectedTower)
    {
        currentSelectedTower = selectedTower;
    }
}
