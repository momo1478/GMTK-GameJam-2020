using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public int size;
    private List<Projectile> projectiles; 

    public Pool(int size)
    {
        this.size = size;
        projectiles = new List<Projectile>();
    }

    public Projectile Get()
    {
        return null;
    }

}
