# Template repository for Unity3D projects
This repo creates new pre-configured git repo with empty unity project.

# Usage: 
create_new_project.command ostype [options: 'mac' 'windows'] projectname 

E.g. if you want to create new project on windows machine with name PacMan, call:
create_new_project.command windows PacMan  

# Result:
New .git repo, configured to work with git-lfs and Unity.
It contains git-hooks and useful scripts, git configs which solve certain 'git vs Unity' problems.
Also it contains Unity project structure which is recomended by 'Saritasa Unity guidelines and best practicies'.
