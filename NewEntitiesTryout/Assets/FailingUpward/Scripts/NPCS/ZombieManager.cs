using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    int updatePerFrame = 2;
    List<Zombie> zombieList = new List<Zombie>();
    // Start is called before the first frame update
    void Start()
    {
        zombieList = GetComponentsInChildren<Zombie>().ToList();
    }

    int lastUpdated = 0;
    // Update is called once per frame
    void Update()
    {
        int startPoint = lastUpdated;
        bool stopLooping = false;
        int amountUpdated = 0;
        for (int i = startPoint + 1; !stopLooping; i++)
        {
            lastUpdated = i;
            zombieList[i].UpdateZombie();
            if (i >= zombieList.Count -1)
            {
                i = 0;
                lastUpdated = 0;
            }

            amountUpdated++;
            if(amountUpdated > updatePerFrame)
            {
                stopLooping = true;
            }
        }
    }
}
