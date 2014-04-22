using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtility.Data.Repository
{
   public class FileUtilityRepository :baseRepository, IFileUtilityRepository
    {

       public List<FileTypesIncluded> SetupFileList()
       {
           var listCollection = new List<FileTypesIncluded>();
           listCollection.Add(new FileTypesIncluded { Text = "exe", Value = "*.exe", Type = "program" });
           listCollection.Add(new FileTypesIncluded { Text = "PNG", Value = "*.png", Type = "image" });
           listCollection.Add(new FileTypesIncluded { Text = "GIF", Value = "*.gif", Type = "image" });
           listCollection.Add(new FileTypesIncluded { Text = "BMP", Value = "*.bmp", Type = "image" });
           listCollection.Add(new FileTypesIncluded { Text = "AVI", Value = "*.avi", Type = "video" });
           listCollection.Add(new FileTypesIncluded { Text = "MPEG", Value = "*.mpg", Type = "video" });
           listCollection.Add(new FileTypesIncluded { Text = "MP4", Value = "*.mp4", Type = "video" });
           listCollection.Add(new FileTypesIncluded { Text = "TIFF", Value = "*.tiff", Type = "image" });
           listCollection.Add(new FileTypesIncluded { Text = "PSD", Value = "*.psd", Type = "editedImage" });
           listCollection.Add(new FileTypesIncluded { Text = "JPEG", Value = "*.jpeg", Type = "image" });
           listCollection.Add(new FileTypesIncluded { Text = "3GP", Value = "*.3gp", Type = "video" });
           listCollection.Add(new FileTypesIncluded { Text = "WAV", Value = "*.wav", Type = "sound" });
           listCollection.Add(new FileTypesIncluded { Text = "MP3", Value = "*.mp3", Type = "sound" });
           listCollection.Add(new FileTypesIncluded { Text = "TIF", Value = "*.tif", Type = "image" });
           listCollection.Add(new FileTypesIncluded { Text = "AMR", Value = "*.amr", Type = "video" });
           return listCollection;
       }


    }

    public interface IFileUtilityRepository
    {
        List<FileTypesIncluded> SetupFileList();

    }
}
