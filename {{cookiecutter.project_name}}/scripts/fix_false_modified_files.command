#!/bin/sh

# Fix for annoying problem of false modified files. To reproduce this problem it is required:
# 1) Commit any *.large file to repo.
# 2) Then call: git lfs track "*.large"
# 3) After that git status will show this file as modified.
# Obvious solution: Commit these files once again. But unfortunately you will have same problem in each branch you check out.
# Previously we had to commit changes to each branch and resolve merge conflicts then. 
# This script allows temporary remove these false modified files from current branch, this works until you touch this files once again.
# Consider to merge commit which uploads files to lfs to each branch. After then problem will be solved for future commits.

# But it will be still possible to repeat problem if you switch to revision before uploading these files to lfs. 
# Currently we don't have solution for that besides this workaround.

# It is required to just disable clean filter and call git status.

git -C './' -c filter.lfs.clean= -c filter.lfs.required=false status > /dev/null
