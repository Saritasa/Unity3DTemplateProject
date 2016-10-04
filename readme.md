# Project generator for Unity3D projects 
Unity3D project scaffold. Uses cookiecutter (https://github.com/audreyr/cookiecutter) to be able to create project with required pre-config. Possible options are described in cookiecutter.json. 

# Setup instructions:
This project requires following binaries: git (min 2.9.0), git-lfs (min 1.4.1) and Unity. Consider to install/upgrade your binaries if your git version, git lfs version return values below required. This project also requires bash, but it is assumed that GitBash is avaiable in system when git 2.9.0+ is installed.

Mac users can install from Homebrew with:
```
brew install git-lfs
```
or from MacPorts with: 
```
port install git-lfs
```
Windows users can install from Chocolatey with:
```
choco install git-lfs
```

In order to create new project it is required to install python and cookiecutter. Here is script which installs python 3.5.2 and latest version of cookiecutter:
```
PS> .\InstallCookiecutterWin.ps1
```
Consider to add [Python install path]\Scripts to PATH.

# Usage:
```
cookiecutter https://github.com/Saritasa/Unity3DTemplateProject.git
```
# Result:
New .git repo, configured to work with Unity and git-lfs (optional). It contains git-hooks and useful scripts, git configs which solve certain 'git vs Unity' problems. Also it contains Unity project structure which is recomended by 'Saritasa Unity guidelines and best practicies'.
