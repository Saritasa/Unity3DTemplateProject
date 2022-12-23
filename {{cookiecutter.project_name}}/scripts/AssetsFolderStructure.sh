#!/bin/sh
#Creating our default Asset folder structure script.

cd "../src" 
allUnityProjects=()
pathToGenerate=""

#Functions
generate_folders_structure()
{
    mkdir -p "$pathToGenerate"/{Audio,Editor,Models,Plugins,Scenes,Scripts/API,Shaders,UI,UserWorkspace,Vendors}

    touch "$pathToGenerate"/Audio/.gitkeep
    touch "$pathToGenerate"/Editor/.gitkeep
    touch "$pathToGenerate"/Models/.gitkeep
    touch "$pathToGenerate"/Plugins/.gitkeep
    touch "$pathToGenerate"/Scenes/.gitkeep
    touch "$pathToGenerate"/Scripts/.gitkeep
    touch "$pathToGenerate"/Scripts/API/.gitkeep
    touch "$pathToGenerate"/Shaders/.gitkeep
    touch "$pathToGenerate"/UI/.gitkeep
    touch "$pathToGenerate"/UserWorkspace/.gitkeep
    touch "$pathToGenerate"/Vendors/.gitkeep

    echo "Generation succeeded"
}

drag_and_drop()
{
    echo -e "Please enter the path to the Assets folder. \nDrag and drop Unity Assets folder here.."
    read assetFolder
    assetFolder="${assetFolder%\'}"
    assetFolder="${assetFolder#\'}"
    
    pathToGenerate=$assetFolder

    generate_folders_structure
}

select_certain_unity_project()
{

    for index in "${!allUnityProjects[@]}"
    do
        echo -e "\nGenerate folders in:"
        echo -e "\n[$(( index + 1 ))] ${allUnityProjects[$index]}"
    done

    read choosedFolder

    for index in "${!allUnityProjects[@]}"
    do
        if [[ $(( index + 1 )) == "$choosedFolder" ]]; then
            pathToGenerate="./${allUnityProjects[$index]}/Assets"
            generate_folders_structure
        fi
    done
}

choose_between_dd_and_selection()
{
    read choosedNumber
    case $choosedNumber in
        1)
            select_certain_unity_project
            break
            ;;
        2)
            drag_and_drop
            break
            ;;
        *)
            echo "Invalid number, only 1 or 2"
            choose_between_dd_and_selection
            ;;
    esac
}

#Main Program
for projectFolder in *
do
    DIR="$projectFolder/Assets"
    if [ -d "$DIR" ]; then
        allUnityProjects+=("$projectFolder")
    fi
done

# Can't use array size for check, because of jinja2 and shell conflict.

if [ -d "${allUnityProjects[0]}" ]; then
    echo -e "Unity projects already exist in the \"src\" folder. \n Do you want to choose one of them? [1]  \n or Drag and Drop Unity Assets folder [2]?"
    choose_between_dd_and_selection
else
    echo "No Unity projects in \"src\" folder"
    drag_and_drop
fi