using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

public class FfmepgUtil
{
    public static void GetImageFromVideo(string ffmpegpath, string sourceFile, string imgpath, string imgsize)
    {
        ProcessStartInfo ImgstartInfo = new ProcessStartInfo(ffmpegpath);
        ImgstartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        ImgstartInfo.UseShellExecute = false;
        ImgstartInfo.CreateNoWindow = true;
        /*参数设置
         * -y（覆盖输出文件，即如果生成的文件（flv_img）已经存在的话，不经提示就覆盖掉了）
         * -i 1.avi 输入文件
         * -f image2 指定输出格式
         * -ss 8 后跟的单位为秒，从指定时间点开始转换任务
         * -vframes
         * -s 指定分辨率
         */
        //duration: 00:00:00.00
        string VideoLength = GetVideoDuration(ffmpegpath, sourceFile); ;
        string[] time = VideoLength.Split(':');
        int seconds = int.Parse(time[0]) * 60 * 60 + int.Parse(time[1]) * 60 + int.Parse(time[2]);
        int ss = seconds > 5 ? 5 : seconds - 1;
        ImgstartInfo.Arguments = " -i " + sourceFile + " -y -f image2 -ss " + "1" + " -vframes 1 -s " + imgsize + " " + imgpath;
        try
        {
            Process.Start(ImgstartInfo);
        }
        catch
        {
        }
    }

    private static string GetVideoDuration(string ffmpegfile, string sourceFile)
    {
        using (Process ffmpeg = new Process())
        {
            String duration;  // soon will hold our video's duration in the form "HH:MM:SS.UU"
            String result;  // temp variable holding a string representation of our video's duration
            StreamReader errorreader;  // StringWriter to hold output from ffmpeg

            // we want to execute the process without opening a shell
            ffmpeg.StartInfo.UseShellExecute = false;
            //ffmpeg.StartInfo.ErrorDialog = false;
            ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            // redirect StandardError so we can parse it
            // for some reason the output comes through over StandardError
            ffmpeg.StartInfo.RedirectStandardError = true;

            // set the file name of our process, including the full path
            // (as well as quotes, as if you were calling it from the command-line)
            ffmpeg.StartInfo.FileName = ffmpegfile;

            // set the command-line arguments of our process, including full paths of any files
            // (as well as quotes, as if you were passing these arguments on the command-line)
            ffmpeg.StartInfo.Arguments = "-i " + sourceFile;

            // start the process
            ffmpeg.Start();

            // now that the process is started, we can redirect output to the StreamReader we defined
            errorreader = ffmpeg.StandardError;

            // wait until ffmpeg comes back
            ffmpeg.WaitForExit();

            // read the output from ffmpeg, which for some reason is found in Process.StandardError
            result = errorreader.ReadToEnd();

            // a little convoluded, this string manipulation...
            // working from the inside out, it:
            // takes a substring of result, starting from the end of the "Duration: " label contained within,
            // (execute "ffmpeg.exe -i somevideofile" on the command-line to verify for yourself that it is there)
            // and going the full length of the timestamp

            UnityEngine.Debug.Log(result);
            duration = result.Substring(result.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00").Length);
            return duration;
        }
    }
}
