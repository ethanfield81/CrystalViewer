using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I may want to add a part that stores the nearest neightbors to this atom. 

public class Atom : MonoBehaviour
{
    private Element atomElement;
    private Vector3 atomPosition;
    private Vector3 latticePosition;

    public static Atom AtomPreFab;

    public string atomLabel;
    private UnitCell parentUnitCell;

    public string GetAtomLabel()
    {
        return atomLabel;
    }

    public void SetAtomLabel(string atomLabel)
    {
        this.atomLabel = atomLabel;
    }

    public Vector3 GetAtomPosition()
    {
        return atomPosition;
    }

    public Vector3 GetLatticePosition()
    {
        return latticePosition;
    }

    public Element GetElement()
    {
        return atomElement;
    }

    // Will be called by Unitcell while it is making all of the atoms originally
    public static Atom NewAtom(Element atomElement, Vector3 atomPosition, Vector3 latticePosition, string atomLabel, UnitCell parentUnitCell)
    {
        GameObject atomObject = Instantiate(Atom.AtomPreFab.gameObject, parentUnitCell.transform);

        Atom atom = atomObject.GetComponent<Atom>();

        atomObject.transform.localPosition = atomPosition;

        atom.atomElement = atomElement;
        atom.atomPosition = atomPosition;
        atom.latticePosition = latticePosition;
        //atomPosition is set relative to the unit cell and the unit cell will be scaled based off a, b, c
        atom.atomLabel = atomLabel;
        atom.parentUnitCell = parentUnitCell;

        float atomSize = (float)atomElement.GetAtomicRadius()* parentUnitCell.unitCellScale;
        atom.transform.localScale = new Vector3(atomSize, atomSize, atomSize);


        return (atom);
    }


    // Start is called before the first frame update
    void Start()

      // can I use the label of the atom and some other parameters to properly name all the different atoms that i'm creating?
        // don't do a dumb: GameObject atomLabel = Instantiate(this.AtomPreFab, this.atomPosition, Quaternion.identity, this.parentUnitCell, worldPositionStays = false);
    {   // can I use the label of the atom and some other parameters to properly name all the different atoms that i'm creating
        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
