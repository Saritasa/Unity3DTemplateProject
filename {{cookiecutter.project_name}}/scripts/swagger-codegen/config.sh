#!/bin/sh

# Add models required in project
declare -a API_MODELS=("$modelsPath/LoginData.cs"
    "$modelsPath/Age.cs"
    "$modelsPath/User.cs"
    )

# Add tests required in project
declare -a API_MODELS_TEST=("$testsPathSrc/LoginDataTest.cs"
    "$testsPathSrc/AgeTest.cs"
    "$testsPathSrc/UserTest.cs"
    )
