# {{ cookiecutter.project_full_name }}

# Description:
{{ cookiecutter.project_description }}

# Requirements
This project requires following binaries: 
* git (min 2.9.0)
{%- if cookiecutter.use_gitlfs == "y" -%}
* git-lfs (min 1.4.1)
{%- endif -%}

**Attention**:
Source tree for Mac, by default, uses emdedded version of git {% if cookiecutter.use_gitlfs == "y" -%} [git-lfs] {% endif %}which is older than reqiured. Install last version of binaries and switch to 'Use system git' {% if cookiecutter.use_gitlfs == "y" -%} [Use system git-lfs] {% endif %} in SourceTree -> Preferences -> Git.

# Note:
Do not forget to run /scripts/setup_project.command script when clone project:

git {% if cookiecutter.use_gitlfs == "y" -%}lfs {% endif %}clone url
./scripts/setup_project.command Windows

And for Mac:
git {% if cookiecutter.use_gitlfs == "y" -%}lfs {% endif %}clone url
./scripts/setup_project.command Mac

{% if cookiecutter.use_gitlfs == "y" -%}
git lfs clone loads lfs-project more faster than usual clone. setup_project.command installs git hooks and adjust local git config to cover Unity workflows.
{%- endif -%}
