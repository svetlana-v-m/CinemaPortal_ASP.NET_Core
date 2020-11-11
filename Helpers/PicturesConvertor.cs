using System.IO;

namespace CinemaPortal_ASP.NET_Core.Helpers
{
    public static class PicturesConvertor
    {
            public static byte[] GetFileBytes(string path, string appRootPath)
            {
                string pth = appRootPath + path;
                FileStream fileOnDisk = new FileStream(pth, FileMode.Open);
                byte[] fileBytes;
                using (BinaryReader br = new BinaryReader(fileOnDisk))
                {
                    fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                }
                return fileBytes;
            }
        
    }
}
