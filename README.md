# NowPlayingSpotify

NowPlayingSpotify reads the song currently playing in the Spotify desktop app, shows it in a small
tray window, and publishes it to Windows Live Messenger as your "now playing" status.

It is a single Win32 executable with no runtime dependency.
One 40 KB file that runs on Windows XP through Windows 11.

## Requirements

- Windows XP or newer
- Spotify desktop application installed
- Windows Live Messenger, connected through [Escargot.chat](https://escargot.chat/)

In Messenger, the "show what I'm listening to" option must be enabled, otherwise it ignores the
status. It is in the dropdown next to your personal message, or under Tools > Options > Personal.

## Installation

1. Download the latest release from the [releases](https://github.com/Frenvius/NowPlayingSpotify/releases) page.
2. Extract the archive anywhere.
3. Run `NowPlayingSpotify.exe`.

## Usage

1. Start Spotify and play a song.
2. Start `NowPlayingSpotify`.
3. The song shows up in the window and in Messenger, refreshing every 3 seconds.

Drag the window from anywhere on it. The close button hides it to the tray; double click the tray
icon to bring it back, right click it for Exit. Quitting clears the Messenger status.

## Building

Requires a MinGW-w64 i686 toolchain with the MSVCRT runtime ([WinLibs](https://winlibs.com/)):

```
export PATH="/path/to/mingw32/bin:$PATH"
mingw32-make          # -> build/NowPlayingSpotify.exe
```

## To Do

- [x] Get currently playing song from Spotify
- [x] Display currently playing song in application
- [x] Send currently playing song to Windows Live Messenger
- [x] Minimize application to system tray
- [ ] Add a settings page
- [ ] Add option to start application on Windows startup
- [ ] Add music controls to application (maybe, if possible)
- [ ] Add support for other languages

## Contributing

If you encounter any issues or have suggestions for improvements, please
[open an issue](https://github.com/Frenvius/NowPlayingSpotify/issues) or submit a pull request.
