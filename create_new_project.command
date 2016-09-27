#!/bin/sh

if [ "$#" -ne 2 ]; then
   echo "Illegal number of parameters."
   echo "Usage: create_new_projects.command ostype [options: 'mac' 'windows'] projectname"
   exit 1
fi

if [[ "$1" == 'mac' ]]; then
   unityBin="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
elif [[ "$1" == 'windows' ]]; then
   unityBin="/C/Program Files/Unity/Editor/Unity.exe"
else
   echo "Error: Specify OS type. Usage: create_new_projects.command ostype [options: 'mac' 'windows'] projectname"
   exit 1
fi

ostype=$1
projectname=$2

if [ ! -f "$unityBin" ]; then
    echo "Can't find Unity editor binaries"
    exit 1
fi

mkdir $projectname

git -C $projectname init

cp -rf ./project_template/* ./$projectname/

cd $projectname

./scripts/setup_project.command $ostype

git lfs install --local

git add .

git commit -m "Initial commit."