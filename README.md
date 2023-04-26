A quickly put together windows version of warpd https://github.com/rvaiya/warpd
This allows you to teleport your mouse to a new location by typing two letters.

## Installation
Download the exe from the releases page.

## Usage
When launched a grid of letters will be shown.
Type these letters, and the mouse will be teleported there.
Adjust the postion with up down left right or hjkl
Enter performs a left click
Escape at anytime resets mouse to original location.

## How I use it
Place the keypress.exe file in your path
Use autohotkey to launch it https://www.autohotkey.com/
I use Win + j


## Development
I am a vim user so this was created without visual studio project files etc

Compilation
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /target:winexe .\keypress.cs
```
