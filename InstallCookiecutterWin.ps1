wget https://www.python.org/ftp/python/3.5.2/python-3.5.2-amd64-webinstall.exe -OutFile python351.exe

# PrependPath=1 adds install and Scripts directories tho PATH and .PY to PATHEXT
.\python351.exe /passive PrependPath=1 | Out-Null

$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

pip install cookiecutter