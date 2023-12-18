
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hider
{

    public partial class Hider : Form
    {
        VideoFile coverVideo;
        VideoFile newVideo;
        //VideoFile alreadyCoveredVideo;

        Stegano stegano;
        private string inputFile;
        private int percent;
        private byte[] data;
        private byte[] key;
        public Hider()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            inputFile = "";
            percent = 0;
        }
        //private bool ConvertToMp4(string PathInput, string PathOutput)
        //{
        //    Process process = new Process();
        //    process.StartInfo.FileName = "E:\\Hider\\Hider\\ffmpeg.exe";
        //    process.StartInfo.Arguments = "-i " + PathInput + " -qscale 0 " + PathOutput;
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.CreateNoWindow = true;
        //    process.Start();

        //    //string output = process.StandardOutput.ReadToEnd();
        //    process.WaitForExit();
        //    return File.Exists(PathOutput);
        //}
        private void ButtonBrowseInput_Click(object sender, EventArgs e)
        {
            ClearData();
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog()
                {
                    Multiselect = false,
                    ValidateNames = true,
                    Filter = "All Video Files|*.wmv;*.wav;*.mp3;*.mp4;*.mkv;*.avi"
                })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo fi = new FileInfo(ofd.FileName);
                        coverVideo = new VideoFile(fi.FullName, Path.GetFileName(fi.FullName));
                        TextBoxInput.Text = Path.GetFileName(fi.FullName);
                    }
                }
            }
            catch (Exception Ex)
            {
                WriteLog(Ex.Message);
            }

        }

        void FullProgessBar()
        {
            ProgessBarPercent.Value = 0;

            for (int i = 0; i <= 100; ++i)
            {
                ProgessBarPercent.PerformStep();
                LabelPercent.Text = i.ToString() + "%";
            }
            RichTextBoxLog.Text = "Success";
            coverVideo = null;
        }
        void ClearData()
        {
            TextBoxInput.Text = string.Empty;
            RichTextBoxData.Text = string.Empty;
            RichTextBoxLog.Text = string.Empty;
            RichTextBoxKey.Text = string.Empty;
            ProgessBarPercent.Value = 0;
            LabelPercent.Text = string.Empty;
            LabelPercent.Text = "0%";
            key = null;
            data = null;
            coverVideo = null;
            newVideo = null;
            percent = 0;
            //alreadyCoveredVideo = null;
            stegano = null;
        }
        public void UpdateProgessbar(int val)
        {
            //int oldVal = ProgessBarPercent.Value;
            ProgessBarPercent.Value = val;
        }

        public void SetProgessbar(int val)
        {
            ProgessBarPercent.Maximum = val;
        }

        void WriteLog(string log, string type = "Error")
        {
            RichTextBoxLog.Text += type + ": " + log;
            RichTextBoxLog.Text += "\n";

        }

        private void ButtonHide_Click_1(object sender, EventArgs e)
        {
            if (coverVideo == null)
            {
                WriteLog("Please select cover video");
                return;
            }

            if (string.Empty == RichTextBoxKey.Text)
            {
                WriteLog("Empty key");
                return;
            }

            key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(RichTextBoxKey.Text));

            if (null == data)
            {
                data = Encoding.UTF8.GetBytes(RichTextBoxData.Text);
            }
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.DefaultExt = "avi";
                save.Filter = "Avi File (*.avi;)|*.avi;";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(save.FileName);
                    if (coverVideo.GetPath().Equals(fi.FullName))
                    {
                        WriteLog("Invalid path");
                    }
                    else
                    {
                        int level = int.Parse(ComboBoxHighLevel.Text);
                        //VideoFile.ExtractAudio(coverVideo);
                        data = AES.Encrypt(data, key);
                        newVideo = new VideoFile(this, fi.FullName, coverVideo.GetFrameList(), coverVideo.GetWidth(), coverVideo.GetHeight(), coverVideo.GetFps(), coverVideo.GetBitrate(), data, level);
                    }
                    WriteLog("Success. Output file in " + fi.FullName, "");
                }
            }
            catch (Exception ex)
            {
                newVideo = null;
                coverVideo = null;
                data = null;
                WriteLog(ex.Message);

            }
            newVideo = null;
            coverVideo = null;
            data = null;
        }

        private void ButtonExtract_Click(object sender, EventArgs e)
        {
            data = null;

            if (string.Empty == RichTextBoxKey.Text)
            {
                WriteLog("Empty key");
                return;
            }
            key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(RichTextBoxKey.Text));

            if (coverVideo == null)
            {
                WriteLog("Please Select Video");
            }
            else
            {
                try
                {
                    data = Stegano.GetDataFromFrames(this, coverVideo.GetFrameList(), coverVideo.GetWidth(), coverVideo.GetHeight());
                    if (data.Length > 100)
                    {
                        RichTextBoxData.Text = string.Empty;
                        WriteLog("Data too long, can't show in textbox. Please write data to file", "");
                    }
                    else
                    {
                        RichTextBoxData.Text = Encoding.UTF8.GetString(data);
                    }
                    data = AES.Decrypt(data, key);
                    //FullProgessBar();
                }
                catch (Exception ex)
                {
                    coverVideo = null;
                    WriteLog(ex.Message);
                }
            }
        }

        public static float calcMSE(List<Bitmap> bitmapsReal, List<Bitmap> bitmapsStego)
        {
            float difference = 0f;

            //foreach (var bitmap in bitmapsReal.ToList().Select((r, i) => new { Row = r, Index = i }))
            for (int k = 0; k < bitmapsReal.Count; k++)
            {
                for (int i = 0; i < bitmapsReal[k].Height; i++)
                {
                    // pass through each row
                    for (int j = 0; j < bitmapsReal[k].Width; j++)
                    {
                        Color realPx = bitmapsReal[k].GetPixel(j, i);
                        Color stegoPx = bitmapsStego[k].GetPixel(j, i);
                        int diffR = realPx.R - stegoPx.R;
                        int diffG = realPx.G - stegoPx.G;
                        int diffB = realPx.B - stegoPx.B;
                        int diffRGB = diffR + diffG + diffB;
                        difference += (diffRGB * diffRGB);
                    }
                }
            }
            float sizeOfPixel = bitmapsReal[0].Width * bitmapsReal[0].Height * 1.0f;
            return (1 / sizeOfPixel) * difference;
        }
        public static double calcPSNR(float MSEvalue, int maxValue = 255)
        {
            return MSEvalue == 0 ? 100.0f : 10.0f * Math.Log10((maxValue * maxValue) / MSEvalue);
        }

        public static double calcAD(List<Bitmap> bitmapsReal, List<Bitmap> bitmapsStego)
        {
            float difference = 0f;

            for (int k = 0; k < bitmapsReal.Count; k++)
            {
                for (int i = 0; i < bitmapsReal[k].Height; i++)
                {
                    // pass through each row
                    for (int j = 0; j < bitmapsReal[k].Width; j++)
                    {
                        Color realPx = bitmapsReal[k].GetPixel(j, i);
                        Color stegoPx = bitmapsStego[k].GetPixel(j, i);
                        int diffR = realPx.R - stegoPx.R;
                        int diffG = realPx.G - stegoPx.G;
                        int diffB = realPx.B - stegoPx.B;
                        int diffRGB = diffR + diffG + diffB;
                        difference += (diffRGB * diffRGB);
                    }
                }
            }
            float sizeOfPixel = bitmapsReal[0].Width * bitmapsReal[0].Height * 1.0f;
            return difference / sizeOfPixel;
        }

        public static double calcNC(List<Bitmap> bitmapsReal, List<Bitmap> bitmapsStego)
        {
            float sum = 0f;
            float sumReal = 0f;

            for (int k = 0; k < bitmapsReal.Count; k++)
            {
                for (int i = 0; i < bitmapsReal[k].Height; i++)
                {
                    // pass through each row
                    for (int j = 0; j < bitmapsReal[k].Width; j++)
                    {
                        Color realPx = bitmapsReal[k].GetPixel(j, i);
                        Color stegoPx = bitmapsStego[k].GetPixel(j, i);
                        int diffR = realPx.R * stegoPx.R;
                        int diffG = realPx.G * stegoPx.G;
                        int diffB = realPx.B * stegoPx.B;
                        sum += diffR + diffG + diffB;
                        sumReal += ((realPx.R + realPx.G + realPx.B) * (realPx.R + realPx.G + realPx.B));
                    }
                }
            }
            return sum / sumReal;
        }

        public static double calcNAE(List<Bitmap> bitmapsReal, List<Bitmap> bitmapsStego)
        {
            float difference = 0f;
            int totalValue = 0;

            for (int k = 0; k < bitmapsReal.Count; k++)
            {
                for (int i = 0; i < bitmapsReal[k].Height; i++)
                {
                    // pass through each row
                    for (int j = 0; j < bitmapsReal[k].Width; j++)
                    {
                        Color realPx = bitmapsReal[k].GetPixel(j, i);
                        Color stegoPx = bitmapsStego[k].GetPixel(j, i);
                        totalValue += (realPx.R + realPx.G + realPx.B);
                        int diffR = Math.Abs(realPx.R - stegoPx.R);
                        int diffG = Math.Abs(realPx.G - stegoPx.G);
                        int diffB = Math.Abs(realPx.B - stegoPx.B);
                        int diffRGB = diffR + diffG + diffB;
                        difference += diffRGB;
                    }
                }
            }
            return difference / (Math.Abs(totalValue));
        }

        public static double calcSC(List<Bitmap> bitmapsReal, List<Bitmap> bitmapsStego)
        {
            double realSum = 0;
            double stegoSum = 0;

            for (int k = 0; k < bitmapsReal.Count; k++)
            {
                for (int i = 0; i < bitmapsReal[k].Height; i++)
                {
                    // pass through each row
                    for (int j = 0; j < bitmapsReal[k].Width; j++)
                    {
                        Color realPx = bitmapsReal[k].GetPixel(j, i);
                        Color stegoPx = bitmapsStego[k].GetPixel(j, i);
                        realSum += ((realPx.R + realPx.G + realPx.B) * (realPx.R + realPx.G + realPx.B));
                        stegoSum += ((stegoPx.R + stegoPx.G + stegoPx.B) * (stegoPx.R + stegoPx.G + stegoPx.B));

                    }
                }
            }
            return (stegoSum / realSum);
        }

        private void ComboBoxHighLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonBrowseData_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialogData = new OpenFileDialog();
            OpenFileDialogData.Filter = "All Files (*.*)|*.*";
            DialogResult result = OpenFileDialogData.ShowDialog();
            if (result == DialogResult.OK)
            {
                data = File.ReadAllBytes(OpenFileDialogData.FileName);
                RichTextBoxData.Text = "Data from file " + OpenFileDialogData.FileName;
            }
        }

        private void TextBoxData_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "All Files (*.*)|*.*";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(save.FileName, data);
                    WriteLog("Write data to " + save.FileName + " success", "");
                }
            }
            catch (Exception ex)
            {

                WriteLog(ex.Message);
            }
        }
    }
}