# Requirements
This project requires following binaries: 
* git (min 2.9.0)
{%- if cookiecutter.use_gitlfs == "y" %}
* git-lfs (min 1.4.1)
{%- endif %}
* [*in case if you intended to build docs*] Python, Sphinx package

# Project install

{% if cookiecutter.use_gitlfs == "y" %}
1. Use ```git lfs clone``` in order to clone project. It loads lfs-project more faster than usual ```git clone```.
{%- else %}
1. Clone project ```git clone project_url```
{%- endif %}
2. Run this **bash** script:
   ```
   ./scripts/setup_project.command
   ```
3. [**Optional**] In order to build docs it is required to install Python and Sphinx.
   * Here are installation guides ([Mac](https://github.com/Saritasa/Unity3DTemplateProject#setup-instructions-mac), [Windows](https://github.com/Saritasa/Unity3DTemplateProject#setup-instructions-windows)) for scaffold project. You can use it to install Python and pip package manager. Nothing wrong if you will install cookiecutter package as well.
   * Install Sphinx using `pip install Sphinx` or `easy_install Sphinx`
   * In order to build docs use:
     ```
     cd docs
     make html
     ```
     After that they will be available in `/docs/build/html`.

 # Attention
SourceTree for Mac, by default, uses embedded version of git {% if cookiecutter.use_gitlfs == "y" -%} [git-lfs] {% endif %}which is older than required. Install last version of binaries and switch to 'Use system ...' option in **SourceTree -> Preferences -> Git**.

