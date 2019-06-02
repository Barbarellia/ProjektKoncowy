using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentences
{
    private static string[] happyLady;
    private static string[] happyCat;
    public Sentences()
    {
        happyCat = new string[] { "Cat: *meows and rubbs on your leg*", "You: You're welcome little one." };
        happyLady = new string[] { "Lady: Oh you found him!", "Lady: Thank you so mutch!", "Cat: *meows happilly and purrs*" };
    }
    public string[] HappyLady()
    {
        return happyLady;
    }
    public string[] HappyCat()
    {
        return happyCat;
    }
}
