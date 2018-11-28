using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class RegExpTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string[] numbers =
        {
            "0.123",
            "1000.123",
            "123",
            "abc.fa",
            "hello",
            ".123",
            "123."
        };


        //      @""      "//" in Javascript

        //               @"^(\.)?(\d+)(\.\d+)?$"        ^시작 $끝 
        //               @"(\d+)?\.?(\d+)"
        string pattern = @"^(\d*)(\.\d+)?$";

        foreach(var s in numbers)
        {
            if (Regex.IsMatch(s, pattern))
                print(s + ": valid");
            else
                print(s + ": envalid");
        }

        //Match match;
        //match.Groups[0] 전체
        //match.Groups[1] 그룹1
        //match.Groups[2] 그룹2

        foreach(var s in numbers)
        {
            foreach(Match match in Regex.Matches(s, pattern, RegexOptions.IgnoreCase))  //RegexOptions.IgnoreCase 대소문자 무시
            {
                print(match.Groups[0].Value + ": " + match.Groups[1].Value + " - " +    //match.Groups[0].Value = s
                                                     match.Groups[1].Index + ", " +
                                                     match.Groups[2].Value + " - " +
                                                     match.Groups[2].Index + ", ");
            }
        }
    }
}
