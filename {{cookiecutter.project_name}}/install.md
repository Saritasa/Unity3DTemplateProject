# Requirements
This project requires following binaries: 
* git (min 2.9.0)
{%- if cookiecutter.use_gitlfs == "y" %}
* git-lfs (min 1.4.1)
{%- endif %}
* [*in case if you intended to build docs*] Python, Sphinx package

# Project install

1. Clone project ```git clone project_url```
2. Run this **bash** script:
   ```
   ./scripts/setup_project.command
   ```
3. [**Optional**] See doc installation guidelines in 
```
docs/install.md
```

# Attention
SourceTree for Mac might use  embedded version of git {% if cookiecutter.use_gitlfs == "y" -%} [git-lfs] {% endif %}which is older than required. Install last version of binaries and switch to 'Use system ...' option in **SourceTree -> Preferences -> Git**.

