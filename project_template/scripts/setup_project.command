#!/bin/sh

if [[ "$1" == 'mac' ]]; then
   git config core.autocrlf input
elif [[ "$1" == 'windows' ]]; then
   git config core.autocrlf true
else
   echo "Error: Specify OS type. Usage: setup_project.command ostype [options: 'mac' 'windows']"
   exit 1
fi

# core.hooksPath available for git 2.9.0 and +
git config core.hooksPath './scripts/hooks'