using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusComponent : MonoBehaviour {

    public int Health;
    public int MaxHealth;

    private void Awake()
    {
        MaxHealth = 5;
        Health = 5;
    }

    private void Update()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Health: ");
        for(int i = 0; i < MaxHealth; i++)
        {
            sb.Append(i < Health ? "O" : "_");
            if (i < MaxHealth -1)
            {
                sb.Append(" ");
            }
        }
        sb.Append("\n");
        sb.AppendLine("Subitem A: None");
        sb.AppendLine("Subitem B: None");
        GetComponent<Text>().text = sb.ToString();
    }
}
