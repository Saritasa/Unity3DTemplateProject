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
{% else %}
# Remove git-lfs specific files
rm scripts/fix_false_modified_files.command
{% endif %}

echo "Running unity to create empty project..."
echo "[If script stuck (normally it takes < 30s) on this stage -- close script and run Unity directly"
echo "There are probably some problems on unity startup, e.g. user is not logged in]"

"$unityBin" -projectPath $PWD/src/$projectname -quit -batchmode

git add .

git commit -m "Create empty Unity project."