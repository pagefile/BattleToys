using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Version
{
    // This is dumb, but the editor has no global version variable to edit
    public int Major = 0;
    public int Minor = 1;
    public int Internal = 0;
    
    public string Full
    {
        get { return Major + "." + Minor + "." + Internal; }
    }
}
