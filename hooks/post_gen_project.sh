#!/bin/sh

ostype="{{cookiecutter.os_type}}"
projectname="{{cookiecutter.project_name}}"

mv disabled.gitattributes .gitattributes

git init

{% if cookiecutter.use_gitlfs == "n" %}
# Remove git-lfs specific files
rm scripts/fix_false_modified_files.command
{% endif %}

./scripts/setup_project.command $ostype

echo ""
echo "Commit changes"

git add .

git commit -m "Create empty project."