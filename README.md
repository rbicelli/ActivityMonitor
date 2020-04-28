[![Buy Me A Coffee](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/rbicelli)
 
# Activity Monitor

This is the reimplementation in Windows.Forms of [Personal Activity Monitor](https://archive.codeplex.com/?p=activitymonitor), originally written by Arkadiusz Benedykt

## Features

- Monitors user's applications usage and idle time.
- Can save Monitoring Data to a SQL Server Database.
- Settings are manageable via Group Policy (ADMX Templates included).

## Installation

You can find the compiled version in [releases](https://github.com/rbicelli/ActivityMonitor/releases)

## SQL Server Database Setup

Create Schema objects with the provided sql script.

## Silent Installation

Execute the setup package with following flags:

ActivityMonitor-%Version%-Setup.exe /VERYSILENT /SUPPRESSMSGBOXES /NORESTART

## Requirements

ActMon is written in C#.
Requires .NET Framework 4.0

## License

ActMon is released under the MIT License
The original Work of Arkadius Benedikt is released under the MS-PL License

## Why Windows.Forms instead of WPF?

I noticed that WPF requires more RAM and I needed to use this application in an terminal server environment, so I saved 10 MB of RAM on each instance.