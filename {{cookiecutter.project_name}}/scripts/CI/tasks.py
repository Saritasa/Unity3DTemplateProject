from invoke import task
import utils
import os
import shutil
import getpass

def project_path():
    """ Absolute path to project """
    return os.path.join(utils.repo_path(), "src", "{{cookiecutter.project_name}}", "")

@task()
def build_game_windows(ctx):
    """ Build game for Windows """
    utils.run_project_method(project_path(), "BuildProject.Windows", ctx.config.unity_editor_path)

@task()
def build_game_android(ctx):
    """ Build game for Android """
    utils.run_project_method(project_path(), "BuildProject.Android", ctx.config.unity_editor_path)
