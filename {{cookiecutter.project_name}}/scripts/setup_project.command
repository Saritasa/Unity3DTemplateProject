#!/bin/sh

cd "$(git rev-parse --show-cdup)"

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

# This part ensures that all *.command files and git hooks have proper permissions (+x)
# If there are some changes, it will commit them.

# ATTENTION: It is assumed that there are no staged files (except scripts) at the moment.
# Otherwise they will be commited as well

# Change mode for executable files
git ls-files --others --exclude-standard -z *.command | xargs -0 git update-index --add --chmod=+x
git ls-files --others --exclude-standard -z scripts/hooks/* | xargs -0 git update-index --add --chmod=+x
git ls-files *.command -z | xargs -0 git update-index --add --chmod=+x
git ls-files scripts/hooks/* -z | xargs -0 git update-index --add --chmod=+x

# It is required to maintain mode of executable files.
# In case if someone accidently will change permissions hooks will not work on Mac
# It is ok if this commit will appear in your local branch after clone. Push it as soon as possible
git commit -m "Shell scripts with proper permissions"

# Fix problem on Mac. All files changed by 'git update-index --add --chmod=+x' appear simultaneously as staged and unstaged
# Thus we commit staged and revert unstaged files
git ls-files -z *.command | xargs -0 git checkout
git ls-files -z scripts/hooks/* | xargs -0 git checkout

echo "Project setup is successfully completed."