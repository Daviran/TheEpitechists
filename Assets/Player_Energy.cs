using UnityEngine;

public class Player_Energy : MonoBehaviour
{
    public EnergyBar energyBar;
    //public int currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
       // currentEnergy = 0;
        energyBar.SetMaxEnergy(0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            TakeEnergy(20);
        }*/
        GainEnergy();
    }

    void GainEnergy()
    {
        energyBar.SetEnergy(0.01f);

    }
}
