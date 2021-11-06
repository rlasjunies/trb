# TEAMS Randomized Background

'TEAMS Randomized Background' (TRB) replaces every hours the background image used in TEAMS video camera.

## How does it work?

TRB replaces the image named `TEAMSRB.png` in the TEAMS backgrounds folder `%appdata%\Microsoft\Teams\Backgrounds\Uploads` by a random image file find in a folder repository of images. 

`TEAMSRB.png` being previously selected as the background image of the video camera in TEAMS.

Each time you activate your camera, TEAMS will show `TEAMSRB.png` who is the latest randomized image overridden.

> Obviously TRB is heavily configurable to fit your environment;folders, file name, duration, filtering ...

## Get Started

1. build trb
1. run trb
1. configure the background the file name of the 

### Build & Run TRB

> TRB requires .net6 framework

use the `*.bat` files proposed to easy these operations:
* Build the program, type: `.\build.bat`
* Run the program, type: `.\run.bat -s <your folder with background images>`

### Configure TEAMS: select `TEAMSRB.png` as background image

1. Run trb 
to create a file `TEAMSRB.png` in the TEAMS background image folder `%appdata%\Microsoft\Teams\Backgrounds\Uploads`
> Assuming you have images in the folder setted as parameter
2. Open TEAMS
1. Start a meeting
1. Select 'Apply Background Effect', from 'more actions' menu
1. Found and Select the image just uploaded `TEAMSRB.png`

### TRB parameters

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
START /min .\run.bat -s "c:\_local\TEAMS Background"
```

## TODO

* [ ] ? Generate thumbnail in the upload folder ?
* [ ] Support natively bing background folder
* [ ] run TRB as a service?
* [ ] run TRB in the systray?