# Project generator for Unity3D projects 
This repo creates new pre-configured git repo with empty unity project.

# Important:
This project required latest (at moment) versions of git (min 2.9.0) and git-lfs (min 1.4.1)
Consider to install/upgrade your binaries
Mac users can install from Homebrew with:
 brew install git-lfs
or from MacPorts with: 
 port install git-lfs
Windows users can install from Chocolatey with:
 choco install git-lfs.

# Usage: 
./create_new_project.command ostype [options: 'mac' 'windows'] projectname 

Note: Use GitBash console at Windows machine it goes with last version of git.

E.g. if you want to create new project PacMan, call for windows:
./create_new_project.command windows PacMan
for Mac
./create_new_project.command mac PacMan

# Result:
New .git repo, configured to work with git-lfs and Unity.
It contains git-hooks and useful scripts, git configs which solve certain 'git vs Unity' problems.
Also it contains Unity project structure which is recomended by 'Saritasa Unity guidelines and best practicies'.
