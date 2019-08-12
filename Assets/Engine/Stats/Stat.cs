using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int baseValue, curValue;

    #region OPERATORS

    public static bool operator <(Stat a, Stat b) => a.curValue < b.curValue;

    public static bool operator <=(Stat a, Stat b) => a.curValue <= b.curValue;

    public static bool operator >(Stat a, Stat b) => a.curValue > b.curValue;

    public static bool operator >=(Stat a, Stat b) => a.curValue >= b.curValue;

    public static int operator *(Stat a, Stat b) => a.curValue * b.curValue;

    public static int operator *(Stat a, int b) => a.curValue * b;

    public static int operator *(Stat a, double b) => (int) (a.curValue * b);

    public static int operator +(Stat a, Stat b) => a.curValue + b.curValue;

    public static int operator +(Stat a, int b) => a.curValue + b;

    public static int operator +(Stat a, double b) => (int) (a.curValue + b);

    public static int operator -(Stat a, Stat b) => a.curValue - b.curValue;

    public static int operator -(Stat a, int b) => a.curValue - b;

    public static int operator -(Stat a, double b) => (int) (a.curValue - b);

    #endregion
}