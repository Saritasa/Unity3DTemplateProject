# TortoiseGit_DontUseGitDLL

TortoiseGit (TGit) sometimes shows unchanged files as modified. 'git status' shows proper list and fixes current changeset for TGit. But it is possible to reproduce situation again.

Make TGit use git.exe directly, instead of call to dll. Just added record to Windows registry. 
https://github.com/TortoiseGit/TortoiseGit/blob/a6945f006cfacd6b522fac4a38d4c2d2c264f9f2/src/Git/Git.cpp#L2578

BTW: This option is not presented in TGit config.

BTW2: This change possibly breaks certain functionality, but according to source code there are not too much use cases of the option so far.
