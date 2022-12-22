# Important
Do not forget to run this script after project clone:

```
./scripts/setup_project.command
```

Otherwise project will be not initialized, git settings are not adjusted, git hooks are not installed.
It is safe to run this script several times (in case if you forget whether you executed it before). The only rescriction: *you shall not have staged files at the moment*.

# Description:
{{ cookiecutter.project_description }}

# Project
It is required to maintain original project skeleton ([folder structure](https://wiki.saritasa.rocks/unity/project-structure)). It is not restricted to add new folders or remove some of leaf folders. But changing the nesting may spoil some utility scripts or tools which were created for certain file structure. 
To create default Saritasa Unity folder structure run
```
 ./scripts/AssetsFolderStructure.sh
```

# Repository structure

* **artifacts** -- *Anything what can be useful for project participants: 3d models, .psd, documents, etc..*
* **docs** -- *Project documentation.*
* **scripts** -- *Useful scripts.*
  * **hooks** -- *git hooks. Git from 2.9.0+ allows to change `git config core.hooksPath /scripts/hooks/`.*
* **src** -- *Project sources*
  * **{{cookiecutter.project_name}}** -- *root folder of Unity project. Unity takes project name from name of this folder.*
    * **[Unity project structure](https://wiki.saritasa.rocks/unity/project-structure)**