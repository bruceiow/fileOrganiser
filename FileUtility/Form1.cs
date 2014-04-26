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
                string fileCheck = "";
                if ((fileTypeFromPath == "*.jpg") || (fileTypeFromPath == "*.gif") || (fileTypeFromPath == "*.jpeg") || (fileTypeFromPath == "*.tiff"))
                {
                    fileCheck = "image"; 
                }
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
                try
                {
                    List<FileTypesIncluded> fileType = new List<FileTypesIncluded>();
                    Stack<DirectoryInfo> dirstack = new Stack<DirectoryInfo>();
                    dirstack.Push(new DirectoryInfo(dlgSourceBrowser.SelectedPath));
                    while (dirstack.Count > 0)
                    {
                        DirectoryInfo current = dirstack.Pop();
                        foreach (DirectoryInfo d in current.GetDirectories())
                        {
                            if ((d.Attributes & FileAttributes.System) != FileAttributes.System)
                            {
                                dirstack.Push(d);
                            }
                        }

                        foreach (FileInfo f in current.GetFiles())
                        {
                            string fileTypeFromPath = "*" + f.Extension.ToLower();
                            int checkTypeInList = fileType.Where(i => i.Type == fileTypeFromPath).Count();
                            if (checkTypeInList == 0)
                            {
                                fileType.Add(new FileTypesIncluded { Type = fileTypeFromPath, Text = fileTypeFromPath.Replace("*.", "").ToUpper(), Value = fileTypeFromPath });
                            }

                            pnlProcessControls.Visible = true;
                            this.Height = 461;
                        }
                    }

                    chkFileTypes.DataSource = fileType;
                    ((ListBox)this.chkFileTypes).DataSource = chkFileTypes.DataSource = fileType;
                    ((ListBox)this.chkFileTypes).DisplayMember = "Text";
                    ((ListBox)this.chkFileTypes).ValueMember = "Value";
                    SetupFileTypeSelection();
                }
                catch (UnauthorizedAccessException)
                {
                }
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
            this.Height = 250;
            List<FileTypesIncluded> list = SetupFileList();
            BindCheckGrid(list);
            SetupFileTypeSelection();
            lblException.Text = "";
            lblResult.Text = "";
        }
        private void BindCheckGrid(List<FileTypesIncluded> list)
        {

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
            foreach (var item in chkFileTypes.CheckedItems.OfType<FileTypesIncluded>())
            {
                listCollection.Add(new FileTypesIncluded { Text = item.Text, Type = item.Type, Value = item.Value });
            }            
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
