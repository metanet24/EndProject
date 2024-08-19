using MBEAUTY.Services.Interfaces;

namespace MBEAUTY.Services.Implementations
{
    public class FileService : IFileService
    {
        public string ReadFile(string path, string file)
        {
            using (StreamReader reader = new(file)) { file = reader.ReadToEnd(); }

            return file;
        }
    }
}
