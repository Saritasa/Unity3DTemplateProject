#!/bin/sh

if [[ "$1" == 'Mac' ]]; then
   git config core.autocrlf input
elif [[ "$1" == 'Windows' ]]; then
   git config core.autocrlf true
else
   echo "Error: Specify OS type. Usage: setup_project.command ostype [options: 'Mac' 'Windows']"
   exit 1
fi

# core.hooksPath available for git 2.9.0 and +
git config core.hooksPath './scripts/hooks'

{% if cookiecutter.use_gitlfs == "y" %}
echo "Initialize git-lfs:"
git lfs install --local
{% endif %}

echo "Project setup is successfully completed."