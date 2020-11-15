using Sorting.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.Helpers
{
    public class FilesHelper
    {
        private string ResultDirectory { get; } = @"\SortingResults";
        public FilesHelper() { }

        // Return single record if passed ID exits
        public string GetRecord(int pID)
        {
            if (!Directory.Exists(ResultDirectory)) throw new FileNotFoundException("There are no sorting results present.");
            
            FileInfo[] vFiles = GetFiles(pID);

            if (vFiles.Length == 0) throw new IndexOutOfRangeException("There is no records with pesented ID.");

            _ = int.TryParse(vFiles[0].Name.Replace(vFiles[0].Extension, ""), out int vFileID);
            SortedData vDataEntry = new SortedData
            {
                ID = vFileID,
                SortDate = vFiles[0].LastWriteTime
            };

            return vDataEntry.ToString() +
                    Environment.NewLine + "Result" +
                    Environment.NewLine +  File.ReadAllText(vFiles[0].FullName);
            
        }
        // Get all recorded sorting operations if any exists in format
        // Id: 1; Sorted Date: yyyy/MM/dd HH:mm:ss;
        public string GetAllRecordsList()
        {
            if (!Directory.Exists(ResultDirectory)) throw new FileNotFoundException("There are no sorting results present.");

            FileInfo[] vFiles = GetFiles();

            if (vFiles.Length == 0) throw new FileNotFoundException("There are no sorting results present.");

            List<SortedData> vRecords = new List<SortedData>();
            foreach (FileInfo vFile in vFiles)
            {
                bool vSuccess = int.TryParse(vFile.Name.Replace(vFile.Extension, ""), out int vFileID);
                if (vSuccess)
                {
                    SortedData vDataEntry = new SortedData
                    {
                        ID = vFileID,
                        SortDate = vFile.LastWriteTime
                    };
                    vRecords.Add(vDataEntry);
                }
            }

            return "Available results lists IDs:" +
                Environment.NewLine + string.Join(", " + Environment.NewLine, vRecords.OrderBy(r => r.ID));
        }

        // Create directory if doesn't exit
        // Save data to txt file in format "1 2 3 4 5"
        // Return created file id
        public string SaveSortedData(int[] pDataToSave)
        {
            try 
            {
                if (!Directory.Exists(ResultDirectory)) Directory.CreateDirectory(ResultDirectory);

                int vNewFileID = GetLatestRecordID() + 1;
                string vFileName = Path.Combine(ResultDirectory + @"\" + vNewFileID + ".txt");
                using (FileStream vFs = new FileStream(vFileName, FileMode.CreateNew))
                {
                    string vStringToSave = string.Join(" ", pDataToSave);
                    using (TextWriter vSw = new StreamWriter(vFs))
                    {
                        vSw.Write(vStringToSave);
                    }
                }
                return "Data sorted and saved." +
                    Environment.NewLine + "FileID: " + vNewFileID +
                    Environment.NewLine + "Result: " +
                    Environment.NewLine + string.Join(", ", pDataToSave);
            }
            catch (Exception)
            {
                throw new Exception("Saving data to file failed.");
            }
        }

        public string GetLatestRecord()
        {
            return GetRecord(GetLatestRecordID());
        }

        // Removes record if exists
        public string DeleteFile(int pID)
        {
            try
            {
                if (!Directory.Exists(ResultDirectory)) throw new FileNotFoundException("There are no sorting results present.");

                FileInfo[] vFiles = GetFiles(pID);

                if (vFiles.Length == 0) throw new IndexOutOfRangeException("There is no records with pesented ID.");

                vFiles[0].Delete();

                return string.Format("Record ID: '{0}' has been removed", pID);
            }
            catch (Exception)
            {
                throw new Exception ("Error occurred during record removal");
            } 
        }

        public string DeleteAllFiles()
        {
            try
            {
                if (!Directory.Exists(ResultDirectory)) throw new FileNotFoundException("There are no sorting results present.");

                FileInfo[] vFiles = GetFiles();

                if (vFiles.Length == 0) throw new IndexOutOfRangeException("There is no records to remove.");

                foreach (FileInfo vFile in vFiles)
                {
                    vFile.Delete();
                }

                return "All files have been removed succesfully.";
            }
            catch (Exception)
            {
                throw new Exception("Error occurred during record removal");
            }
        }

        private FileInfo[] GetFiles()
        {
            DirectoryInfo vDirectoryInfo = new DirectoryInfo(ResultDirectory);
            FileInfo[] vFiles = vDirectoryInfo.GetFiles("*.txt");
            return vFiles;
        }

        private FileInfo[] GetFiles(int pID)
        {
            DirectoryInfo vDirectoryInfo = new DirectoryInfo(ResultDirectory);
            FileInfo[] vFiles = vDirectoryInfo.GetFiles(pID + ".txt");
            return vFiles;
        }

        // If directordy doesn't exists, return 0
        // If directory doesn't contain any files, return 0
        // Else return latest FileID + 1
        private int GetLatestRecordID()
        {
            if (!Directory.Exists(ResultDirectory))
            {
                Directory.CreateDirectory(ResultDirectory);
                return 0;
            }
            FileInfo[] vFiles = GetFiles();

            if (vFiles.Length == 0) return 0;

            List<int> vFileNames = new List<int>();
            foreach (FileInfo vFile in vFiles)
            {
                bool vSuccess = int.TryParse(vFile.Name.Replace(vFile.Extension, ""), out int vFileID);
                if (vSuccess) vFileNames.Add(vFileID);
            }
            return vFileNames.Max();
        }
    }
}
