# TEAMS Randomized Background

'TEAMS Randomized Background' (TRB) replaces every hours the background image of TEAMS.

## How does it work?

TRB replaces the image named `TEAMSRB.png` in the TEAMS backgrounds folder `%appdata%\Microsoft\Teams\Backgrounds\Uploads` by a random file from the folder name passed as parameter `-s` or current folder `\.` by default.

`TEAMSRB.png` being previously selected as the background image of TEAMS.

Each time you activate you camera, TEAMS will show the last randomized image, override on the file name `TEAMSRB.png`.

> Obviously TRB is heavily configurable to fit your environment;folders, file name, duration, filtering ...

## Get Started

> Read the following the chapter to then end for a perfect understanding (5 min long)

### Configure your TEAMS

1. Run trb  (see below Buil & Run TRB)
This will create a file `TEAMSRB.png` in the `%appdata%\Microsoft\Teams\Backgrounds\Uploads`
> Assuming you have images in the folder setted as parameter
2. Open TEAMS
1. Start a meeting
1. Select 'Apply Background Effect', from 'more actions' menu
1. Found and Select the image just uploaded `TEAMSRB.png`

### Build & Run TRB

> TRB requires .net6 framework

Bat files proposed to easy these operations:
* Build the program, type: `.\build.bat`
* Run the program, type: `.\run.bat`

### Configure TRB

If you already have run the program and you may have seen `There is no images in the folder: "." ....`

Type `.\run.bat --help` to have the details at any time:


* -f, --filter :  
(Default: ) String pattern to filter the images from the source folder. This string should be present in the filename. For example: '--filter Corporate*' will select only the images with the substring 'Corporate' in the file name. The filters are case sensitive
* -t, --teams-background-folder  
(Default: %appdata%\Microsoft\Teams\Backgrounds\Uploads) Override the system TEAMS background folder.

* -s, --source-image-folder  
(Default: .) Folder with the source images. By default it's the current folder

* -n, --name-of-the-image  
(Default: TEAMSRB.png) The file name to be replaced in the TEAMS background folder

  -d, --duration  
(Default: 60) Frequency of replacement of the background in minutes.

  -v, --verbose  
Set output to verbose messages.

  --help  
Display this help screen.

  --version  
Display version information.

### Run TRB Starting Windows

1. Goes in the folder `C:\Users\<... your user ...> \AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup`
1. Create a bat file like this one below. ie: `TEAMSRandomizeBAckground.bat`.
In this bat file:
    * Change directory to your local repo, 
    * run the trb with the image source folder expected

As example:
```dos 
cd C:\_local\Github\trb
.\run.bat -s "c:\_local\TEAMS Background"
```

## TODO

* [ ] ? Generate thumbnail in the upload folder ?
* [ ] Support natively bing background folder