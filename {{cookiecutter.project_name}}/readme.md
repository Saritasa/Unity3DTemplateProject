# test-ci

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
It is required to maintain original project skeleton ([folder structure](https://kb.saritasa.com/Unity/UnityProjectStructure)). It is not restricted to add new folders or remove some of leaf folders. But changing the nesting may spoil some utility scripts or tools which were created for certain file structure.

# Build Scripts

## Instalation
1. Install Python 3
2. Run:
    ```
    pip install invoke
    ```
3. Copy ```invoke_template.yaml``` to ```invoke.yaml```. Fill settings in ```invoke.yaml```

## Usage:

Call
```
inv -l
```
in order to get available functions.

Usage example
```
inv build-game-android
```

# Repository structure

* **artifacts** -- *Anything what can be useful for project participants: 3d models, .psd, documents, etc..*
* **docs** -- *Project documentation.*
* **scripts** -- *Useful scripts.*
  * **CI** -- *Continuous integration scripts*
  * **hooks** -- *git hooks. Git from 2.9.0+ allows to change `git config core.hooksPath /scripts/hooks/`.*
* **src** -- *Project sources*
  * **test-ci** -- *root folder of Unity project. Unity takes project name from name of this folder.*
    * **[Unity project structure](https://kb.saritasa.com/Unity/UnityProjectStructure)***
 