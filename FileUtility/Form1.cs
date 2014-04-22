using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;

namespace FileUtility
{
    public partial class Form1 : Form
    {
     
        #region datatypes
        BackgroundWorker fileWorker;
        string directory;
        string acceptedFileTypes;
        string sdate;
        string secondhalf;
        string firsthalf;
        string takenMonth;
        string takenYear;
        string takenFolder;
        string newFileSerialised;
        string outputResult;
        bool exist;
        int folderMode = 0;
        int fileCount = 0;
        int exceptionCount = 0;
        int incrimentCounter =0;
        DateTime dtaken;
        string outputsuffix;
        #endregion
        public Form1()
        {
            InitializeComponent();
            fileWorker = new BackgroundWorker();
            fileWorker.DoWork += new DoWorkEventHandler(fileWorker_DoWork);
            fileWorker.ProgressChanged += new ProgressChangedEventHandler(fileWorker_ProgressChanged);
            fileWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(fileWorker_RunWorkerCompleted);
            fileWorker.WorkerReportsProgress = true;
            fileWorker.WorkerSupportsCancellation = true;

        }
        void fileWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
             if (e.Cancelled)
             {
                 lblResult.Text = "Cancelled";
             }

             if (e.Error != null)
             {
                 lblException.Text = e.Error.InnerException.ToString();
             }
             else
             {
                 lblResult.Text = "Complete...";
             }
             button1.Enabled = true;
             btnCancel.Visible = false;
        }
        void fileWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblResult.Text = "Processing...." + progressBar.Value.ToString() + "%";
        }
        void fileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(fileWorker.CancellationPending)
            {
                e.Cancel = true; ;
                fileWorker.ReportProgress(0);
                return;
            }

            try
            {
                if(rbFolderDay.Checked)
                {
                    folderMode = 1;
                }
                if(rbFolderMonth.Checked)
                {
                    folderMode=2;
                }
                fileCount = 0;
                exceptionCount = 0;
                GatherSelectedFileTypes();
                directory = directorySelection.Text;
                var filesInDir = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories).Where(s => acceptedFileTypes.Contains(Path.GetExtension(s).ToLower()));

                int countFiles = filesInDir.Count();
                if (countFiles > 0)
                {
                    string formattedDirecotry;
                    string outputcheck = outputDirectory.Text.Substring(outputDirectory.Text.Length - 1, 1);
                    if (outputcheck != @"\")
                    {
                        formattedDirecotry = outputDirectory.Text + @"\";
                    }
                    else
                    {
                        formattedDirecotry = outputDirectory.Text;
                    }
                    LoopFilesAndCopy(filesInDir, countFiles, formattedDirecotry);
                    fileWorker.ReportProgress(100);
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                fileWorker.ReportProgress(0);

            }
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            btnCancel.Enabled = true;
            fileWorker.RunWorkerAsync();
        }
        private void LoopFilesAndCopy(IEnumerable<string> filesInDir, int countFiles, string formattedDirecotry)
        {
            foreach (string file in filesInDir)
            {
                string fileTypeFromPath = "*"+ Path.GetExtension(file).ToLower();
                string fileCheck=  EstablishTypeOfFile(fileTypeFromPath);
                fileCount++;
                Int32 percent = (fileCount * 100) / countFiles;
                fileWorker.ReportProgress(percent);
                   
                try
                {
                    if (fileCheck == "image")
                    {
                        Image myImage = Image.FromFile(file);
                        if (myImage.PropertyIdList.Any(x => x == 36867))
                        {
                            PropertyItem propItem = myImage.GetPropertyItem(36867);
                            sdate = Encoding.UTF8.GetString(propItem.Value).Trim();
                        }
                        myImage.Dispose();
                    }

                    else
                    {
                        sdate = File.GetCreationTime(file).ToString();
                    }
                  
                    CalculateFolderName();
                    outputsuffix = Path.GetExtension(file);
                    newFileSerialised = dtaken.ToString().Replace(@"/", "").Replace(":", "").Replace(" ", "") + outputsuffix;
                    outputResult = formattedDirecotry + takenFolder + @"\" + newFileSerialised;
                    if (Directory.Exists(formattedDirecotry + takenFolder))
                    {
                        exist = File.Exists(outputResult);

                        if (exist)
                        {
                            if (!Directory.Exists(formattedDirecotry + @"\duplicates"))
                            {
                                Directory.CreateDirectory(formattedDirecotry + @"\duplicates");
                            }

                            outputResult = formattedDirecotry + @"\duplicates\" + Path.GetFileNameWithoutExtension(outputResult) + "_" + incrimentCounter + outputsuffix;
                            incrimentCounter++;
                        }
                        File.Copy(file, outputResult);
                    }
                    else
                    {
                        Directory.CreateDirectory(formattedDirecotry + takenFolder);
                        File.Copy(file, outputResult);
                    }
                }
                catch (Exception ex)
                {
                    ex.InnerException.ToString();
                    lblException.Text = ex.InnerException.ToString();
                }
            }
        }
        private string EstablishTypeOfFile(string fileTypeFromPath)
        {
            var fileType = SetupFileList();
            var currenttype = (from a in fileType
                               where a.Value.ToString() == fileTypeFromPath
                               select new
                               {
                                   a.Type
                               }).FirstOrDefault();
            return currenttype.Type.ToString();
        }
        private void CalculateFolderName()
        {
            secondhalf = sdate.Substring(sdate.IndexOf(" "), (sdate.Length - sdate.IndexOf(" ")));
            firsthalf = sdate.Substring(0, 10);
            firsthalf = firsthalf.Replace(":", "-");
            sdate = firsthalf + secondhalf;
            dtaken = DateTime.Parse(sdate);
            takenMonth = dtaken.ToString("MMM", CultureInfo.InvariantCulture);
            takenYear = dtaken.Year.ToString();
            if (folderMode == 2)
            {
                takenFolder = takenMonth + " " + takenYear;
            }
            if(folderMode ==1)
            {
                takenFolder = dtaken.ToShortDateString().Replace("/","");
            }
            else
            {
                takenFolder = "images";
            }
        }
        private void GatherSelectedFileTypes()
        {
            acceptedFileTypes = null;
            foreach (var item in chkFileTypes.CheckedItems.OfType<FileTypesIncluded>())
            {
                acceptedFileTypes = acceptedFileTypes + item.Value.ToString()  + ",";
            }
        }
        public string incrimentCount(string outputDirectory, string outputFile, string outputResult, int incriment)
        {
            outputsuffix = Path.GetExtension(outputFile);
            if (File.Exists(outputResult))
            {
           //     string incrimenting = Path.GetFileNameWithoutExtension(outputFile);
           //     string lastFile = incrimenting.Substring(incrimenting.Length - 1, 1);
           //     int incrimentValue = int.Parse(lastFile) + 1;


   //             string checkFileNumber = Path.GetFileNameWithoutExtension(outputFile);
     //           incrimentCounter = Directory.EnumerateFiles(outputDirectory + @"\"+ takenFolder, "*"  +  checkFileNumber + "*").Count();
            //    incrimentCounter++;
                outputResult = outputDirectory + @"\duplicates\" + Path.GetFileNameWithoutExtension(outputFile) + "_" + incriment + outputsuffix;
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
            List<FileTypesIncluded> list = SetupFileList();
            BindCheckGrid(list);
            SetupFileTypeSelection();
            lblException.Text = "";
            lblResult.Text = "";
        }
        private void BindCheckGrid(List<FileTypesIncluded> list)
        {
            chkFileTypes.DataSource = list;
            ((ListBox)this.chkFileTypes).DataSource = list;
            ((ListBox)this.chkFileTypes).DisplayMember = "Text";
            ((ListBox)this.chkFileTypes).ValueMember = "Value";


        }
        private void SetupFileTypeSelection()
        {
            chkFileTypes.Visible = false;
            for (int x = 0; x < chkFileTypes.Items.Count; x++)
            {
                chkFileTypes.SetItemChecked(x, true);
            }
            chkFileTypes.Visible = true;
        }
        private List<FileTypesIncluded> SetupFileList()
        {
            var listCollection = new List<FileTypesIncluded>();
            listCollection.Add(new FileTypesIncluded { Text = "JPG", Value = "*.jpg", Type="image" });
            listCollection.Add(new FileTypesIncluded { Text = "PNG", Value = "*.png", Type="image" });
            listCollection.Add(new FileTypesIncluded { Text = "GIF", Value = "*.gif", Type="image"});
            listCollection.Add(new FileTypesIncluded { Text = "BMP", Value = "*.bmp", Type = "image" });
            listCollection.Add(new FileTypesIncluded { Text = "AVI", Value = "*.avi", Type = "video" });
            listCollection.Add(new FileTypesIncluded { Text = "MPEG", Value = "*.mpg", Type = "video" });
            listCollection.Add(new FileTypesIncluded { Text = "MP4", Value = "*.mp4", Type = "video" });
            listCollection.Add(new FileTypesIncluded { Text = "TIFF", Value = "*.tiff", Type = "image" });
            listCollection.Add(new FileTypesIncluded { Text = "PSD", Value = "*.psd", Type = "image" });
            listCollection.Add(new FileTypesIncluded { Text = "JPEG", Value = "*.jpeg", Type = "image" });
            listCollection.Add(new FileTypesIncluded { Text = "TIF", Value = "*.tif", Type = "image" });
            return listCollection;
        }
        public class FileTypesIncluded
        {
           public string Text { get; set; }
           public object Value { get; set; }
           public string Type { get; set; }
        }

        private void directorySelection_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
