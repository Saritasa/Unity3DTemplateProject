echo "Verify whether python is already installed to system... (takes ~1 min)"
$installedProduct = Get-CimInstance -Class Win32_Product -Filter "Name LIKE 'Python%Executables%'"
if ($installedProduct)
{
    echo "Python is already installed to your system. Options:"
    echo " * Uninstall python, run script again."
    echo " * call: 'pip install cookiecutter', but make sure that [path to python]\Scripts are added to path."
    echo "   Otherwise cookicutter command will not work without full path specifing."
}

echo "Downloading python 3.5.2..."

iwr https://www.python.org/ftp/python/3.5.2/python-3.5.2-amd64-webinstall.exe -OutFile python352.exe

echo "Installing python..."

# PrependPath=1 adds install and Scripts directories tho PATH and .PY to PATHEXT
.\python352.exe /passive PrependPath=1 | Out-Null

rm python352.exe

# Reload path variable to be able to use it in current terminal session
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

pip install cookiecutter