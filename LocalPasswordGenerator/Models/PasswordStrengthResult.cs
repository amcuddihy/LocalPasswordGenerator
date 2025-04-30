using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPasswordGenerator.Models;

public class PasswordStrengthResult 
{
    public int Score { get; set; } // 0-4
    public string CrackTimeDisplay { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public List<string> Suggestions { get; set; } = new List<string>();
}