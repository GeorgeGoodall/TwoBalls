using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IWall
{
    void grab(Head head);
    void release();
    void release(Head head);
}