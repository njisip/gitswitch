# gitswitch
A CLI tool to easily switch between users in a repository. It allows having a collection of user information and easily set them as local or global user using a key.

## Requirements

- [.NET 6.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or above
- [Git](https://git-scm.com/)

## Installation

1. Download the [latest version](https://github.com/njisip/gitswitch/releases/latest) from [releases](https://github.com/njisip/gitswitch/releases).
2. Place the application on any directory you want.
3. Add the directory to the PATH user varible. ([Guide](https://www.java.com/en/download/help/path.html))

## Usage

1. Run the app on Windows Terminal/Powershell/Command Prompt or any other terminal.
2. To see available commands, arguments, and options, run:
	```
	gitsw -h
	```
	It will show the help documentation.
	```bash
	Description:
	A tool to easily switch between users in a repository

	Usage:
	gitsw [command] [options]

	Options:
	--version       Show version information
	-?, -h, --help  Show help and usage information

	Commands:
	user  Show user information
	init  Create an empty Git repository or reinitialize an existing one

	```

### Samples

#### Show local user
```sh
gitsw user
```
#### Show global user
```sh
gitsw user -g
```
#### Show all users
```sh
gitsw user -a
```
#### Add user
```sh
gitsw user add "key" "name" "email"
```
#### Add user and set as local user
```sh
gitsw user add "key" "name" "email" -s
```
#### Switch local user
```sh
gitsw user switch "key"
```
#### Switch global user
```sh
gitsw user switch "key" -g
```
#### Initialize repository with user
```sh
gitsw init -u "key"
```

## License

This project is under the [MIT License](LICENSE).
