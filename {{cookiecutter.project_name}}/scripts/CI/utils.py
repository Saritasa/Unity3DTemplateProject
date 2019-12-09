""" Trivver Build scripts misc utilities """ 

import os
from sys import platform
from os import environ, path
import shutil
import subprocess

class bcolors:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'
    BOLD = '\033[1m'
    UNDERLINE = '\033[4m'

def rmdir(path):
    """ Remove folder and content """

    if not os.path.exists(path):
        return

    for root, dirs, files in os.walk(path, topdown=False):
        for name in files:
            os.remove(os.path.join(root, name))
        for name in dirs:
            os.rmdir(os.path.join(root, name))
    os.rmdir(path)

def rmfile_if_exists(path):
    """ Remove file if it is exists """

    if os.path.isfile(path):
        os.remove(path)

def compress_folder(output_filename, path):
    """ Compress folder, use 7z tool for Windows since all python compressors losing file 
    permissions on Windows
    """

    if platform == 'win32':
        args = [
            "7z/7za.exe",
            "a",
            "{}.zip".format(output_filename),
            path]

        success, _ = run_process_blocking(args, None)

        if not success:
            raise Exception( \
                "Error while compressing directory {}".format(path))
    else:
        shutil.make_archive(output_filename, 'zip', path)

def GetAssemblyVersion():
    """ Run GitVersion, get AssemblyVersion param """

    if platform != 'win32':
        raise Exception("Mac version of 'GetAssemblyVersion' is not implemented")

    args = [
        "../GitVersion/GitVersion.exe",
        "/showvariable",
        "AssemblySemVer"]

    success, output = run_process_blocking(args, None)
    if not success:
        raise Exception( \
            "Error while GettingAssemblyVersion")

    return output.strip()

def repo_path():
    """ Absolute, root path of current repo """
    return os.path.realpath(os.path.join(os.path.dirname(os.path.realpath(__file__)), "..", ".."))

def shell(ctx):
    """ Workaround fro pyinvoke bug (run does not work for windows) """
    if platform == 'win32':
        ctx._config['run']['shell'] = environ['COMSPEC']
    return ctx

def short_git_version_hash(ctx):
    """ Returns short version of git hash for current commit """

    ctx = shell(ctx)

    print ("Current version is:")
    return unicode.strip(ctx.run("git rev-parse --short HEAD").stdout)

def git_branch(ctx):
    """ Get current git branch """

    ctx = shell(ctx)

    print ("Current branch is:")
    return unicode.strip(ctx.run("git rev-parse --abbrev-ref HEAD").stdout)

def yes_or_no(question):
    while "the answer is invalid":
        reply = str(raw_input(question+' (y/n): ')).lower().strip()
        if reply[0] == 'y':
            return True
        if reply[0] == 'n':
            return False

def run_process_blocking(args, log_file):
    """ Creates subprocess, waits for completion. If process exits with non-zero code - 
    return false, true otherwise. As arguments takes subprocess.Popen args. 
    and resulting log_file. See docs: https://docs.python.org/2/library/subprocess.html
    """

    process = subprocess.Popen(args, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    output, err = process.communicate()
    if log_file is not None:
        with open(log_file, 'r') as fin:
            print (fin.read())

    print (output)
    print ("Errors:")
    print (err)

    return (process.returncode == 0, output)

def run_project_method(project_path, mehtod_name, unity_editor_path, custom_cmd_param = ""):
    """ Execute UnityEditor.exe with give project and execute give method. 
    Used to lunch C# build scripts
    """

    log_file_name = "{}.log.txt".format(mehtod_name)
    rmfile_if_exists(log_file_name)

    args = [
        unity_editor_path,
        "-quit",
        "-batchmode",
        "-logfile", "{}".format(log_file_name),
        "-executeMethod", mehtod_name,
        "-projectPath", "{}".format(project_path),
        custom_cmd_param]

    success, _ = run_process_blocking(args, log_file_name)
    if not success:
        raise Exception(
            "Error while executing {} for project {}".format(mehtod_name, project_path))