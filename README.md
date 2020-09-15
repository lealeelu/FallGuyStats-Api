# FallGuyStats-Api
![OBS window. Stats showing on the upper left corner of the screen while playing fall guys.](https://github.com/lealeelu/FallGuyStats-Api/blob/media/StatsExample.png)
Fall Guy Stats App written in ASP.NET, C#, with EFCore for a backend and Angular for frontend.
It watches the log file as you play for a DTO output in your player.log file, captures that DTO and adds it to the SQLite db.
Has endpoints for Episodes, Rounds, and Stats.

# Getting Started
1. Download the [latest relase](https://github.com/lealeelu/FallGuyStats-Api/releases) FallGuyStats_v.x.xx.zip file under assets.
2. Unzip the file to somewhere you'll remember it.
3. Inside the zip folder, double click on FallGuyStats.exe.
4. Open up a browser and navigate to http://localhost:5000
5. If all looks good there, open OBS and add a browser source. Change the URL to the one above. You should see the stat tracker show up on your obs capture screen.
6. Add custom css if you want it.
7. In OBS, go to File->Settings->Advanced-> Uncheck "Enable Browser Source Hardware Acceleration".
8. Play Fall guys and watch the stats rack up!

# Need help?
Tweet or message me on [twitter](https://twitter.com/lealeelu) or create a bug in the issues section if you found something that isn't working as it should.

# Contributing to FallGuyStats-Api
### Are you a developer?
- When you encounter bugs, get as many details as you can and add it to a [new issue](https://github.com/lealeelu/FallGuyStats-Api/issues/new/choose), and tag it as a bug.
- If you want to add a new feature, I would love to see your contribution. Fork the repo, make your changes, test those changes and make a pull request.
- Add documentation to the Wiki. I <3 documentation.

### Not a developer?
- Tweet about the tool and link to my Repo. Soon there will be videos explaining how to use this tool.
- [Gimme money on Ko-fi](https://ko-fi.com/lealeelu)! In your donation message, let me know what feature you would like to add and that feature might become more important to me >_>
