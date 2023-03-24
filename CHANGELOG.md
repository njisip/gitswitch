# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

<!-- ## [Unreleased] -->

## [v0.2.0] - 2023-03-24

### Added

- `init` command to create a git repository with option to set user on initialization. (#6)

### Changed

- Modify running git process to get output of long running commands like `clone`. (#7)

## [v0.1.0] - 2023-03-22

### Added

- `user` command to view local, global, all users. (#1)
- `user add` command to add users. (#2 )
- `user switch` command to switch local or global user to the specified user. (#4)

[unreleased]: https://github.com/njisip/gitswitch/compare/v0.2.0...HEAD
[v0.2.0]: https://github.com/njisip/gitswitch/compare/v0.1.0...v0.2.0
[v0.1.0]: https://github.com/njisip/gitswitch/releases/tag/v0.1.0
