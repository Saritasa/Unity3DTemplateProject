# {{ cookiecutter.project_full_name }}

# Description:
{{ cookiecutter.project_description }}

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
