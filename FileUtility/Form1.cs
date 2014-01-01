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
            try
            {
                string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff";

                var filesInDir = Directory.EnumerateFiles(directory,"*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));
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

                        Image myImage = Image.FromFile(file);
                        try
                        {
                           if (myImage.PropertyIdList.Any(x => x == 36867))
                           {
                               PropertyItem propItem = myImage.GetPropertyItem(36867);
                               sdate = Encoding.UTF8.GetString(propItem.Value).Trim();
                           }
                           else
                           {  
                               sdate = File.GetCreationTime(file).ToString();
                           }
                            myImage.Dispose();
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
                        }
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
            //    incrimentCounter++;
                outputResult = outputDirectory+@"\"+ Path.GetFileNameWithoutExtension(outputFile) + "_" + incrimentCounter + outputsuffix;
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
