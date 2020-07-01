#!/bin/sh

# if unityBin is not match your setup, consider to clone this repo as is, change path call, cookiecutter localy
{% if cookiecutter.os_type == "Mac" %}
unityBin="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
{% elif cookiecutter.os_type == "Windows" %}
unityBin="/C/Program Files/Unity/Hub/Editor/2019.4.2f1/Editor/Unity.exe"
{% else %}
echo "Operating system is not specified"
exit 1
{% endif %}

if [ ! -f "$unityBin" ]; then
echo $unityBin
    echo "Can't find Unity editor binaries. Please enter the path to the Unity Editor.(Drag and drop Unity.exe here.."
    read unityBinRaw
	
	#! for windows user removes single quates when drga and drop Unity.exe
	unityBin="${unityBinRaw%\'}"
	unityBin="${unityBin#\'}"

	if [ ! -f "$unityBin" ]; then
	echo $unityBin
	echo "Can't find Unity editor on specified path. Exiting..."
		sleep 3
		exit 1
	fi
fi

ostype="{{cookiecutter.os_type}}"
projectname="{{cookiecutter.project_name}}"

mv disabled.gitattributes .gitattributes

git init

{% if cookiecutter.use_gitlfs == "n" %}
# Remove git-lfs specific files
rm scripts/fix_false_modified_files.command
{% endif %}

echo "Running Unity to create empty project..."
echo ""
echo "[ONLY IF PROGRESS STUCK (> 1 min)]: Close script and run Unity directly."
echo "There are probably startup problems which do not allow to perform project creation."
echo ""

"$unityBin" -projectPath $PWD/src/$projectname -quit -batchmode

./scripts/setup_project.command $ostype

echo ""
echo "Commit changes"

git add .

git commit -m "Create empty Unity project."