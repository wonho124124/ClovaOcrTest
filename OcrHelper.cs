using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClovaOcrTest
{
    public static class Helper
    {
        public static string ImageToBase64(string srcPath)
        {
            string result = string.Empty;
            try
            {
                byte[] imageByte = File.ReadAllBytes(srcPath);
                result = Convert.ToBase64String(imageByte);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetImageTempPath(string filePath)
        {
            string tmpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OCRTemp", DateTime.Now.ToString("yyyyMMddHHmmssfff_") + Path.GetFileNameWithoutExtension(filePath) + Path.GetExtension(filePath));
            if (Directory.Exists(Path.GetDirectoryName(tmpPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(tmpPath));
            }
            return tmpPath;
        }
        public static string StringtoBase64(string str)
        {
            string result = string.Empty;
            try
            {
                byte[] baseEncodedResult = Encoding.UTF8.GetBytes(str);
                result = Convert.ToBase64String(baseEncodedResult);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
