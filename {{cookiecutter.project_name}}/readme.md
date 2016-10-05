# {{ cookiecutter.project_name }}

# Important
Do not forget to run this script after project clone:

```./scripts/setup_project.command```

Otherwise project will be not initialized, git settings are not adjusted, git hooks are not installed.
It is safe to run this script several times (in case if you forget whether you executed it before).

# Description:
{{ cookiecutter.project_description }}

# Requirements
This project requires following binaries: 
* git (min 2.9.0)
{%- if cookiecutter.use_gitlfs == "y" %}
* git-lfs (min 1.4.1)
{%- endif %}

**Attention**:
SourceTree for Mac, by default, uses embedded version of git {% if cookiecutter.use_gitlfs == "y" -%} [git-lfs] {% endif %}which is older than required. Install last version of binaries and switch to 'Use system ...' option in **SourceTree -> Preferences -> Git**.

{% if cookiecutter.use_gitlfs == "y" -%}
# Note:
Use ```git lfs clone``` in order to clone project. It loads lfs-project more faster than usual ```git clone```.
{%- endif -%}

# Project
It is required to maintain original project skeleton ([folder structure](https://kb.saritasa.com/Unity/UnityProjectStructure)). It is not restricted to add new folders or remove some of leaf folders. But changing the nesting may spoil some utility scripts or tools which were created for certain file structure.