namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class TaskFilesDto
{
    public int Id { get; set; }
    
    public string IdNumber { get; set; }
    public List<TaskFile> TasksFiles { get; set; }
    
    public bool OutputFilesAsZipDownloadable { get; set; }

    public class TaskFile
    {
        public string Id { get; set; }
        public string IdNumber { get; set; }
        public LanguageCombination LanguageCombination { get; set; }
        public FileGroup InputWorkfiles { get; set; }
        public FileGroup InputResources { get; set; }
        public FileGroup? Output { get; set; }
        public bool OutputFilesAsZipDownloadable { get; set; }
    }

    public class LanguageCombination
    {
        public Language SourceLanguage { get; set; }
        public Language TargetLanguage { get; set; }
    }

    public class Language
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class FileGroup
    {
        public string Name { get; set; }
        public List<Directory> Directories { get; set; }
        public List<File> Files { get; set; }
    }

    public class Directory
    {
        public string Name { get; set; }
        public List<Directory> Directories { get; set; }
        public List<File> Files { get; set; }
    }

    public class File
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool Downloadable { get; set; }
        public string Category { get; set; }
        public bool Zip { get; set; }
        public FileStats FileStats { get; set; }
    }

    public class FileStats
    {
        public int CharactersWithSpaces { get; set; }
        public int CharactersWithoutSpaces { get; set; }
        public int Words { get; set; }
        public int Lines { get; set; }
        public int Pages { get; set; }
    }
}