namespace Company.G01.PL.Helpers
{
    public static class DocumentSettings
    {
        // 2-Function (Upload---Delete) ---->Static

        public static string UploadFile(IFormFile file, string FolderName)
        {
            // file path
            //1- locatin
            //2-file name

            //- locatin
            //1- Static
            //var folderpath = "F:\\MOHAMED\\route\\ASP.NET.MVC\\sec03\\Company.G01\\Company.G01.PL\\wwwroot\\fiels\\" + FolderName;

            //2
            //var folderpath = Directory.GetCurrentDirectory() + "\\wwwroot\\fiels\\" + FolderName;

            //3 Path >>>>Built In Class 
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\fiels", FolderName);


            ///2>>File Name and get it uniqu by using (Guid())
            
            var FileName=$"{Guid.NewGuid()}{file.FileName}";

            //To Git File Path (Combine (FolderPath+FileName))

            var FilePath=Path.Combine(FolderPath, FileName);

            //Put It In FileStream And +FileMode) >>>Built In Struct

           using var FileStream=new FileStream(FilePath,FileMode.Create);

            file.CopyTo(FileStream);  // make copy from filestream to file in parameter

            return FileName;
            


        } 


        public static void DeleteFile(string FileName,string FolderName)
        {
            var FilePath=Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\fiels",FolderName,FileName);

            if(File.Exists(FilePath))   //File>.Built In Class
                File.Delete(FilePath);
                
        }


    }
}
