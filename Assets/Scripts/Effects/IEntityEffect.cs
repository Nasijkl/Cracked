using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityEffect 
{
    void Resolve(RuntimeCharacter source, RuntimeCharacter target);

    void aResolve(RuntimeCharacter source, RuntimeCharacter target, int input_value);
}
