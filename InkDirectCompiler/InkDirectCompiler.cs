using Ink;
using Ink.Runtime;
using Path = System.IO.Path;

namespace InkDirectCompiler;

public class InkDirectCompiler
{
    public static string GetStoryJson(string pathToInkFile)
    {
        string inputString = SetupWorkDirectory(pathToInkFile);
        Compiler compiler = new Compiler(inputString);
        Story story = compiler.Compile();
                
        string? result = story.ToJson();

        return result;
    }

    static string SetupWorkDirectory(string pathToInkFile)
    {
        string workingDirectory = Directory.GetCurrentDirectory();
                
        string fullFilename = pathToInkFile;
        if(!Path.IsPathRooted(fullFilename)) {
            fullFilename = Path.Combine(workingDirectory, fullFilename);
        }

        // Make the working directory the directory for the root ink file,
        // so that relative paths for INCLUDE files are correct.
        workingDirectory = Path.GetDirectoryName(fullFilename);
        Directory.SetCurrentDirectory(workingDirectory);

        var inputString = File.ReadAllText(pathToInkFile);
        return inputString;
    }

}
