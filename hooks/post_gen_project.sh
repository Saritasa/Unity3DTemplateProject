#!/bin/sh

{% if cookiecutter.os_type == "Mac" %}
unityBin="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
{% elif cookiecutter.os_type == "Windows" %}
unityBin="/C/Program Files/Unity/Editor/Unity.exe"
{% else %}
echo "Operating system is not specified"
exit 1
{% endif %}

if [ ! -f "$unityBin" ]; then
echo $unityBin
    echo "Can't find Unity editor binaries"
    read input_variable
    exit 1
fi

ostype="{{cookiecutter.os_type}}"
projectname="{{cookiecutter.project_name}}"

mv disabled.gitattributes .gitattributes

git init

./scripts/setup_project.command $ostype

{% if cookiecutter.use_gitlfs == "y" %}
git lfs install --local
{% endif %}

"$unityBin" -projectPath $PWD/develop/$projectname -quit -batchmode

git add .

git commit -m "Create empty Unity project."