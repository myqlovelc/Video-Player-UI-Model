using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class VideoValidator
{
    public static string[] VideoSuffixList = { ".mp4", ".m4v", ".3pg", ".3g2", ".ts", ".webm", ".mkv", ".wmv", ".asf", ".avi", ".flv", ".mpeg", ".mpg" };

    public static bool ValidateVideoFormat (string VideoName) {
        return VideoSuffixList.Contains(Path.GetExtension(VideoName));
    }

    public static string GetPatternString()
    {
        StringBuilder str = new StringBuilder("(");
        for (int i = 0; i < VideoSuffixList.Length; i++)
        {
            if (i == 0) {
                str.Append("*" + VideoSuffixList[i]);
            }
            else
            {
                str.Append("|*" + VideoSuffixList[i]);
            }
        }
        str.Append(")");
        Debug.Log(str.ToString());
        return str.ToString();
    }
}
