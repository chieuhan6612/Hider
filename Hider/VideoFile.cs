using Accord.IO;
using Accord.Video.FFMPEG;
using AviFile;
using FFMpegSharp;
using FFMpegSharp.FFMPEG;
using NAudio.Wave;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Hider
{
    internal class VideoFile
    {
        private string Path;
        private string Filename;
        private long FrameCount;
        private int width;
        private int height;
        private double fps;
        private int bitrate;

        VideoFileReader reader;
        VideoFileWriter writer;

        private List<Bitmap> FrameList;

        public VideoFile(string path, string filename)
        {
            this.Path = path;
            this.Filename = filename;
            reader = new VideoFileReader();
            reader.Open(path);
            FrameCount = reader.FrameCount;
            this.width = reader.Width;
            this.height = reader.Height;
            this.fps = reader.FrameRate.Value;
            this.bitrate = reader.BitRate;
            FrameList = GetVideoFrames(FrameCount);
        }

        public VideoFile(Hider hider, string path, List<Bitmap> frames, int width, int height, double fps, int bitrate, byte[] message, int level)
        {
            Path = path;
            this.FrameCount = frames.Count();
            this.width = width;
            this.height = height;
            this.fps = fps;
            this.bitrate = bitrate;

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            FrameList = Stegano.HideDataToFrames(hider, frames, message, width, height, level);
            
            CreateVideoFromFrames();
            frames.Clear();
            //MessageBox.Show("The video is ready in " + Path, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public List<Bitmap> GetFrameList()
        {
            return FrameList;
        }

        public string GetPath()
        {
            return Path;
        }

        public string GetFilename()
        {
            return Filename;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public VideoFileWriter GetWriter()
        {
            return writer;
        }

        public int GetBitrate()
        {
            return bitrate;
        }

        public double GetFps()
        {
            return fps;
        }


        private List<Bitmap> GetVideoFrames(long frameCount)
        {
            List<Bitmap> frames = new List<Bitmap>();
           
            for (int i = 0; i < frameCount; i++)
            {
                frames.Add(reader.ReadVideoFrame());
            }
            return frames;
        }

        public void CreateVideoFromFrames()
        {

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            AviManager aviManager = new AviManager(Path, false);
            VideoStream vstream = aviManager.AddVideoStream(false, fps, FrameList[0]);

            vstream.GetFrameOpen();
            for (int i = 1; i < FrameList.Count; i++)
            {
                vstream.AddFrame(FrameList[i]);
                FrameList[i].Dispose();
            }

            //var audioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.wav";
            //aviManager.AddAudioStream(audioDir,0);

            aviManager.Close();
            vstream.GetFrameClose();
            aviManager = null;
            vstream = null;
            //File.Delete(audioDir);

        }


        public static void ExtractAudio(VideoFile audioVideo)
        {
            var newAudioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.wav";

            if (File.Exists(newAudioDir))
            {
                File.Delete(newAudioDir);
            }

            if (System.IO.Path.GetExtension(audioVideo.GetPath()).ToLower().Equals(".avi"))
            {
                AviManager aviManager =
                    new AviManager(audioVideo.GetPath(), true);
                AudioStream audioStream = aviManager.GetWaveStream();
                audioStream.ExportStream(newAudioDir);
                aviManager.Close();
                return;
            }

            var audioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.mp3";

            if (File.Exists(audioDir))
            {
                File.Delete(audioDir);
            }

            new FFMpeg().ExtractAudio(
                VideoInfo.FromPath(audioVideo.GetPath()),
                new FileInfo(audioDir));



            using (Mp3FileReader mp3 = new Mp3FileReader(audioDir))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(newAudioDir, pcm);
                }
            }

            File.Delete(audioDir);
        }

    }
}


//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using Accord.Video.FFMPEG;
//using FFMpegSharp;
//using FFMpegSharp.FFMPEG;
//using AviFile;
//using NAudio.Wave;

////using AForge.Video;
////using AForge.Video.VFW;
//namespace Hider
//{
//    internal class VideoFile
//    {
//        private string Path;
//        private string Filename;
//        private long FrameCount;
//        private int width;
//        private int height;
//        private double fps;
//        private int bitrate;

//        VideoFileReader reader;
//        VideoFileWriter writer;

//        private List<Bitmap> FrameList;

//        public VideoFile(string path, string filename)
//        {
//            this.Path = path;
//            this.Filename = filename;
//            reader = new VideoFileReader();
//            reader.Open(path);
//            FrameCount = reader.FrameCount;
//            this.width = reader.Width;
//            this.height = reader.Height;
//            this.fps = reader.FrameRate.Value;
//            this.bitrate = reader.BitRate;
//            FrameList = GetVideoFrames(FrameCount);
//        }

//        public VideoFile(string path, List<Bitmap> frames, int width, int height, double fps, int bitrate, string message)
//        {
//            Path = path;
//            this.FrameCount = frames.Count();
//            this.width = width;
//            this.height = height;
//            this.fps = fps;
//            this.bitrate = bitrate;

//            if (File.Exists(Path))
//            {
//                File.Delete(Path);
//            }

//            frames = Stegano.HideDataToFrames(frames, message, width, height);
//            FrameList = frames;

//            CreateVideoFromFrames();

//            MessageBox.Show("The video is ready in " + Path, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

//        }

//        public List<Bitmap> GetFrameList()
//        {
//            return FrameList;
//        }

//        public string GetPath()
//        {
//            return Path;
//        }

//        public string GetFilename()
//        {
//            return Filename;
//        }

//        public int GetWidth()
//        {
//            return width;
//        }

//        public int GetHeight()
//        {
//            return height;
//        }

//        public VideoFileWriter GetWriter()
//        {
//            return writer;
//        }

//        public int GetBitrate()
//        {
//            return bitrate;
//        }

//        public double GetFps()
//        {
//            return fps;
//        }


//        private List<Bitmap> GetVideoFrames(long frameCount)
//        {
//            List<Bitmap> frames = new List<Bitmap>();
//            Bitmap videoFrame;
//            for (int i = 0; i < frameCount; i++)
//            {
//                videoFrame = reader.ReadVideoFrame();
//                frames.Add(videoFrame);
//            }
//            return frames;
//        }

//        public void CreateVideoFromFrames()
//        {

//            if (File.Exists(Path))
//            {
//                File.Delete(Path);
//            }

//            AviManager aviManager = new AviManager(Path, false);
//            VideoStream vstream = aviManager.AddVideoStream(false, (int)fps, FrameList[0]);

//            vstream.GetFrameOpen();
//            for (int i = 1; i < FrameList.Count; i++)
//            {
//                vstream.AddFrame(FrameList[i]);
//            }

//            var audioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.wav";
//            aviManager.AddAudioStream(audioDir, 0);

//            aviManager.Close();
//            vstream.GetFrameClose();
//            File.Delete(audioDir);

//        }


//        public static void ExtractAudio(VideoFile audioVideo)
//        {
//            var audioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.mp3";

//            if (File.Exists(audioDir))
//            {
//                File.Delete(audioDir);
//            }

//            new FFMpeg().ExtractAudio(
//                VideoInfo.FromPath(audioVideo.GetPath()),
//                new FileInfo(audioDir));

//            var newAudioDir = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\META_DATA\audio.wav";

//            if (File.Exists(newAudioDir))
//            {
//                File.Delete(newAudioDir);
//            }

//            using (Mp3FileReader mp3 = new Mp3FileReader(audioDir))
//            {
//                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
//                {
//                    WaveFileWriter.CreateWaveFile(newAudioDir, pcm);
//                }
//            }

//            File.Delete(audioDir);
//        }

//    }
//}
