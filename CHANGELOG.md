Changelog
=========

All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

## [2.0.0] - 2015-11-19
### Added
- Updated for the new mastery system.

### Fixed
- Prevent crashing when no mastery data is found for the selected champion and role.

## [1.1.3] - 2015-10-16
### Added
- Application version number now visible (including a link to the latest release page).

### Fixed
- Fixed an issue where having the client minimized when assigning masteries would cause the cursor to get stuck in the top-left corner of the screen.

## [1.1.2] - 2015-10-13
### Fixed
- Adjusted coordinate calculation to support default client size at 1920x1080 resolution.

## [1.1.1] - 2015-10-12
### Added
- Will now prompt to run as administrator so that the application can properly lock the cursor during mastery assignment.
- Portable version (.zip archive) now offered in addition to the installer.

### Fixed
- Adjusted coordinate calculation code.
  - All client sizes are supported when assigning mastery points from the menu.
  - More client sizes are supported when assigning mastery points during champion select.
    - Larger clients still have issues. Will continue to address.

## [1.1.0] - 2015-10-11
### Added
- Show loading screen while downloading the mastery data from Champion.GG on first run.
- Screenshots and GIFs showing usage.

### Fixed
- Put back the static masteries JSON data.

## [1.0.0] - 2015-10-11
- Initial release.

[2.0.0]: https://github.com/maxdeviant/lol-mastery-manager/compare/v1.1.3...v2.0.0
[1.1.3]: https://github.com/maxdeviant/lol-mastery-manager/compare/v1.1.2...v1.1.3
[1.1.2]: https://github.com/maxdeviant/lol-mastery-manager/compare/v1.1.1...v1.1.2
[1.1.1]: https://github.com/maxdeviant/lol-mastery-manager/compare/v1.1.0...v1.1.1
[1.1.0]: https://github.com/maxdeviant/lol-mastery-manager/compare/v1.0.0...v1.1.0
[1.0.0]: https://github.com/maxdeviant/lol-mastery-manager/compare/d946e11...v1.0.0
