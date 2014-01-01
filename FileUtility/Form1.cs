using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;

namespace FileUtility
{
    public partial class Form1 : Form
    {
        string outputsuffix;
        public Form1()
        {
            InitializeComponent();
        }
        int incrimentCounter;
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            lblResult.Text = "Working...Please Wait";
            string directory = directorySelection.Text;
          
            string sdate;
            string secondhalf;
            string firsthalf;
            string takenMonth;
            string takenYear;
            string takenFolder;
            string newFileSerialised;
            string outputResult;
            bool exist;
            int fileCount = 0;
            int exceptionCount = 0;
            DateTime dtaken;

            try
            {
                var filesInDir = Directory.EnumerateFiles(directory, "*.jpg", SearchOption.AllDirectories);
                int countFiles = filesInDir.Count();
                if (countFiles >0)
                {
                    string outputcheck = outputDirectory.Text.Substring(outputDirectory.Text.Length-1,1);
                    if (outputcheck != @"\")
                    {
                        outputDirectory.Text = outputDirectory.Text + @"\";
                    }

                    foreach (string file in filesInDir)
                    {                       
                        fileCount++;
                        lblResult.Text = "Currently Processing: " + fileCount.ToString() + " of " +countFiles;
                        lblResult.Refresh();

                        //If Image then group by date taken
                        Image myImage = Image.FromFile(file);
                        try
                        {
                            PropertyItem propItem = myImage.GetPropertyItem(306);
                          
                            sdate = Encoding.UTF8.GetString(propItem.Value).Trim();
                            secondhalf = sdate.Substring(sdate.IndexOf(" "), (sdate.Length - sdate.IndexOf(" ")));
                            firsthalf = sdate.Substring(0, 10);
                            firsthalf = firsthalf.Replace(":", "-");
                            sdate = firsthalf + secondhalf;
                            dtaken = DateTime.Parse(sdate);
                            takenMonth = dtaken.ToString("MMM", CultureInfo.InvariantCulture);
                            takenYear = dtaken.Year.ToString();
                            takenFolder = takenMonth + " " + takenYear;
                            outputsuffix = Path.GetExtension(file);
                            newFileSerialised = dtaken.ToString().Replace(@"/", "").Replace(":", "").Replace(" ", "") + outputsuffix;
                            outputResult = outputDirectory.Text + takenFolder + @"\" + newFileSerialised;
                            if (Directory.Exists(outputDirectory.Text + takenFolder))
                            {

                                exist = File.Exists(outputResult);

                                if (exist)
                                {
                                    outputResult = incrimentCount(outputDirectory.Text + @"/" + takenFolder, newFileSerialised, outputResult, 0);
                                }

                                File.Copy(file, outputResult);
                            }
                            else
                            {
                                Directory.CreateDirectory(outputDirectory.Text + takenFolder);
                                File.Copy(file, outputResult);
                            }


                        }
                        catch
                        {
                            //Anything that isnt an image will get filed by created date..
                            dtaken = File.GetCreationTime(file);
                            takenMonth = dtaken.ToString("MMM", CultureInfo.InvariantCulture);
                            takenYear = dtaken.Year.ToString();
                            takenFolder = takenMonth + " " + takenYear;
                            outputsuffix = Path.GetExtension(file);
                            newFileSerialised = dtaken.ToString().Replace(@"/", "").Replace(":", "").Replace(" ", "") + outputsuffix;
                        }
                        lblResult.Text = "processed";
                        lblResult.Refresh();
                        myImage.Dispose();
                    }

                    lblResult.Text = "Files Processed:" + fileCount + " Exceptions:" + exceptionCount;
                    button1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No Files Found");
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.ToString();
                button1.Enabled = true;
            }
        }

        /// <summary>
        /// Call this to iterate through the file in directories
        /// </summary>
        /// <param name="source">Source Directory</param>
        /// <param name="destination">Destination Directory</param>
        public string incrimentCount(string outputDirectory, string outputFile, string outputResult, int incriment)
        {
            outputsuffix = Path.GetExtension(outputFile);
            if (File.Exists(outputResult))
            {
               
                string checkFileNumber = Path.GetFileNameWithoutExtension(outputFile);
                incrimentCounter = Directory.EnumerateFiles(outputDirectory, "*" + checkFileNumber + "*").Count();
                incrimentCounter++;
                outputResult = outputResult.ToLower().Replace(".jpg", "");
                outputResult = outputResult + "_" + incrimentCounter + outputsuffix;
                return outputResult;
            }
            return outputResult;
        }

        private void directorySelection_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgSourceBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                directorySelection.Text = dlgSourceBrowser.SelectedPath;

            }
        }

        private void btnDestSearch_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgSourceBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                outputDirectory.Text = dlgSourceBrowser.SelectedPath;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblException.Text = "";
            lblResult.Text = "";
        }
    }
}
