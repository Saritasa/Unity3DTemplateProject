# Project generator for Unity3D projects 
Unity3D project scaffold. Uses cookiecutter (https://github.com/audreyr/cookiecutter) to be able to create project with required pre-config. Possible options are described in cookiecutter.json. 

# Requirements
This project requires following binaries: 

* git (min 2.9.0)
* git-lfs (min 1.4.1) [optional]
* Unity 5.4+
* python (tested with versions 3.5.2 and 2.7.12)
* pip (goes with python usualy)
* cookiecutter
* bash (for windows, shipped with git)

**It is assumed that this repo and final Unity project will be used in case insensitive filesystem.**

**Attention**:
Source tree for Mac, by default, uses emdedded versions of git and git-lfs which are older than reqiured. Install last versions and switch to 'Use system ...' settings in SourceTree -> Preferences -> Git:
* ![Hint for SourceTree on Mac](https://raw.githubusercontent.com/Saritasa/Unity3DTemplateProject/master/images/use-system-git.png "Hint for SourceTree on Mac")

# Setup instructions (Mac)
Exclude steps if you already have certain component. Or update it, see: ```brew upgrade ...```
It is assumed that http://brew.sh/ is installed. (TODO: Check with MacPorts)

```
brew install git
brew install git-lfs
brew install python
pip install cookiecutter
```

In case if python is already installed to system:
```
# the only that you need -- cookiecutter
pip install cookiecutter
# or if you don't have pip use:
easy_install cookiecutter
# or 
sudo easy_install cookiecutter
```

# Setup instructions (Windows)

Exclude steps if you already have certain component. Or update it, see: ```choco upgrade ...```

(Use PowerShell)

```
choco install git
choco install git-lfs
```
Python package for chocolatey is broken at moment, please use following script for installation of python and cookiecutter:
```
iwr https://raw.githubusercontent.com/Saritasa/Unity3DTemplateProject/master/InstallCookiecutterWin.ps1 -OutFile InstallCookiecutterWin.ps1
.\InstallCookiecutterWin.ps1
rm InstallCookiecutterWin.ps1
```
This script will warns you if you already have python. And suggests options.

# Usage
```
cookiecutter https://github.com/Saritasa/Unity3DTemplateProject.git
```

Use following command in order to add remote to your repo:
```
git remote add origin https://path.to.repo.com
```

# Result
New .git repo, configured to work with Unity and git-lfs (optional). It contains git-hooks, useful scripts and git configs which solve certain 'git vs Unity' problems. Also it contains Unity project structure which is recomended by 'Saritasa Unity guidelines and best practicies'.

# What's happening
> What's happening when i call cookiecutter for this repo?

Answer:
* ```git clone``` current repo
* Run quiz
* Make replacements in project according to quiz resuts ({{ cookiecutter.var }} is replaced by 'var_from_quiz')
* Call [post_gen_project.sh](https://github.com/Saritasa/Unity3DTemplateProject/blob/master/hooks/post_gen_project.sh "hook")  hook:
  * Create .gitattributes file
  * Initialize git in destination folder
  * Run ```./scripts/setup_project.command```
    * Set proper git config 
    * Set up git hooks
  * [optional] Initialize git-lfs
  * [optional] Remove git-lfs specific stuf from non-lfs project
  * Run Unity without UI in order to create empty project
  * Commit all changes to git
