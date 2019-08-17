using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IClickableEntity
{
    bool IsSelected { get; set; }
    
    void IsChanged();
}
