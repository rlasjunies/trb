using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using CommandLine;
using CommandLine.Text;

//ParserResult<Configuration> ParserResult;

var parserResult = Parser.Default.ParseArguments<Configuration>(args);

parserResult
    .WithParsed<Configuration>(Run)
    .WithNotParsed<Configuration>(HandleParseError);

void Run(Configuration conf)
{
    conf = ReplaceTEAMSFolderConstantBySystemVariableIfNotOverridden(conf);
    if (conf.Verbose)
    {
        Console.WriteLine($"Verbose output enabled. Current Arguments: -v {conf.Verbose}");
    }

    // Timer object that will call Background replacement
    var _timer = new Timer(ReplaceTEAMSBackground, conf, 0, conf.duration * 1000 * 60);
    if (conf.Verbose)
    {
        Console.WriteLine($"Background will be reset every:{conf.duration} min");
    }

    // Wait for the user to hit <Enter>
    Console.ReadLine();

    void ReplaceTEAMSBackground(Object? o)
    {
        var conf = o as Configuration ?? new Configuration();

        var TEAMSBackgroundTargetFileName = Path.Combine(conf.TEAMSBackgroundFolder, conf.TEAMSBackgroundFileName);
        if (conf.Verbose)
        {
            Console.WriteLine($"TEAMS background folder:\"{TEAMSBackgroundTargetFileName}\"");
        }

        var SourceFileNames = RetrieveSourceImages(conf.SourceImageFolder, conf.ImagePattern);
        if (conf.Verbose)
        {
            Console.WriteLine($"source folder:\"{conf.SourceImageFolder}\"");
            Console.WriteLine($"file pattern:\"{conf.ImagePattern}\"");
            ListAllImagesFilteredFromSourceFolder(SourceFileNames);
        }

        if (SourceFileNames.Count() == 0)
        {
            Console.WriteLine($"No image found in the source folder:\"{conf.SourceImageFolder}\"");
            Console.WriteLine($"You may need to set the folder name with your image. Have a look in the parameter -s.");
            Console.WriteLine( GetHelp<Configuration>(parserResult));
            Environment.Exit(0);
        }

        var newBgFileName = SelectImageRandomly(SourceFileNames);
        if (conf.Verbose)
        {
            Console.WriteLine($"New background image file name:\"{newBgFileName}\"");
        }

        try
        {
            File.Copy(newBgFileName, TEAMSBackgroundTargetFileName, true);
            Console.WriteLine($"{DateTime.Now} - replace TEAMS background:\"{TEAMSBackgroundTargetFileName}\" by \"{newBgFileName}\" ");
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"{DateTime.Now} fail replacing the TEAMS background file:\"{conf.TEAMSBackgroundFileName}\" by \"{newBgFileName}\" \n with the message:{e.Message}");
        }

        ////////////////
        // Local function
        ////////////////
        string[] RetrieveSourceImages(string folder, string filePattern)
        {
            var listOfExtensions = new List<string> { ".png", "jpg", "jpeg", "bmp" };
            return Directory
                .GetFiles(folder, filePattern, SearchOption.TopDirectoryOnly)       // retrieve the files in the folder
                .Where(file => listOfExtensions.Any(ext => file.EndsWith(ext)))    // filter image based on file name extensions
                .ToArray();

            // May be interesting to filter also based on the size of the image
            // TEAMS supports:
            //      Min: 360 x 360
            //      Max: 2048 w 2048
            // aspect ratio greater than 4 ???! 

        }

        string SelectImageRandomly(string[] files)
        {
            var rnd = new Random();
            var newBackgroundIndex = rnd.Next(files.Count());
            return files[newBackgroundIndex];
        }

        void ListAllImagesFilteredFromSourceFolder(string[] sourceFileNames)
        {
            foreach (var file in sourceFileNames)
            {
                Console.WriteLine($"file:{file}");
            }
        }

        string GetHelp<T>(ParserResult<T> result)
        {
            return HelpText.AutoBuild(result, h => h, e => e);
        }

    }

    Configuration ReplaceTEAMSFolderConstantBySystemVariableIfNotOverridden(Configuration conf)
    {
        if (conf.TEAMSBackgroundFolder == @"%appdata%\Microsoft\Teams\Backgrounds\Uploads")
        {
            conf.TEAMSBackgroundFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Teams\Backgrounds\Uploads");
        }
        return conf;
    }
}

void HandleParseError(IEnumerable<Error> errs)
{
    if (errs.IsVersion())
    {
        Console.WriteLine("Version Request");
        return;
    }

    if (errs.IsHelp())
    {
        Console.WriteLine("Help Request");
        return;
    }
    Console.WriteLine("Parser Fail");
}

class Configuration
{

    // Windows Bing Wall paper background folder
    // %UserProfile%\AppData\Local\Microsoft\BingWallpaperApp\WPImages 

    [Option('f'
            , "filter"
            , Default = ""
            , Required = false
            , HelpText = "String pattern to filter the images from the source folder. This string should be present in the filename. For example: '--filter Corporate*' will select only the images with the substring 'Corporate' in the file name. The filters are case sensitive")]
    public string ImagePattern { get; set; } = "";

    [Option('t'
            , "teams-background-folder"
            , Default = @"%appdata%\Microsoft\Teams\Backgrounds\Uploads"
            , Required = false
            , HelpText = "Override the system TEAMS background folder.")]
    public string TEAMSBackgroundFolder { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Teams\Backgrounds");


    [Option('s'
            , "source-image-folder"
            , Default = "."
            , Required = false
            , HelpText = "Folder with the source images. By default it's the current folder")]
    public string SourceImageFolder { get; set; } = "";

    [Option('n'
            , "name-of-the-image"
            , Default = "TEAMSRB.png"
            , Required = false
            , HelpText = "The file name to be replaced in the TEAMS background folder")]
    public string TEAMSBackgroundFileName { get; set; } = "";

    [Option('d'
            , "duration"
            , Default = 60
            , Required = false
            , HelpText = "Frequency of replacement of the background in minutes.")]
    public int duration { get; set; }

    [Option('v'
            , "verbose"
            , Required = false
            , HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }

}