using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorScheme: Singleton<ColorScheme>{

    protected ColorScheme() { }

    //Rarity Colours
    public Color lightBronze = new Color(.383f, .27f, .109f);
    public Color darkBronze = new Color(.16f, .098f, .016f);

    public Color lightSilver = new Color(.887f, .875f, .852f);
    public Color darkSilver = new Color(.578f, .551f, .516f);

    public Color lightGold = new Color(.996f, .75f, 0f);
    public Color darkGold = new Color(.754f, .582f, 0f);

    public RarityScheme common;
    public RarityScheme uncommon;
    public RarityScheme rare;

    public void Setup()
    {
        common = new RarityScheme(lightBronze, darkBronze);
        uncommon = new RarityScheme(lightSilver, darkSilver);
        rare = new RarityScheme(lightGold, darkGold);
    }

    void Start()
    {
        Setup();
    }
}

public struct RarityScheme
{
    public readonly Color Background;
    public readonly Color Foreground;

    public RarityScheme(Color BG, Color FG)
    {
        this.Background = BG;
        this.Foreground = FG;
    }

    public RarityScheme(RarityScheme scheme)
    {
        this.Background = scheme.Background;
        this.Foreground = scheme.Foreground;
    }
}
