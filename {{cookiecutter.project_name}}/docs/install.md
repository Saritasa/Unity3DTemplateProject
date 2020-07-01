## Documentation

If more comprehensive documentation is required for the project, it is agreed to use: (DocFX)[https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html]

Installation and usage is quite straightforward:

* *Step1.* Install DocFX. Choose from one of the following sources:
    * **[Chocolatey](https://chocolatey.org/packages/docfx)**: `choco install docfx -y`.
    * **[Homebrew](https://formulae.brew.sh/formula/docfx)** (owned by community): `brew install docfx`.
    * **GitHub**: download and unzip `docfx.zip` from https://github.com/dotnet/docfx/releases, extract it to a local folder, and add it to PATH so you can run it anywhere.

* *Step2.* Create a sample project
    ```
    docfx init -q
    ```
    This command generates a default project named `docfx_project`.

* *Step3.* Build the website
    ```
    docfx docfx_project\docfx.json --serve
    ```

Now you can view the generated website on http://localhost:8080.

> [!Important]
>
> For macOS/Linux users: Use [Mono](https://www.mono-project.com/) version >= 5.10.
>
> For macOS users: **DO NOT** Install Mono from Homebrew, but rather from the [Mono download page](https://www.mono-project.com/download/stable/#download-mac).
