using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bond : MonoBehaviour
{
    private Atom atom1;
    private Atom atom2;

    // try to use a range of calculated radii to be able guess where the bonds exist

    // I think that I might try to make a half of a bond in each direction towards a nearest neighbor.
    // finding position in world space from the local space
    // cube.transform.TransformPoint()
    public Atom GetAtom1()
    {
        return atom1;
    }

    public Atom GetAtom2()
    {
        return atom2;
    }

    public void NewBond(Atom atom1, Atom atom2)
    {
        this.atom1 = atom1;
        this.atom2 = atom2;
    }

    // go from the center of one atom to the center of the other? could be center + radius in correct direction so it goes from surface to surface

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
